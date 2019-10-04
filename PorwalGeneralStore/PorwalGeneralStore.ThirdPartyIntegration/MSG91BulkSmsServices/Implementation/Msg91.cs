using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Implementation
{
    public class Msg91 : IMsg91
    {
        private readonly Msg91BulkSmsServiceConfiguration msg91ServiceConfiguration;
        public Msg91(Msg91BulkSmsServiceConfiguration msg91BulkSmsServiceConfiguration)
        {
            msg91ServiceConfiguration = msg91BulkSmsServiceConfiguration;
        }

        public BaseResponse ResendOtpSms(ResendSmsOtpRequest smsRequest)
        {
            throw new NotImplementedException();
        }

        public BaseResponse SendBulkSms(BulkSmsRequest bulkSmsRequest)
        {
            throw new NotImplementedException();
        }

        public EmailOtpResponse SendOtpOnEmail(EmailOtpRequest smsRequest)
        {
            throw new NotImplementedException();
        }

        public BaseResponse SendOtpSms(SmsOtpRequest smsRequest)
        {
            throw new NotImplementedException();
        }

        public BaseResponse SendSingleSms(SingleSmsRequest smsRequest)
        {
            throw new NotImplementedException();
        }

        public BaseResponse VerifyOtpSms(VerifyOtpRequest smsRequest)
        {
            throw new NotImplementedException();
        }
    }
}
