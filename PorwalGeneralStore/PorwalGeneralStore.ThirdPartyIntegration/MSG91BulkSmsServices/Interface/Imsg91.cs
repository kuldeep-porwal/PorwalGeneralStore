using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface
{
    public interface IMsg91
    {
        Msg91ApiResponse SendSingleSms(SingleSmsRequest smsRequest);
        Msg91ApiResponse SendBulkSms(BulkSmsRequest bulkSmsRequest);
        Msg91ApiResponse SendOtpOnEmail(EmailOtpRequest smsRequest);
        Msg91ApiResponse SendOtpSms(SmsOtpRequest smsRequest);
        Msg91ApiResponse ResendOtpSms(ResendSmsOtpRequest smsRequest);
        Msg91ApiResponse VerifyOtpSms(VerifyOtpRequest smsRequest);
    }
}
