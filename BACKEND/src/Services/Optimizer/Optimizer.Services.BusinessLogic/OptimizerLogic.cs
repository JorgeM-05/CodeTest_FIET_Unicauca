using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Optimizer.Services.Commands;
using RabbitMQ.Client;
using System.Text.Json;
using Optimizer.Services.BusinessLogic.Commands;
using Optimizer.Services.Proxies.Seeker;
using Optimizer.Services.Proxies.Seeker.Commands;


namespace Optimizer.Services.BusinessLogic
{
    public interface IOptimizerLogic
    {
        Task<CasOptLogic> OptLogic(CasOptLogic command);
        Task<CasOptLogic> Dictionary(DictionaryModel command);
    }
    public class OptimizerLogic : IOptimizerLogic
    {
            private readonly HttpClient _httpClient;
            public static int num_Variables;
            private readonly ISeekerProxy _seekerProxy;
        public OptimizerLogic(
                HttpClient httpClient,
                ISeekerProxy seekerProxy,
                IHttpContextAccessor httpContextAccessor
                )
            {
               // httpClient.AddBearerToken(httpContextAccessor);
                _httpClient = httpClient;
                _seekerProxy = seekerProxy;
            }


        //Diccionario
        public async Task<CasOptLogic> Dictionary(DictionaryModel command)
        {

                 
            String[] Alphabet = command.Alphabet_User.Split('/');
            String[] Ca_notes = command.CA_notes.Split('\n');
            var aux = 0;

            String concatenado = "";
            Dictionary<int, string> nums = new Dictionary<int, string>();

            for (int i = 0; i < Ca_notes.Length -1; i++)
                {
                //String[] Vatiable_User = Alphabet[i].Split(',');
                 String[] Vatiable = Ca_notes[i].Split(' ');

                for (int K = 0; K < command.Columns; K++) {
                    String[] Vatiable_User = Alphabet[K].Split(',');
                    String[] Vatiable2 = Ca_notes[K].Split(' ');

                             for (int j = 0; j < Vatiable_User.Length; j++) {

                                nums.Add(j, Vatiable_User[j]);
                             }
                    if (K < command.Columns - 1)
                    {
                        concatenado = concatenado + nums[Int32.Parse(Vatiable[K])] + ",";
                        nums.Clear();
                    }
                    else {
                        concatenado = concatenado + nums[Int32.Parse(Vatiable[K])];
                        nums.Clear();
                    }
                }
                if (i < Ca_notes.Length - 2)
                {
                    concatenado = concatenado + "/";
                }
            }

            var commandOpt = new CasOptLogic()
            {
                Columns = command.Columns,
                Strength = command.Strength,
                Alphabet = command.Alphabet_User,
                Rows = command.Rows,
                CA_notes = concatenado,
            };

          
            return await Task.FromResult(commandOpt);
        }



        //Post Optimizador
        public async Task<CasOptLogic> OptLogic(CasOptLogic command)

        {


            string TarjetAlphabet = contructModel(command.TarjetAlphabet);
            var countVariablesTarjet = num_Variables;
            var Alphabet = contructModel(command.Alphabet);
            int Rows = command.Rows;
            var endSol = "";
            string theRequiredCA = "N" + Rows + "K" + command.Columns + "V" + Alphabet + "t" + command.Strength + ".ca";
            string MAtrizCA = command.CA_notes;
            //****************************

            var postToGenerator = new CasPosToGeneratorlogic()
            {
                Columns = command.TarjetColumns,
                Rows = command.Rows,
                Strength = command.Strength,
                Alphabet = command.TarjetAlphabet,
            };

            string message;

            var factory = new ConnectionFactory() { HostName = "localhost" };
            //creamos una conexion
            using (var connection = factory.CreateConnection())
            {
                // creamos un canal
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "hello", durable: false, exclusive: false, autoDelete: false, arguments: null);


                    message = System.Text.Json.JsonSerializer.Serialize(postToGenerator);
                    var body = Encoding.UTF8.GetBytes(message);

                    channel.BasicPublish(exchange: "", routingKey: "hello", basicProperties: null, body: body);

                }
            }

            var myPostOtimicer = new AlgoritmoPostOptimicer(MAtrizCA, theRequiredCA, TarjetAlphabet, countVariablesTarjet);
            var sol = myPostOtimicer.Ejecutar();

            if (sol.Fitness == 0)
            {

                command.Rows = sol.MiCA.N;
                command.Columns = sol.MiCA.K;
                command.Alphabet = command.Alphabet;
                command.TarjetAlphabet = command.TarjetAlphabet;
                endSol = sol.MiCA.ToString();
                command.CA_notes = endSol; // posible error


                //var content = new StringContent(
                //        System.Text.Json.JsonSerializer.Serialize(command),
                //        Encoding.UTF8,
                //        "application/json"
                //    );


                var commandOpt = new CasCreateCommandSeeker()
                {
                    Columns = command.Columns,
                    Strength = command.Strength,
                    Alphabet = command.TarjetAlphabet,
                    Rows = command.Rows,
                    CA_notes = command.CA_notes,
                    //Aux = sol.Fitness,

                };
                await _seekerProxy.SendCasAsync(commandOpt);

                return await Task.FromResult(command);
            }
            else
            {
                return null;
            }

        }


        public static string contructModel(String Alphabet)
        {
            String[] Variables = Alphabet.Split(',');
            num_Variables = Variables.Length;
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
