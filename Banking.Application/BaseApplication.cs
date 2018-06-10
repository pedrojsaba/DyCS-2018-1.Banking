
using Banking.Common.Dto;
using Banking.Common.Notification;
using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Application
{
    public class BaseApplication
    {
        public BaseApplication()
        {
        }

        public BaseErrorResponseDto getExceptionErrorResponse()
        {
            BaseErrorDto error = new BaseErrorDto(500, "Failed Request", 321);
            List<BaseErrorDto> errors = new List<BaseErrorDto>();
            errors.Add(error);
            BaseErrorResponseDto response = new BaseErrorResponseDto();
            response.Errors = errors;
            return response;
        }

        public BaseErrorResponseDto getApplicationErrorResponse(List<Error> errors)
        {
            BaseErrorResponseDto response = new BaseErrorResponseDto();
            List<BaseErrorDto> responseErrors = new List<BaseErrorDto>();
            foreach (Error error in errors)
            {
                responseErrors.Add(new BaseErrorDto(400, error.getMessage(), 123));
            }
            response.Errors = responseErrors;
            return response;
        }

        public int ForceException()
        {
            int a = 5;
            int b = 0;
            return a / b;
        }
    }
}
