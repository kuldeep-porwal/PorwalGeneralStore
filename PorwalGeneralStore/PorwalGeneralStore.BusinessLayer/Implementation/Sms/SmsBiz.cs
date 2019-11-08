﻿using PorwalGeneralStore.BusinessLayer.Interface.Sms;
using PorwalGeneralStore.DataModel.Request.Sms;
using PorwalGeneralStore.DataModel.Response.Sms;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Implementation.Sms
{
    public class SmsBiz : ISmsBiz
    {
        private readonly IMsg91 _msg91;
        public SmsBiz(IMsg91 msg91)
        {
            _msg91 = msg91;
        }
        public SmsApiResponse ResendOtpSms(ResendSmsOtpRequest smsRequest)
        {
            SmsApiResponse smsApiResponse = new SmsApiResponse() { StatusCode = 200 };
            if (smsRequest != null)
            {
                if (smsRequest.mobile <= 0)
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.mobile),
                            Message=nameof(smsRequest.mobile)+" is required."
                        }
                    };
                }

                //Msg91ApiResponse msg91ApiResponse = _msg91.ResendOtpSms(smsRequest);
            }
            else
            {
                smsApiResponse.StatusCode = 400;
                smsApiResponse.ErrorList = new List<SmsApiValidationResponse>() {
                    new SmsApiValidationResponse()
                    {
                        Code=1001,
                        FieldName=nameof(smsRequest),
                        Message="Request Data is Invlid."
                    }
                };
            }
            return smsApiResponse;
        }

        public SmsApiResponse SendBulkSms(BulkSmsApiRequest bulkSmsRequest)
        {
            SmsApiResponse smsApiResponse = new SmsApiResponse() { StatusCode = 200 };
            if (bulkSmsRequest != null)
            {
                if (bulkSmsRequest.sms == null || bulkSmsRequest.sms.Count == 0)
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(bulkSmsRequest.sms),
                            Message=nameof(bulkSmsRequest.sms)+" is required and can't be blank."
                        }
                    };
                    return smsApiResponse;
                }
                if (bulkSmsRequest.sms.Any(x => x.to == null || x.to.Count == 0))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName="to",
                            Message=nameof(bulkSmsRequest.sms)+" is required and can't be blank and should be a valid mobile number."
                        }
                    };
                    return smsApiResponse;
                }
                if (bulkSmsRequest.sms.Any(x => string.IsNullOrWhiteSpace(x.message)))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName="message",
                            Message=nameof(bulkSmsRequest.sms)+" is required and can't be blank."
                        }
                    };
                    return smsApiResponse;
                }
            }
            else
            {
                smsApiResponse.StatusCode = 400;
                smsApiResponse.ErrorList = new List<SmsApiValidationResponse>() {
                    new SmsApiValidationResponse()
                    {
                        Code=1001,
                        FieldName=nameof(bulkSmsRequest),
                        Message="Request Data is Invlid."
                    }
                };
            }
            return smsApiResponse;
        }

        public SmsApiResponse SendOtpOnEmail(EmailOtpRequest smsRequest)
        {
            SmsApiResponse smsApiResponse = new SmsApiResponse() { StatusCode = 200 };
            if (smsRequest != null)
            {
                if (string.IsNullOrWhiteSpace(smsRequest.email))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.email),
                            Message=nameof(smsRequest.email)+" is required."
                        }
                    };
                    return smsApiResponse;
                }
                if (string.IsNullOrWhiteSpace(smsRequest.otp))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.otp),
                            Message=nameof(smsRequest.otp)+" is required."
                        }
                    };
                    return smsApiResponse;
                }
            }
            else
            {
                smsApiResponse.StatusCode = 400;
                smsApiResponse.ErrorList = new List<SmsApiValidationResponse>() {
                    new SmsApiValidationResponse()
                    {
                        Code=1001,
                        FieldName=nameof(smsRequest),
                        Message="Request Data is Invlid."
                    }
                };
            }
            return smsApiResponse;
        }

        public SmsApiResponse SendOtpSms(SmsOtpRequest smsRequest)
        {
            SmsApiResponse smsApiResponse = new SmsApiResponse() { StatusCode = 200 };
            if (smsRequest != null)
            {
                if (string.IsNullOrWhiteSpace(smsRequest.Mobile))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.Mobile),
                            Message=nameof(smsRequest.Mobile)+" is required."
                        }
                    };
                    return smsApiResponse;
                }
                if (string.IsNullOrWhiteSpace(smsRequest.CountryCode))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.CountryCode),
                            Message=nameof(smsRequest.CountryCode)+" is required."
                        }
                    };
                    return smsApiResponse;
                }

                Msg91SmsOtpRequest smsOtpRequest = new Msg91SmsOtpRequest()
                {
                    mobile = smsRequest.Mobile,
                    message = "Otp Send to User",
                    otp = 12345
                };
                _msg91.SendOtpSms(smsOtpRequest);
            }
            else
            {
                smsApiResponse.StatusCode = 400;
                smsApiResponse.ErrorList = new List<SmsApiValidationResponse>() {
                    new SmsApiValidationResponse()
                    {
                        Code=1001,
                        FieldName=nameof(smsRequest),
                        Message="Request Data is Invlid."
                    }
                };
            }
            return smsApiResponse;
        }

        public SmsApiResponse SendSingleSms(SingleSmsRequest smsRequest)
        {

            SmsApiResponse smsApiResponse = new SmsApiResponse() { StatusCode = 200 };
            if (smsRequest != null)
            {
                if (string.IsNullOrWhiteSpace(smsRequest.Mobiles))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.Mobiles),
                            Message=nameof(smsRequest.Mobiles)+" is required."
                        }
                    };
                    return smsApiResponse;
                }
                if (string.IsNullOrWhiteSpace(smsRequest.Route))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.Route),
                            Message=nameof(smsRequest.Route)+" is required."
                        }
                    };
                    return smsApiResponse;
                }
                if (smsRequest.Country <= 0)
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.Route),
                            Message=nameof(smsRequest.Route)+" is required."
                        }
                    };
                    return smsApiResponse;
                }
            }
            else
            {
                smsApiResponse.StatusCode = 400;
                smsApiResponse.ErrorList = new List<SmsApiValidationResponse>() {
                    new SmsApiValidationResponse()
                    {
                        Code=1001,
                        FieldName=nameof(smsRequest),
                        Message="Request Data is Invlid."
                    }
                };
            }
            return smsApiResponse;
        }

        public SmsApiResponse VerifyOtpSms(VerifyOtpRequest smsRequest)
        {
            SmsApiResponse smsApiResponse = new SmsApiResponse() { StatusCode = 200 };
            if (smsRequest != null)
            {
                if (string.IsNullOrWhiteSpace(smsRequest.mobile))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.mobile),
                            Message=nameof(smsRequest.mobile)+" is required."
                        }
                    };
                    return smsApiResponse;
                }

                if (string.IsNullOrWhiteSpace(smsRequest.otp))
                {
                    smsApiResponse.StatusCode = 400;
                    smsApiResponse.ErrorList = new List<SmsApiValidationResponse>()
                    {
                        new SmsApiValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(smsRequest.otp),
                            Message=nameof(smsRequest.otp)+" is required."
                        }
                    };
                    return smsApiResponse;
                }
            }
            else
            {
                smsApiResponse.StatusCode = 400;
                smsApiResponse.ErrorList = new List<SmsApiValidationResponse>() {
                    new SmsApiValidationResponse()
                    {
                        Code=1001,
                        FieldName=nameof(smsRequest),
                        Message="Request Data is Invlid."
                    }
                };
            }
            return smsApiResponse;
        }
    }
}
