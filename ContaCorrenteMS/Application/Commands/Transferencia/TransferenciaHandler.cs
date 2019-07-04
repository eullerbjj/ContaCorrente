using ContaCorrente.Domain.Repositories;
using ContaCorrente.Domain.Validation;
using ContaCorrente.Infrastructure.DataAcess.Repository;
using ContaCorrenteMS.Application.Commands.Transferencia;
using ContaCorrenteMS.Application.Mediator.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContaCorrenteMS.Application.Commands.Transferencia
{
    public class TransferenciaHandler : AbstractRequestHandler<TransferenciaCommand>
    {
        private readonly IContaCorrenteRepository _contaCorrenteRepository;

        public TransferenciaHandler(IContaCorrenteRepository contaCorrenteRepository)
        {
            _contaCorrenteRepository = contaCorrenteRepository;
        }

        internal override HandleResponse HandleIt(TransferenciaCommand request, CancellationToken cancellationToken)
        {
            List<DomainValidationMessage> messages = new List<DomainValidationMessage>();
            
            //validações da solicitação
            var validation = new ContaCorrente.Domain.Entities.ContaCorrente(request);
           
            var contaDebito = _contaCorrenteRepository.Find(request.numeroContaOrigem);
            if(contaDebito == null)
            {
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, "numeroContaOrigem", "Não foi possível localizar a Conta Origem"));
                throw new DomainValidationException(messages);
            }

            var contaCredito = _contaCorrenteRepository.Find(request.numeroContaOrigem);
            if (contaCredito == null)
            {
                messages.Add(new DomainValidationMessage(ValidationLevel.Error, "numeroContaDestino", "Não foi possível localizar a Conta Destino"));
                throw new DomainValidationException(messages);
            }

            //regras de negocios do debito 
            contaDebito.DebitoContaCorrente(request);

            //regras de negocios do credito
            contaCredito.CreditoContaCorrente(request);

            //Persistir na base neste momento 
            
            return new HandleResponse() { Content = contaCredito };

        }

    }
}
