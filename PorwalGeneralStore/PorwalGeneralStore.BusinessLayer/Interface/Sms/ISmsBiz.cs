using PorwalGeneralStore.DataModel.Request.Sms;
using PorwalGeneralStore.DataModel.Response.Sms;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Interface.Sms
{
    public interface ISmsBiz
    {
        SmsApiResponse SendSingleSms(SingleSmsRequest smsRequest);
        SmsApiResponse SendBulkSms(BulkSmsApiRequest bulkSmsRequest);
        SmsApiResponse SendOtpOnEmail(EmailOtpRequest smsRequest);
        SmsApiResponse SendOtpSms(SmsOtpRequest smsRequest);
        SmsApiResponse ResendOtpSms(ResendSmsOtpRequest smsRequest);
        SmsApiResponse VerifyOtpSms(VerifyOtpRequest smsRequest);
    }
}
