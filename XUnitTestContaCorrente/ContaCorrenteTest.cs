using ContaCorrente.Domain.Entities;
using ContaCorrente.Domain.Entities.CommandsInterfaces;
using ContaCorrente.Domain.Enums;
using ContaCorrente.Infrastructure.DataAcess.Repository;
using ContaCorrenteMS.Application.Commands.Transferencia;
using ContaCorrenteMS.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using Xunit;

namespace XUnitTestContaCorrente
{
    public class ContaCorrenteTest
    {
        [Fact]
        public void TESTAR_TRANSFERENCIA_CC()
        {
            var fakeMediator = new Mock<ITransferencia>();

            fakeMediator.Setup(m => m.numeroContaOrigem).Returns(12346565);
            fakeMediator.Setup(m => m.numeroContaDestino).Returns(123456749);
            fakeMediator.Setup(m => m.valorTransferencia).Returns(2000);

            var contaCorrenteCredito = new ContaCorrente.Domain.Entities.ContaCorrente
            {
                numeroConta = 123456749,
                saldoContaCorrente = 5000,
                lancamentos = new List<Lancamentos> { new Lancamentos(TipoLancamento.Credito, 5000)}
            };

            var contaCorrenteDebito = new ContaCorrente.Domain.Entities.ContaCorrente
            {
                numeroConta = 12346565,
                saldoContaCorrente = 5000,
                lancamentos = new List<Lancamentos> { new Lancamentos(TipoLancamento.Credito, 5000) }
            };

            contaCorrenteCredito.CreditoContaCorrente(fakeMediator.Object);
            contaCorrenteDebito.DebitoContaCorrente(fakeMediator.Object);

            Equals(contaCorrenteCredito.saldoContaCorrente == 7000);
            Equals(contaCorrenteDebito.saldoContaCorrente == 3000);
        }
    }
}
