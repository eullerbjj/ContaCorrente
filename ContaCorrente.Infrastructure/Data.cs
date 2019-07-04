using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContaCorrente.Infrastructure
{
    public static class Data
    {
        public static List<ContaCorrente.Domain.Entities.ContaCorrente> DataContacorrente()
        {
            var contaCorrenteList = new List<ContaCorrente.Domain.Entities.ContaCorrente>();

            contaCorrenteList.Add(new Domain.Entities.ContaCorrente { numeroConta = 123456789, saldoContaCorrente = 3000, lancamentos = new List<Lancamentos>() { new Lancamentos(TipoLancamento.Credito, 10000), new Lancamentos(TipoLancamento.Debito , -7000) } });

            contaCorrenteList.Add(new Domain.Entities.ContaCorrente { numeroConta = 789654321, saldoContaCorrente = 15000, lancamentos = new List<Lancamentos>() { new Lancamentos(TipoLancamento.Credito, 10000), new Lancamentos(TipoLancamento.Credito, 5000) } });

            contaCorrenteList.Add(new Domain.Entities.ContaCorrente { numeroConta = 654321789, saldoContaCorrente = 7000, lancamentos = new List<Lancamentos>() { new Lancamentos(TipoLancamento.Credito, 12000), new Lancamentos(TipoLancamento.Debito, 5000) } });

            return contaCorrenteList;
        }

    }
}
