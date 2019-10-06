using Moq;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Interface;
using PorwalGeneralStore.HttpWebRequestClientLibrary.Model;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Implementation;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Interface;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Request;
using PorwalGeneralStore.ThirdPartyIntegration.MSG91BulkSmsServices.Model.Response;
using PorwalGeneralStore.Utility;
using System.Collections.Generic;
using System.Net;
using Xunit;

namespace XUnitThirdPartyIntegrationUnitTest.Msg91UnitTest
{
    public class Msg91SmsUtilityUnitTest
    {
        private readonly IMsg91 msg91;
        private readonly Msg91BulkSmsServiceConfiguration _msg91ServiceConfiguration;
        private readonly Mock<IHttpWebRequestHandler> _httpWebRequestHandler;
        public Msg91SmsUtilityUnitTest()
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
        }

        [Theory(DisplayName = "Validate ResendOtpSms method request")]
        [MemberData(nameof(InvalidResendOtpSmsRequestData))]
        public void UnitTest1(ResendSmsOtpRequest resendSmsOtpRequest)
        {
            var ActualResult = msg91.ResendOtpSms(resendSmsOtpRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Theory(DisplayName = "Validate VerifyOtpSms method request")]
        [MemberData(nameof(InvalidVerifyOtpSmsRequestData))]
        public void UnitTest2(VerifyOtpRequest verifyOtpRequest)
        {
            var ActualResult = msg91.VerifyOtpSms(verifyOtpRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Theory(DisplayName = "Validate SendOtpSms method request")]
        [MemberData(nameof(InvalidSendOtpSmsRequestData))]
        public void UnitTest3(SmsOtpRequest smsOtpRequest)
        {
            var ActualResult = msg91.SendOtpSms(smsOtpRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Theory(DisplayName = "Validate SendOtpOnEmail method request")]
        [MemberData(nameof(InvalidSendOtpOnEmailRequestData))]
        public void UnitTest4(EmailOtpRequest emailOtpRequest)
        {
            var ActualResult = msg91.SendOtpOnEmail(emailOtpRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Theory(DisplayName = "Validate SendBulkSms method request")]
        [MemberData(nameof(InvalidSendBulkSmsRequestData))]
        public void UnitTest5(BulkSmsRequest bulkSmsRequest)
        {
            var ActualResult = msg91.SendBulkSms(bulkSmsRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Theory(DisplayName = "Validate SendSingleSms method request")]
        [MemberData(nameof(InvalidSendSingleSmsRequestData))]
        public void UnitTest6(SingleSmsRequest singleSmsRequest)
        {
            var ActualResult = msg91.SendSingleSms(singleSmsRequest);
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Fact(DisplayName = "Send Otp Message to User Successful")]
        public void UnitTest7()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.SendOtpSms(new SmsOtpRequest() { mobile = "9876543214", otp = 123456, message = "temp Message" });
            Assert.NotNull(ActualResult);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Fact(DisplayName = "Send resend Otp Message to User Successful")]
        public void UnitTest8()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.ResendOtpSms(new ResendSmsOtpRequest() { mobile = 98765432148 });
            Assert.NotNull(ActualResult);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Fact(DisplayName = "verify Otp Message to User Successful")]
        public void UnitTest9()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.VerifyOtpSms(new VerifyOtpRequest() { mobile = "98765432148", otp = "123456" });
            Assert.NotNull(ActualResult);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Message));
        }
        [Fact(DisplayName = "Send email Otp Message to User Successful")]
        public void UnitTest10()
        {
            _httpWebRequestHandler
                     .Setup(
                             x => x.Post(
                                 It.IsAny<string>(),
                                 It.IsAny<string>(),
                                 It.IsAny<Dictionary<string, string>>(),
                                 It.IsAny<Dictionary<string, string>>())
                            )
                    .Returns(new BaseHttpWebResponse() { StatusCode = 200, Response = StringUtility.ConvertObjectToJson(new EmailOtpResponse() { msg = "3763646c3058373530393938", msg_type = "success" }), WebResponse = new HttpWebResponse() });
            var ActualResult = msg91.SendOtpOnEmail(new EmailOtpRequest() { email = "kuldeep@gmail.com", otp = "123456" });
            Assert.NotNull(ActualResult);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Message));
        }
        [Fact(DisplayName = "Send single Message to User Successful")]
        public void UnitTest11()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.SendSingleSms(new SingleSmsRequest() { mobiles = "98765432311", message = "TEMP mwssage 123456789", route = "1", country = 91 });
            Assert.NotNull(ActualResult);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Message));
        }
        [Fact(DisplayName = "Send bulk sms Message to User Successful")]
        public void UnitTest12()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.SendBulkSms(new BulkSmsRequest() { country = "91", route = "1", sms = new List<SmsRequestFormat>() { new SmsRequestFormat() { message = "test message", to = new List<string>() { "987654362331" } } } });
            Assert.NotNull(ActualResult);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Message));
        }
        [Fact(DisplayName = "Send Otp Message to User Successful")]
        public void UnitTest13()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.SendOtpSms(new SmsOtpRequest() { mobile = "9876543214", otp = 123456, message = "temp Message" });
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Fact(DisplayName = "Send resend Otp Message to User Successful")]
        public void UnitTest14()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.ResendOtpSms(new ResendSmsOtpRequest() { mobile = 98765432148 });
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Fact(DisplayName = "verify Otp Message to User Successful")]
        public void UnitTest15()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.VerifyOtpSms(new VerifyOtpRequest() { mobile = "98765432148", otp = "123456" });
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Fact(DisplayName = "Send email Otp Message to User Successful")]
        public void UnitTest16()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.SendOtpOnEmail(new EmailOtpRequest() { email = "kuldeep@gmail.com", otp = "123456" });
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Fact(DisplayName = "Send single Message to User Successful")]
        public void UnitTest17()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.SendSingleSms(new SingleSmsRequest() { mobiles = "98765432311", message = "TEMP mwssage 123456789", route = "1", country = 91 });
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
        }

        [Fact(DisplayName = "Send bulk sms Message to User Successful")]
        public void UnitTest18()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.SendBulkSms(new BulkSmsRequest() { country = "91", route = "1", });
            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
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
            new object[] { new BulkSmsRequest() { } },
            new object[] { new BulkSmsRequest() {sms=null } },
            new object[] { new BulkSmsRequest() {sms=new List<SmsRequestFormat>() } },
            new object[] { new BulkSmsRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message=null,to=null}
                    }
                }
            },
            new object[] { new BulkSmsRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message="",to=null}
                    }
                }
            },
            new object[] { new BulkSmsRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message="   ",to=null}
                    }
                }
            },
            new object[] { new BulkSmsRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message="kuldeep",to=new List<string>()}
                    }
                }
            },
            new object[] { new BulkSmsRequest()
                {
                    sms =new List<SmsRequestFormat>(){
                        new SmsRequestFormat(){ message=null,to=new List<string>(){ "","",""} }
                    }
                }
            },
            new object[] { new BulkSmsRequest()
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
        private void MockHttpSuccessResponse()
        {
            _httpWebRequestHandler
            .Setup(
                    x => x.Get(
                        It.IsAny<string>(),
                        It.IsAny<Dictionary<string, string>>(),
                        It.IsAny<Dictionary<string, string>>())
                   )
            .Returns(new BaseHttpWebResponse() { StatusCode = 200, Response = StringUtility.ConvertObjectToJson(new BaseResponse() { Message = "3763646c3058373530393938", Type = "success" }), WebResponse = new HttpWebResponse() });

            _httpWebRequestHandler
                     .Setup(
                             x => x.Post(
                                 It.IsAny<string>(),
                                 It.IsAny<string>(),
                                 It.IsAny<Dictionary<string, string>>(),
                                 It.IsAny<Dictionary<string, string>>())
                            )
                    .Returns(new BaseHttpWebResponse() { StatusCode = 200, Response = StringUtility.ConvertObjectToJson(new BaseResponse() { Message = "3763646c3058373530393938", Type = "success" }), WebResponse = new HttpWebResponse() });

            _httpWebRequestHandler
                         .Setup(
                                 x => x.Post(
                                     It.IsAny<string>(),
                                     It.IsAny<string>(),
                                     It.IsAny<Dictionary<string, string>>())
                                )
                        .Returns(new BaseHttpWebResponse() { StatusCode = 200, Response = StringUtility.ConvertObjectToJson(new BaseResponse() { Message = "3763646c3058373530393938", Type = "success" }), WebResponse = new HttpWebResponse() });
        }
        private void MockHttpFailResponse()
        {
            _httpWebRequestHandler
            .Setup(
                    x => x.Get(
                        It.IsAny<string>(),
                        It.IsAny<Dictionary<string, string>>(),
                        It.IsAny<Dictionary<string, string>>())
                   )
            .Returns(new BaseHttpWebResponse() { StatusCode = 400, Response = StringUtility.ConvertObjectToJson(new BaseResponse() { Message = "error while sending message", Type = "error" }), WebResponse = new HttpWebResponse() });

            _httpWebRequestHandler
                     .Setup(
                             x => x.Post(
                                 It.IsAny<string>(),
                                 It.IsAny<string>(),
                                 It.IsAny<Dictionary<string, string>>(),
                                 It.IsAny<Dictionary<string, string>>())
                            )
                    .Returns(new BaseHttpWebResponse() { StatusCode = 400, Response = StringUtility.ConvertObjectToJson(new BaseResponse() { Message = "error while sending message", Type = "error" }), WebResponse = new HttpWebResponse() });

            _httpWebRequestHandler
                         .Setup(
                                 x => x.Post(
                                     It.IsAny<string>(),
                                     It.IsAny<string>(),
                                     It.IsAny<Dictionary<string, string>>())
                                )
                        .Returns(new BaseHttpWebResponse() { StatusCode = 400, Response = StringUtility.ConvertObjectToJson(new BaseResponse() { Message = "error while sending message", Type = "error" }), WebResponse = new HttpWebResponse() });
        }
    }
}