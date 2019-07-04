using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Domain.Entities.CommandsInterfaces
{
    public interface ITransferencia
    {
        int numeroContaOrigem { get; set; }
        int numeroContaDestino { get; set; }
        decimal valorTransferencia { get; set; }
    }
}
