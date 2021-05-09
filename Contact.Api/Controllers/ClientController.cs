using AutoMapper;
using Contact.Api.Response;
using Contact.Core.Dto;
using Contact.Core.Enitties;
using Contact.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Contact.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientServices clienteServices;
        private readonly IBankAccountServices bankAccountServices;
        private readonly IAccountMovementServices accountMovementServices;
        private readonly IMapper mapper;

        public ClientController(IClientServices clienteServices, IBankAccountServices bankAccountServices, IAccountMovementServices accountMovementServices, IMapper mapper)
        {
            this.clienteServices = clienteServices;
            this.bankAccountServices = bankAccountServices;
            this.accountMovementServices = accountMovementServices;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ClientDto clientDto)
        {
            var cliente =  mapper.Map<Client>(clientDto);
                
            var client = await clienteServices.InsertClient(cliente);
            
            var bank = new BankAccount { ClientId = client.ClientId, InitialBalance = clientDto.InitialBalance };
                    
            var bankAccount = await bankAccountServices.InsertBankAccount(bank);

            var accountMovement = new AccountMovement { Amount = clientDto.InitialBalance, RecorDate = DateTime.Now, BankAccountId = bankAccount.BankAccountId, MovementType = Core.Enumerations.MovementType.Deposit };
            
            var movement = await accountMovementServices.InsertAccountMovement(accountMovement);
                        
            var result = mapper.Map<ClientResponseDto>(client);
            result.Product = bankAccount.Product;
            result.InitialBalance = bankAccount.InitialBalance;
                                
            var response = new ApiResponse<ClientResponseDto>(result);
            
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var clients = await clienteServices.GetAll();                       
            var response = new ApiResponse<IEnumerable<ClientResponseDto>>(clients);
            return Ok(response);
        }
    }
}
