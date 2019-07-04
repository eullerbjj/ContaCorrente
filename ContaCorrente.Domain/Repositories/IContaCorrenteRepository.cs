using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Domain.Repositories
{
    public interface IContaCorrenteRepository
    {
        Domain.Entities.ContaCorrente Find(int numeroConta);
    }
}
