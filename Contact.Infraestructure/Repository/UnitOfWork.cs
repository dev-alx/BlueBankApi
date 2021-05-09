using Contact.Core.Enitties;
using Contact.Core.Interfaces;
using Contact.Infraestructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Infraestructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ContactDbContext dbContext;
        private readonly IClientRepository clientRepository;
        private readonly IBankAccountRepository bankAccountRepository;
        private readonly IAccountMovementRepository accountMovementRepository;

        public UnitOfWork(ContactDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public IClientRepository ClientRepository => clientRepository ?? new ClientRepository(dbContext);

        public IBankAccountRepository BankAccountRepository => bankAccountRepository ?? new BankAccountRepository(dbContext);

        public IAccountMovementRepository AccountMovementRepository => accountMovementRepository ?? new AccountMovementRepository(dbContext);

        public void Dispose()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
    }
}
