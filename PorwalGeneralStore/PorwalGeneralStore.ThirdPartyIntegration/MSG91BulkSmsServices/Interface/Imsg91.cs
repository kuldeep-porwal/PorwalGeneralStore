using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface
{
    public interface IMsg91
    {
        BaseResponse SendSingleSms(SingleSmsRequest smsRequest);
        BaseResponse SendBulkSms(BulkSmsRequest bulkSmsRequest);
        EmailOtpResponse SendOtpOnEmail(EmailOtpRequest smsRequest);
        BaseResponse SendOtpSms(SmsOtpRequest smsRequest);
        BaseResponse ResendOtpSms(ResendSmsOtpRequest smsRequest);
        BaseResponse VerifyOtpSms(VerifyOtpRequest smsRequest);
    }
}
