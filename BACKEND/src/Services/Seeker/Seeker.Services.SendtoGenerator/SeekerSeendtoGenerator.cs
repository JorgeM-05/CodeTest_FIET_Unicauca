using MediatR;
using Seeker.Services.Proxies.Generator;
using Seeker.Services.Proxies.Generator.Commands;
using Seeker.Services.Send.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Seeker.Services.SendtoGenerator
{
    public class SeekerSeendtoGenerator : INotificationHandler<CasGeneratorCommand>
    {

        private readonly ISeekerProxy _seekerProxy;
        // inyeccion dependencias de la clase seekerProxy
        public SeekerSeendtoGenerator(
            ISeekerProxy seekerProxy
            )
        {
            _seekerProxy = seekerProxy;
        }

        //recibimos el objeto y casteamos el objeto.
        public async Task Handle(CasGeneratorCommand notification, CancellationToken cancellationToken)
        {
            var command = new CasSeekerCommandProxies()
            {
                Columns = notification.Columns,
                Strength = notification.Strength,
                Alphabet = notification.Alphabet,
                Rows = notification.Rows,
            };
            // envio del obejto al metodo
            await _seekerProxy.SendCasToGeneratorAsync(command);

        }
    }
}
