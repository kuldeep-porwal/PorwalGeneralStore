using PorwalGeneralStore.HttpWebRequestClientLibrary.Interface;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Model;
using System.Linq;
using PorwalGeneralStore.Utility;

namespace PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Implementation
{
    public class Msg91 : IMsg91
    {
        private readonly Msg91BulkSmsServiceConfiguration _msg91ServiceConfiguration;
        private readonly IHttpWebRequestHandler _httpWebRequestHandler;
        public Msg91(Msg91BulkSmsServiceConfiguration msg91ServiceConfiguration, IHttpWebRequestHandler httpWebRequestHandler)
        {
            _msg91ServiceConfiguration = msg91ServiceConfiguration;
            _httpWebRequestHandler = httpWebRequestHandler;
        }

        public Msg91ApiResponse ResendOtpSms(ResendSmsOtpRequest smsRequest)
        {
            Msg91ApiResponse msg91ApiResponse = new Msg91ApiResponse() { StatusCode = 200 };
            string url = _msg91ServiceConfiguration.SendOtpApiUrl + Msg91Constant.RESEND_OTP_SMS_URL;

            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            requestHeader.Add("Content-Type", "application/x-www-form-urlencoded");

            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            queryParameter.Add("authkey", _msg91ServiceConfiguration.AuthKey);
            queryParameter.Add("mobile", smsRequest.mobile.ToString());
            if (!string.IsNullOrWhiteSpace(smsRequest.retrytype))
            {
                queryParameter.Add("retrytype", smsRequest.retrytype);
            }

            BaseHttpWebResponse httpWebResponse = _httpWebRequestHandler.Post(url, null, requestHeader, queryParameter);
            if (httpWebResponse != null)
            {
                BaseResponse msg91response = StringUtility.ConvertJsonToObject<BaseResponse>(httpWebResponse.Response);
                msg91ApiResponse.StatusCode = httpWebResponse.StatusCode;
                msg91ApiResponse.MessageResponse = msg91response;
            }
            return msg91ApiResponse;
        }

        public Msg91ApiResponse SendBulkSms(BulkSmsRequest bulkSmsRequest)
        {
            Msg91ApiResponse msg91ApiResponse = new Msg91ApiResponse() { StatusCode = 200 };
            string url = _msg91ServiceConfiguration.BaseApiUrl + Msg91Constant.SEND_BULK_SMS_URL;

            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            requestHeader.Add("Content-Type", "application/x-www-form-urlencoded");
            requestHeader.Add("authkey", _msg91ServiceConfiguration.AuthKey);

            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            queryParameter.Add("country", bulkSmsRequest.country);

            bulkSmsRequest.sender = _msg91ServiceConfiguration.SenderId;

            string postData = StringUtility.ConvertObjectToJson(bulkSmsRequest);
            BaseHttpWebResponse httpWebResponse = _httpWebRequestHandler.Post(url, postData, requestHeader, queryParameter);
            if (httpWebResponse != null)
            {
                BaseResponse msg91response = StringUtility.ConvertJsonToObject<BaseResponse>(httpWebResponse.Response);
                msg91ApiResponse.StatusCode = httpWebResponse.StatusCode;
                msg91ApiResponse.MessageResponse = msg91response;
            }
            return msg91ApiResponse;
        }

        public Msg91ApiResponse SendOtpOnEmail(EmailOtpRequest smsRequest)
        {
            Msg91ApiResponse msg91ApiResponse = new Msg91ApiResponse() { StatusCode = 200 };
            string url = _msg91ServiceConfiguration.SendOtpApiUrl + Msg91Constant.SEND_OTP_ON_EMAIL_URL;

            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            requestHeader.Add("Content-Type", "application/x-www-form-urlencoded");

            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            queryParameter.Add("authkey", _msg91ServiceConfiguration.AuthKey);
            queryParameter.Add("email", smsRequest.email);
            queryParameter.Add("otp", smsRequest.otp);

            BaseHttpWebResponse httpWebResponse = _httpWebRequestHandler.Post(url, null, requestHeader, queryParameter);
            if (httpWebResponse != null)
            {
                BaseResponse msg91response = StringUtility.ConvertJsonToObject<BaseResponse>(httpWebResponse.Response);
                msg91ApiResponse.StatusCode = httpWebResponse.StatusCode;
                msg91ApiResponse.MessageResponse = msg91response;
            }
            return msg91ApiResponse;
        }

        public Msg91ApiResponse SendOtpSms(SmsOtpRequest smsRequest)
        {
            Msg91ApiResponse msg91ApiResponse = new Msg91ApiResponse() { StatusCode = 200 };
            string url = _msg91ServiceConfiguration.SendOtpApiUrl + Msg91Constant.SEND_OTP_SMS_URL;

            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            requestHeader.Add("Content-Type", "application/x-www-form-urlencoded");

            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            queryParameter.Add("authkey", _msg91ServiceConfiguration.AuthKey);
            queryParameter.Add("sender", smsRequest.sender);
            queryParameter.Add("mobile", smsRequest.mobile.ToString());
            queryParameter.Add("message", smsRequest.message);
            queryParameter.Add("otp", smsRequest.otp.ToString());

            if (!string.IsNullOrWhiteSpace(smsRequest.email))
            {
                queryParameter.Add("email", smsRequest.email);
            }
            if (smsRequest.otp_expiry > 0)
            {
                queryParameter.Add("otp_expiry", smsRequest.otp_expiry.ToString());
            }
            if (smsRequest.otp_length > 0)
            {
                queryParameter.Add("otp_length", smsRequest.otp_length.ToString());
            }
            if (smsRequest.template > 0)
            {
                queryParameter.Add("template", smsRequest.template.ToString());
            }

            BaseHttpWebResponse httpWebResponse = _httpWebRequestHandler.Post(url, null, requestHeader, queryParameter);
            if (httpWebResponse != null)
            {
                BaseResponse msg91response = StringUtility.ConvertJsonToObject<BaseResponse>(httpWebResponse.Response);
                msg91ApiResponse.StatusCode = httpWebResponse.StatusCode;
                msg91ApiResponse.MessageResponse = msg91response;
            }
            return msg91ApiResponse;
        }

        public Msg91ApiResponse SendSingleSms(SingleSmsRequest smsRequest)
        {

            Msg91ApiResponse msg91ApiResponse = new Msg91ApiResponse() { StatusCode = 200 };
            string url = _msg91ServiceConfiguration.BaseApiUrl + Msg91Constant.SEND_SINGLE_SMS_URL;

            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            requestHeader.Add("Content-Type", "application/x-www-form-urlencoded");

            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            queryParameter.Add("authkey", _msg91ServiceConfiguration.AuthKey);
            queryParameter.Add("sender", _msg91ServiceConfiguration.SenderId);
            queryParameter.Add("message", smsRequest.message);
            queryParameter.Add("mobiles", smsRequest.mobiles);
            queryParameter.Add("route", smsRequest.route);
            queryParameter.Add("country", smsRequest.country.ToString());
            queryParameter.Add("response", "json");

            if (smsRequest.afterminutes > 0)
            {
                queryParameter.Add("afterminutes", smsRequest.afterminutes.ToString());
            }
            if (!string.IsNullOrWhiteSpace(smsRequest.campaign))
            {
                queryParameter.Add("campaign", smsRequest.campaign);
            }
            if (smsRequest.schtime != null && smsRequest.schtime != DateTime.MinValue)
            {
                queryParameter.Add("schtime", Convert.ToString(smsRequest.schtime));
            }
            if (smsRequest.flash != null)
            {
                queryParameter.Add("flash", smsRequest.flash.Value ? "1" : "0");
            }
            if (smsRequest.unicode != null)
            {
                queryParameter.Add("unicode", smsRequest.unicode.Value ? "1" : "0");
            }

            BaseHttpWebResponse httpWebResponse = _httpWebRequestHandler.Post(url, null, requestHeader, queryParameter);
            if (httpWebResponse != null)
            {
                BaseResponse msg91response = StringUtility.ConvertJsonToObject<BaseResponse>(httpWebResponse.Response);
                msg91ApiResponse.StatusCode = httpWebResponse.StatusCode;
                msg91ApiResponse.MessageResponse = msg91response;
            }
            return msg91ApiResponse;
        }

        public Msg91ApiResponse VerifyOtpSms(VerifyOtpRequest smsRequest)
        {
            Msg91ApiResponse msg91ApiResponse = new Msg91ApiResponse() { StatusCode = 200 };
            string url = _msg91ServiceConfiguration.SendOtpApiUrl + Msg91Constant.VERIFYREQUESTOTP_OTP_SMS_URL;

            Dictionary<string, string> requestHeader = new Dictionary<string, string>();
            requestHeader.Add("Content-Type", "application/x-www-form-urlencoded");

            Dictionary<string, string> queryParameter = new Dictionary<string, string>();
            queryParameter.Add("authkey", _msg91ServiceConfiguration.AuthKey);
            queryParameter.Add("mobile", smsRequest.mobile);
            queryParameter.Add("otp", smsRequest.otp);

            BaseHttpWebResponse httpWebResponse = _httpWebRequestHandler.Post(url, null, requestHeader, queryParameter);
            if (httpWebResponse != null)
            {
                BaseResponse msg91response = StringUtility.ConvertJsonToObject<BaseResponse>(httpWebResponse.Response);
                msg91ApiResponse.StatusCode = httpWebResponse.StatusCode;
                msg91ApiResponse.MessageResponse = msg91response;
            }
            return msg91ApiResponse;
        }
    }
}
