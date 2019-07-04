using ContaCorrente.Domain.Entities.CommandsInterfaces;
using ContaCorrente.Domain.Enums;
using ContaCorrente.Domain.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContaCorrente.Domain.Entities
{
    public class ContaCorrente
    {
        public int numeroConta { get;  set; }
        public decimal saldoContaCorrente { get;  set; }
        public ICollection<Lancamentos> lancamentos { get; set; } = new List<Lancamentos>();


        /// <summary>
        /// Construtor sem parametros, usado para criar a lista na camada de Data
        /// </summary>
        public ContaCorrente()
        {
        }


        /// <summary>
        /// Faz as primeiras validacoes de campos obrigatorios para realizar uma transferencia
        /// </summary>
        /// <param name="command"></param>
        public ContaCorrente(ITransferencia command)
        {
            var msgs = ValidarTransferencia(command);

            if (msgs.Count > 0)
                throw new DomainValidationException(msgs);
        }


        /// <summary>
        /// Evento de credito em contacorrente
        /// </summary>
        /// <param name="command"></param>
        public void CreditoContaCorrente(ITransferencia command)
        {
            var lancamento = new Lancamentos(TipoLancamento.Credito, command.valorTransferencia);

            this.AdicionarLancamento(lancamento);

            this.saldoContaCorrente += command.valorTransferencia;

        }

        /// <summary>
        /// Evento de debito em contacorrente
        /// </summary>
        /// <param name="command"></param>
        public void DebitoContaCorrente(ITransferencia command)
        {
            var lancamento = new Lancamentos(TipoLancamento.Debito , command.valorTransferencia);

            this.AdicionarLancamento(lancamento);

            this.saldoContaCorrente -= command.valorTransferencia;
        }

        /// <summary>
        /// Valida se a requisição de transferência está correta
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        protected List<DomainValidationMessage> ValidarTransferencia(ITransferencia command)
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();

            if (command.numeroContaDestino == 0)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error,  nameof(command.numeroContaDestino), "Número da Conta Destino é obrigatorio"));

            if (command.numeroContaOrigem == 0)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error,  nameof(command.numeroContaOrigem), "Número da Conta Origem é obrigatorio"));

            if (command.valorTransferencia <= 0)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error,  nameof(command.valorTransferencia), "Valor da Transferência deve ser maior que zero"));

            if (command.numeroContaDestino == command.numeroContaOrigem)
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, nameof(command.valorTransferencia), "Conta Origem e Conta Destino devem ser diferentes"));

            return messages;
        }

        /// <summary>
        /// Adiciona a transferencia nos lançamentos da conta
        /// </summary>
        /// <param name="lancamentos"></param>
        protected void AdicionarLancamento(Lancamentos lancamentos)
        {
            this.lancamentos.Add(lancamentos);
        }


    }
}
