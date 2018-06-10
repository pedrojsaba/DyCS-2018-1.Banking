using Banking.Common.Dto;
using Banking.Domain.Customers.Entity;
using Banking.Domain.Customers.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Application.Customers.Service
{
    public class CustomerApplication : BaseApplication
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerApplication(ICustomerRepository customerRepository) : base()
        {
            this.customerRepository = customerRepository;
        }

        public Object getCustomersDto(int skip, int pageSize)
        {
            try
            {
                BaseResponseDto<CustomerDto> baseResponseDto = new BaseResponseDto<CustomerDto>();
                List<CustomerDto> customerDto = this.customerRepository.getCustomersDto(skip, pageSize);
                baseResponseDto.Data = customerDto;
                return baseResponseDto;
            }
            catch (Exception)
            {
                return this.getExceptionErrorResponse();
            }
        }

    }
}
