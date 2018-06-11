using Banking.Application.Accounts.Dto;
using Banking.Common.Dto;
using Banking.Domain.Accounts.Repository;
using Banking.Domain.Common.ValueObject;
using Banking.Domain.Customers.Repository;
using Banking.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Banking.Application.Customers.Service
{
    public class BankingApplicationService : BaseApplication
    {

        private readonly IBankAccountRepository bankAccountRepository;
        private readonly TransferDomainService transferDomainService = new TransferDomainService();

        public BankingApplicationService(IBankAccountRepository bankAccountRepository) : base()
        {
            this.bankAccountRepository = bankAccountRepository;
        }

        public Object PerformTransfer(BankAccountDto originBankAccountDto, BankAccountDto destinationBankAccountDto, Decimal amount)
        {
            try
            {
                BaseResponseDto<BankAccountDto> baseResponseDto = new BaseResponseDto<BankAccountDto>();
                //List<BankAccountDto> bankAccountDto = this.bankAccountRepository.FindByNumber(originBankAccountDto.Number);
                //baseResponseDto.Data = customerDto;

                var originAccount = bankAccountRepository.FindByNumber(originBankAccountDto.Number);
                var destinationAccount = bankAccountRepository.FindByNumber(destinationBankAccountDto.Number);
            //transferDomainService.PerformTransfer(originAccount, destinationAccount, amount);

               originAccount.Balance= originAccount.Balance - amount;
               destinationAccount.Balance= destinationAccount.Balance + amount;

            bankAccountRepository.Update(originAccount);
                bankAccountRepository.Update(destinationAccount);
         
                return baseResponseDto;
        }
            catch (Exception)
            {
                return this.getExceptionErrorResponse();
    }
}

        

    }
}
