using MediatR;
using Seeker.Services.Proxies.Generator;
using Seeker.Services.Proxies.Generator.Commands;
using Seeker.Services.Switch.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Seeker.Services.Switch
{
    public class SeekerSwitch : INotificationHandler<CasSeekerSwitchCommand>
    {
        private readonly ISeekerProxy _seekerProxy;
        // inyeccion dependencias de la clase seekerProxy
        public SeekerSwitch (
            ISeekerProxy seekerProxy
            )
        {
            _seekerProxy = seekerProxy;
        }
        // Peticion de creacion en servicio Generador 
        public async Task Handle(CasSeekerSwitchCommand notification, CancellationToken cancellationToken)
        {
             
            var command = new CasSeekerCommandProxies()
            {
                Columns = notification.Columns,
                Strength = notification.Strength,
                Alphabet = notification.Alphabet
            };
            await _seekerProxy.SendCasToGeneratorAsync(command);
        }
    }
}
