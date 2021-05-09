using AutoMapper;
using Contact.Api.Response;
using Contact.Core.Dto;
using Contact.Core.Enitties;
using Contact.Core.Interfaces;
using Contact.Core.Services;
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
    public class AccountMovementController : ControllerBase
    {
        private readonly IClientServices clientServices;
        private readonly IBankAccountServices bankAccountServices;
        private readonly IAccountMovementServices accountMovementServices;
        private readonly IMapper mapper;

        public AccountMovementController(IClientServices clientServices,IBankAccountServices bankAccountServices,IAccountMovementServices accountMovementServices, IMapper mapper)
        {
            this.clientServices = clientServices;
            this.bankAccountServices = bankAccountServices;
            this.accountMovementServices = accountMovementServices;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AccountMovementDto accountMovementDto)
        {
            var bankAccount = await bankAccountServices.GetBankAccount(accountMovementDto.Account);
            
            var accountMovement = mapper.Map<AccountMovement>(accountMovementDto);
                    
            accountMovement.BankAccountId = bankAccount.BankAccountId;
                    
            var result = await accountMovementServices.InsertAccountMovement(accountMovement);

            var response = new ApiResponse<AccountMovementDto>(accountMovementDto);

            return Ok(response);            
        }
        
        [HttpGet("{accountNumber}")]
        public async Task<IActionResult> GetMovements(Guid accountNumber)
        {            
            AccountMovementResponseDto dto = new AccountMovementResponseDto();

            var bankAccount = await bankAccountServices.GetBankAccountByAccountNumber(accountNumber);
            if (bankAccount == null)
            {
                return NotFound();
            }
            var client = await clientServices.GetById(bankAccount.ClientId);

            var result = await accountMovementServices.GetMovements(accountNumber);

            var totalAmount = await accountMovementServices.GetTotalAmount(accountNumber);

            dto.FullName = client.FullName;
            dto.Product = bankAccount.Product;
            dto.Balance = totalAmount;
            dto.accountMovement =  mapper.Map<IEnumerable<AccountMovementDto>>(result);

            var response = new ApiResponse<AccountMovementResponseDto>(dto);

            return Ok(response);            
        }
    }
}
