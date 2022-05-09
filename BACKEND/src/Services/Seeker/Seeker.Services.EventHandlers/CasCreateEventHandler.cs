using API.Seeker.Data;
using API.Seeker.Models;
using MediatR;
using Seeker.Services.EventHandlers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Seeker.Services.EventHandlers
{
    public class CasCreateEventHandler : INotificationHandler<CasCreateCommand>
    {
        private readonly APIContext _context;
        public CasCreateEventHandler(
            APIContext context
            )
        {
            _context = context;
        }

        public async Task Handle(CasCreateCommand command, CancellationToken cancellationToken)
        {
           
            await _context.AddAsync(new CaRepository
            {
                Columns = command.Columns,
                Strength = command.Strength,
                Alphabet = command.Alphabet,
                Rows = command.Rows,
                CA_notes = command.CA_notes,
                Aux = command.Aux
            });
            await _context.SaveChangesAsync();
        }

    }
}
