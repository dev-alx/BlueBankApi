using Contact.Core.Enitties;
using Contact.Core.Exceptions;
using Contact.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Core.Services
{
    public class AccountMovementServices : IAccountMovementServices
    {
        private readonly IUnitOfWork unitOfWork;

        public AccountMovementServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public async Task<IEnumerable<AccountMovement>> GetMovements(Guid accountNumber)
        {
            var bankAccount = await unitOfWork.BankAccountRepository.GetBankAccountByAccountNumber(accountNumber);
            if (bankAccount == null)
            {
                throw new BusinessException("Numero de Cuenta no encontrado");
            }           
            
            var accountMovement = await unitOfWork.AccountMovementRepository.GetMovementsByAccountId(bankAccount.BankAccountId);
                        
            return accountMovement;
        }

        public async Task<decimal> GetTotalAmount(Guid accountNumber)
        {
            decimal total = 0;
            var bankAccount = await unitOfWork.BankAccountRepository.GetBankAccountByAccountNumber(accountNumber);
            if (bankAccount == null)
            {
                throw new BusinessException("Numero de Cuenta no encontrado");
            }
            var accountMovement = await unitOfWork.AccountMovementRepository.GetMovementsByAccountId(bankAccount.BankAccountId);
            if (accountMovement != null)
            {
                var a = accountMovement.Where(s => s.MovementType == Enumerations.MovementType.Deposit).Select(s => s.Amount).Sum();
                var b = accountMovement.Where(s => s.MovementType == Enumerations.MovementType.Withdrawals).Select(s => s.Amount).Sum();
                total = a - b;
            }
            return total;
        }

        public async Task<AccountMovement> InsertAccountMovement(AccountMovement accountMovement)
        {            
            var result = await unitOfWork.BankAccountRepository.GetById(accountMovement.BankAccountId);

            //Validar que aun tenga saldo
            var saldo = await GetTotalAmount(result.Product);
            if (accountMovement.MovementType == Enumerations.MovementType.Withdrawals)
            {
                if (saldo == 0)
                    throw new BusinessException("El Saldo de la cuenta esta en 0");                
                if ((saldo - accountMovement.Amount) < 0)
                    throw new BusinessException("Saldo insuficiente para realizar la operacion");                
            }

            //Guardar
            accountMovement.RecorDate = DateTime.Now;
            await unitOfWork.AccountMovementRepository.Add(accountMovement);
            await unitOfWork.SaveChangesAsync();
            return accountMovement;

        }
    }
}
