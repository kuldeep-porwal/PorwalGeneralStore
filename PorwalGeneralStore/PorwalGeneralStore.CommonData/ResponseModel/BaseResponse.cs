using PorwalGeneralStore.Global;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.CommonData.ResponseModel
{
    public class BaseResponse
    {
        public ResponseStatusCode Status { get; set; } = ResponseStatusCode.Ok;
        public string StatusMessage { get; set; }
        public List<string> ErrorMessage { get; set; }
    }
}
