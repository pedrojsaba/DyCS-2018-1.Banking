using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Banking.Application.Accounts.Dto;
using Banking.Application.Customers.Service;
using Banking.Infrastructure.Accounts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Produces("application/json")]
    [Route("api/Transfer")]
    public class TransferController : Controller
    {

        private readonly BankingApplicationService bankingApplicationService;

        public TransferController()
        {
            this.bankingApplicationService = new BankingApplicationService(new BankAccountAdoNetRepository());
        }

        [HttpGet]
        [Route("PerformTransfer")]
        public Object PerformTransfer(string accountFrom, string accountTo, Decimal amount)
        {
            return bankingApplicationService.PerformTransfer(new BankAccountDto { Number = accountFrom }, new BankAccountDto { Number = accountTo }, amount);
        }


    }
}