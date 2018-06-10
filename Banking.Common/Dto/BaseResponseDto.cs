using System;
using System.Collections.Generic;
using System.Text;

namespace Banking.Common.Dto
{
    public class BaseResponseDto<T>
    {
        public List<T> Data { get; set; }
    }
}
