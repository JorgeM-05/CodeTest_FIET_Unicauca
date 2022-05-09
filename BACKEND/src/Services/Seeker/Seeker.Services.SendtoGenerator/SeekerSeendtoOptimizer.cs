using MediatR;
using Seeker.Services.Proxies.Generator;
using Seeker.Services.Proxies.Generator.Commands;
using Seeker.Services.Send.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Seeker.Services.SendtoGenerator
{
    public class SeekerSeendtoOptimizer : INotificationHandler<CasOptimizerCommand>
    {

        private readonly ISeekerProxy _seekerProxy;
        // inyeccion dependencias de la clase seekerProxy
        public SeekerSeendtoOptimizer(
            ISeekerProxy seekerProxy
            )
        {
            _seekerProxy = seekerProxy;
        }

        //recibimos el objeto y casteamos el objeto.
        public async Task Handle(CasOptimizerCommand notification, CancellationToken cancellationToken)
        {
            var command = new CasSeekerToOptCommandProxies()
            {
                Columns = notification.Columns,
                Strength = notification.Strength,
                Alphabet = notification.Alphabet,
                Rows = notification.Rows,
                //CA_notes = notification.CA_notes
            };
            // envio del obejto al metodo
            await _seekerProxy.SendCasToOptAsync(command);

        }
    }
}
