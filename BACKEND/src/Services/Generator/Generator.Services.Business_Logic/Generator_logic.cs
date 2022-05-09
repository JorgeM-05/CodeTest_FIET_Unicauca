using Generator.Services.Commands;
using Generator.Services.Proxies.Seeker;
using Generator.Services.Proxies.Seeker.Commands;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Generator.Services.business_logic
{
    public class Generator_logic : INotificationHandler<CasSeekertoGeneratorlogic>
    {
        private readonly ISeekerProxy _seekerProxy;
        // inyeccion dependencias de la clase seekerProxy
        public Generator_logic(
            ISeekerProxy seekerProxy
            )
        {
            _seekerProxy = seekerProxy;
        }

        public async Task Handle(CasSeekertoGeneratorlogic notification, CancellationToken cancellationToken)
        {

            var ALphabet = contructModel(notification.Alphabet);
            string theGenerator = "ISA";
            int theSeed = 1;
            string theParameters = "500 500";
            int rows = notification.Rows - 1 ;
            var endSol = "";
            int flag = 0;
            for (int i = 0; i < 50; i++)
            {
                string theRequiredCA = "N" + rows + "K" + notification.Columns + "V" + ALphabet + "t" + notification.Strength + ".ca";
                var myAlgorithm = new AlgoritmoParalelo(theGenerator, theRequiredCA, theSeed, theParameters);
                var sol = myAlgorithm.Ejecutar();
                int fitness = sol.Fitness;
                if (i == 0 && fitness > 0) {
                    break;
                }
                if (fitness == 0)
                {
                    flag = 1;
                    rows = rows - 1;
                    endSol = sol.MiCA.ToString();
                }
                else if (flag == 1 )
                {
                    var command = new CasCreateCommandSeeker()
                    {

                        Columns = notification.Columns,
                        Strength = notification.Strength,
                        Alphabet = notification.Alphabet,
                        Rows = rows + 1,
                        CA_notes = endSol

                    };
                    await _seekerProxy.SendCasAsync(command);
                    break;
                }
            }
        }
        public static string contructModel(String Alphabet) {
            String[] Variables = Alphabet.Split(',');
            int num_Variables = Variables.Length;
            String concatenado = "";

            for (int i = 0; i < Variables.Length; i++)
            {

                concatenado = concatenado + Variables[i] + "^";
                if (i < (Variables.Length - 1))
                {
                    concatenado = concatenado + "1-";
                }
                else
                {
                    concatenado = concatenado + "1";
                }
            }
            return concatenado;
        }
    }
}
