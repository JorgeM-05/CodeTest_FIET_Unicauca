using MediatR;
using Optimizer.Services.Commands;
using Optimizer.Services.Proxies.Seeker;
using Optimizer.Services.Proxies.Seeker.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Optimizer.Services.EventHandlers
{
    public class OptimizerSendEventHandlers : INotificationHandler<CasOptCommand>
    {
        private readonly ISeekerProxy _seekerProxy;
        // inyeccion dependencias de la clase seekerProxy
        public OptimizerSendEventHandlers(
            ISeekerProxy seekerProxy
            )
        {
            _seekerProxy = seekerProxy;
        }

        //recibimos el objeto y casteamos el objeto.
        public async Task Handle(CasOptCommand notification, CancellationToken cancellationToken)
        {
            var command = new CasCreateCommandSeeker()
            {
                CAID = notification.CAID,
                Columns = notification.Columns,
                Strength = notification.Strength,
                Alphabet = notification.Alphabet,
                Rows = notification.Rows,
                CA_notes = notification.CA_notes,
                //Aux = notification.Aux
            };
            // envio del obejto al metodo
            await _seekerProxy.SendCasAsync(command);

        }
      
    }
}
