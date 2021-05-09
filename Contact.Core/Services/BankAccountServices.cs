using Contact.Core.Enitties;
using Contact.Core.Exceptions;
using Contact.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Services
{
    public class BankAccountServices : IBankAccountServices
    {
        private readonly IUnitOfWork unitOfWork;

        public BankAccountServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<BankAccount> GetBankAccount(Guid accountId)
        {            
            var bankAccount = await unitOfWork.BankAccountRepository.GetBankAccountByAccountNumber(accountId);
            if (bankAccount == null)
            {
                throw new BusinessException("Numero de Cuenta no encontrado");
            }
            return bankAccount;
        }

        public async Task<BankAccount> GetBankAccountByAccountNumber(Guid accountNumber)
        {
            return await unitOfWork.BankAccountRepository.GetBankAccountByAccountNumber(accountNumber);
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountByClientId(int clientId)
        {
            return await unitOfWork.BankAccountRepository.GetBankAccountByClientId(clientId);
        }

        public async Task<BankAccount> InsertBankAccount(BankAccount bankAccount)
        {
            bankAccount.Description = "Cuenta de Ahorro";
            bankAccount.Product = Guid.NewGuid();
            bankAccount.RecordDate = DateTime.Now;
            
            await unitOfWork.BankAccountRepository.Add(bankAccount);
            await unitOfWork.SaveChangesAsync();
            return bankAccount;
        }
    }
}
