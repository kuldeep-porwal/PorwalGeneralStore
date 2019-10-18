using Moq;
using PorwalGeneralStore.BusinessLayer.Implementation.Sms;
using PorwalGeneralStore.BusinessLayer.Interface.Sms;
using PorwalGeneralStore.DataModel.Request.Sms;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Interface;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Implementation;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model;
using System;
using System.Collections.Generic;
using Xunit;

namespace XUnitSmsBizUnitTest
{
    public class SmsBizUnitTest
    {
        private readonly ISmsBiz _smsBiz;
        private readonly IMsg91 msg91;
        private readonly Msg91BulkSmsServiceConfiguration _msg91ServiceConfiguration;
        private readonly Mock<IHttpWebRequestHandler> _httpWebRequestHandler;
        public SmsBizUnitTest()
        {
            _msg91ServiceConfiguration = new Msg91BulkSmsServiceConfiguration()
            {
                AuthKey = "sandbox key",
                BaseApiUrl = "base api url",
                SendOtpApiUrl = "send otp url",
                SenderId = "Msg912"
            };
            _httpWebRequestHandler = new Mock<IHttpWebRequestHandler>();
            msg91 = new Msg91(_msg91ServiceConfiguration, _httpWebRequestHandler.Object);
            _smsBiz = new SmsBiz(msg91);
        }

        [Theory(DisplayName = "Validate ResendOtpSms method request")]
        [MemberData(nameof(InvalidResendOtpSmsRequestData))]
        public void UnitTest1(ResendSmsOtpRequest resendSmsOtpRequest)
        {
            var ActualResult = _smsBiz.ResendOtpSms(resendSmsOtpRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
        }

        [Theory(DisplayName = "Validate VerifyOtpSms method request")]
        [MemberData(nameof(InvalidVerifyOtpSmsRequestData))]
        public void UnitTest2(VerifyOtpRequest verifyOtpRequest)
        {
            var ActualResult = _smsBiz.VerifyOtpSms(verifyOtpRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
        }

        [Theory(DisplayName = "Validate SendOtpSms method request")]
        [MemberData(nameof(InvalidSendOtpSmsRequestData))]
        public void UnitTest3(SmsOtpRequest smsOtpRequest)
        {
            var ActualResult = _smsBiz.SendOtpSms(smsOtpRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
        }

        [Theory(DisplayName = "Validate SendOtpOnEmail method request")]
        [MemberData(nameof(InvalidSendOtpOnEmailRequestData))]
        public void UnitTest4(EmailOtpRequest emailOtpRequest)
        {
            var ActualResult = _smsBiz.SendOtpOnEmail(emailOtpRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
        }

        [Theory(DisplayName = "Validate SendBulkSms method request")]
        [MemberData(nameof(InvalidSendBulkSmsRequestData))]
        public void UnitTest5(BulkSmsApiRequest bulkSmsRequest)
        {
            var ActualResult = _smsBiz.SendBulkSms(bulkSmsRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
        }

        [Theory(DisplayName = "Validate SendSingleSms method request")]
        [MemberData(nameof(InvalidSendSingleSmsRequestData))]
        public void UnitTest6(SingleSmsRequest singleSmsRequest)
        {
            var ActualResult = _smsBiz.SendSingleSms(singleSmsRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
        }

        public static IEnumerable<object[]> InvalidResendOtpSmsRequestData =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { new ResendSmsOtpRequest() { } },
            new object[] { new ResendSmsOtpRequest() { mobile=0,retrytype=null} },
            new object[] { new ResendSmsOtpRequest() { mobile=-1,retrytype="Voice"} },
            new object[] { new ResendSmsOtpRequest() { mobile=-100,retrytype="SMS"} },
            };
        public static IEnumerable<object[]> InvalidSendBulkSmsRequestData =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { new BulkSmsApiRequest() { } },
            new object[] { new BulkSmsApiRequest() {sms=null } },
            new object[] { new BulkSmsApiRequest() {sms=new List<SmsRequestFormat>() } },
            new object[] { new BulkSmsApiRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message=null,to=null}
                    }
                }
            },
            new object[] { new BulkSmsApiRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message="",to=null}
                    }
                }
            },
            new object[] { new BulkSmsApiRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message="   ",to=null}
                    }
                }
            },
            new object[] { new BulkSmsApiRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message="kuldeep",to=new List<string>()}
                    }
                }
            },
            new object[] { new BulkSmsApiRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message=null,to=new List<string>(){ "","",""} }
                    }
                }
            },
            new object[] { new BulkSmsApiRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message=null,to=new List<string>(){ null,"","   "} }
                    }
                }
            },
        };
        public static IEnumerable<object[]> InvalidSendSingleSmsRequestData =>
        new List<object[]>
        {
            new object[] { null},
            new object[] { new SingleSmsRequest() {mobiles=null } },
            new object[] { new SingleSmsRequest() {mobiles="" } },
            new object[] { new SingleSmsRequest() {mobiles="  " } },
            new object[] { new SingleSmsRequest() {message=null } },
            new object[] { new SingleSmsRequest() {message = "" } },
            new object[] { new SingleSmsRequest() {message = "  " } },
            new object[] { new SingleSmsRequest() {route=null } },
            new object[] { new SingleSmsRequest() {route="" } },
            new object[] { new SingleSmsRequest() {route="  " } },
            new object[] { new SingleSmsRequest() {country=0 } },
            new object[] { new SingleSmsRequest() {country=-1 } },
            };
        public static IEnumerable<object[]> InvalidSendOtpOnEmailRequestData =>
                new List<object[]>
                {
            new object[] {null },
            new object[] { new EmailOtpRequest() { } },
            new object[] { new EmailOtpRequest() {email=null,otp="otp" } },
            new object[] { new EmailOtpRequest() {email="  ",otp="otp" } },
            new object[] { new EmailOtpRequest() {email="",otp="otp" } },
            new object[] { new EmailOtpRequest() {email="email",otp=null } },
            new object[] { new EmailOtpRequest() {email= "email", otp="" } },
            new object[] { new EmailOtpRequest() {email= "email", otp=" " } },
                    };
        public static IEnumerable<object[]> InvalidSendOtpSmsRequestData =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { new SmsOtpRequest() { } },
            new object[] { new SmsOtpRequest() {mobile=null } },
            new object[] { new SmsOtpRequest() {mobile=""} },
            new object[] { new SmsOtpRequest() {mobile="    ", } },
            new object[] { new SmsOtpRequest() {mobile="Valid NUmber",message=null } },
            new object[] { new SmsOtpRequest() {mobile="Valid NUmber",message="" } },
            new object[] { new SmsOtpRequest() {mobile="Valid NUmber",message="   " } },
            new object[] { new SmsOtpRequest() {mobile="Valid NUmber",message=string.Empty } },
      };
        public static IEnumerable<object[]> InvalidVerifyOtpSmsRequestData =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { new VerifyOtpRequest() {mobile=null,otp =null} },
            new object[] { new VerifyOtpRequest() {mobile="",otp =""} },
            new object[] { new VerifyOtpRequest() {mobile="    ",otp ="  "} },
            };
    }
}
