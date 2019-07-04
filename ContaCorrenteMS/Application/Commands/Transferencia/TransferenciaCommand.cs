using ContaCorrente.Domain;
using ContaCorrente.Domain.Entities.CommandsInterfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContaCorrenteMS.Application.Commands.Transferencia
{
    public class TransferenciaCommand : IRequest<Response>, ITransferencia
    {
        public int numeroContaOrigem { get; set; }
        public int numeroContaDestino { get; set; }
        public decimal valorTransferencia { get; set; }
    }
}
