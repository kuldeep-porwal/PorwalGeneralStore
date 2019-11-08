using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface
{
    public interface IMsg91
    {
        Msg91ApiResponse SendSingleSms(Msg91SingleSmsRequest smsRequest);
        Msg91ApiResponse SendBulkSms(Msg91BulkSmsRequest bulkSmsRequest);
        Msg91ApiResponse SendOtpOnEmail(Msg91EmailOtpRequest smsRequest);
        Msg91ApiResponse SendOtpSms(Msg91SmsOtpRequest smsRequest);
        Msg91ApiResponse ResendOtpSms(Msg91ResendSmsOtpRequest smsRequest);
        Msg91ApiResponse VerifyOtpSms(Msg91VerifyOtpRequest smsRequest);
    }
}
