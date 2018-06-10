using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Common.Dto
{
    public class BaseErrorResponseDto
    {
        public List<BaseErrorDto> Errors { get; set; } = new List<BaseErrorDto>();
    }
}
