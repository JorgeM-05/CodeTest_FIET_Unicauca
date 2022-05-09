using Generator.Services.Proxies.Seeker;
using Generator.Services.Proxies.Seeker.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Generator.Servies.SendtoSeeker.Commands;

namespace Generator.Servies.SendtoSekeer
 {
    public class GeneratorSendtoSeeker : INotificationHandler<CasCreateCommand>
    {
        private readonly ISeekerProxy _seekerProxy;
        // inyeccion dependencias de la clase seekerProxy
        public GeneratorSendtoSeeker( 
            ISeekerProxy seekerProxy
            )
        {
            _seekerProxy = seekerProxy;
        }

        //recibimos el objeto y casteamos el objeto.
        public async Task Handle(CasCreateCommand notification, CancellationToken cancellationToken)
        {
            var command = new CasCreateCommandSeeker()
            {
                CAID = notification.CAID,
                Columns = notification.Columns,
                Strength = notification.Strength,
                Alphabet = notification.Alphabet,
                Rows = notification.Rows,
                CA_notes = notification.CA_notes,
                
            };
            // envio del obejto al metodo
            await _seekerProxy.SendCasAsync(command);

        }
      
    }
}
