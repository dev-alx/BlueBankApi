using Contact.Core.Dto;
using Contact.Core.Enitties;
using Contact.Core.Exceptions;
using Contact.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Core.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IUnitOfWork unitOfWork;

        public ILogger<ClientServices> Logger { get; }

        public ClientServices(IUnitOfWork unitOfWork, ILogger<ClientServices> logger)
        {
            this.unitOfWork = unitOfWork;
            Logger = logger;
        }

        public async Task<Client> InsertClient(Client client)
        {
            //Logger.LogInformation($"Getting a client for {client.SocialNumber}");
            var clie = await unitOfWork.ClientRepository.GetClientBySocialNumber(client.SocialNumber);
            if (clie != null)
            {
                throw new BusinessException ("Numero de identificacion digitado ya se encuentra registrado, solo se puede crear una cuenta bancaria por cliente");
            }
            client.RecordDate = DateTime.Now;
            
            //Logger.LogInformation($"Adding an object of Type Client to the context");
            await unitOfWork.ClientRepository.Add(client);
            //Logger.LogInformation($"Attempting to save the changes in the context");
            await unitOfWork.SaveChangesAsync();
            return client;
        }
        //Paginar por 10
        public async Task<IEnumerable<ClientResponseDto>> GetAll()
        {
            List<ClientResponseDto> lst = new List<ClientResponseDto>();
            //Logger.LogInformation($"Getting all clients");
            var clients = await unitOfWork.ClientRepository.GetAll();
            foreach (var client in clients)
            {
                //Logger.LogInformation($"Getting a bank account for ClientId");
                var bankAccounts = await unitOfWork.BankAccountRepository.GetBankAccountByClientId(client.ClientId);
                foreach (var account in bankAccounts)
                {
                    ClientResponseDto dto = new ClientResponseDto();
                    dto.FullName = client.FullName;
                    dto.SocialNumber = client.SocialNumber;
                    dto.Product = account.Product;
                    dto.InitialBalance = await getTotalAmount(account.InitialBalance, account.BankAccountId);
                    lst.Add(dto);
                }
                
            }
            return lst;
            
        }
        public async Task<Client> GetById(int id)
        {
            //Logger.LogInformation($"Getting a client");
            return await unitOfWork.ClientRepository.GetById(id);
        }
        private async Task<decimal> getTotalAmount(decimal initialAmount,int id)
        {
            decimal total = 0;
            //Logger.LogInformation($"Getting Account Movements by Account Id");
            var accountMovement = await unitOfWork.AccountMovementRepository.GetMovementsByAccountId(id);
            if (accountMovement != null)
            {
                var a = accountMovement.Where(s => s.MovementType == Enumerations.MovementType.Deposit).Select(s => s.Amount).Sum();
                var b = accountMovement.Where(s => s.MovementType == Enumerations.MovementType.Withdrawals).Select(s => s.Amount).Sum();
                total = a - b;
            }
            return total;
        }
    }
}
