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

        [Fact(DisplayName = "Send Otp Message to User Successful")]
        public void UnitTest7()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.SendOtpSms(new Msg91SmsOtpRequest() { mobile = "9876543214", otp = 123456, template_id = "temp Message" });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
        }

        [Fact(DisplayName = "Send resend Otp Message to User Successful")]
        public void UnitTest8()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.ResendOtpSms(new Msg91ResendSmsOtpRequest() { mobile = 98765432148 });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
        }

        [Fact(DisplayName = "verify Otp Message to User Successful")]
        public void UnitTest9()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.VerifyOtpSms(new Msg91VerifyOtpRequest() { mobile = "98765432148", otp = "123456" });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
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
            var ActualResult = msg91.SendOtpOnEmail(new Msg91EmailOtpRequest() { email = "kuldeep@gmail.com", otp = "123456" });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
        }

        [Fact(DisplayName = "Send single Message to User Successful")]
        public void UnitTest11()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.SendSingleSms(new Msg91SingleSmsRequest() { mobiles = "98765432311", message = "TEMP mwssage 123456789", route = "1", country = 91 });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
        }

        [Fact(DisplayName = "Send bulk sms Message to User Successful")]
        public void UnitTest12()
        {
            MockHttpSuccessResponse();
            var ActualResult = msg91.SendBulkSms(new Msg91BulkSmsRequest() { country = "91", route = "1", sms = new List<Msg91SmsRequestFormat>() { new Msg91SmsRequestFormat() { message = "test message", to = new List<string>() { "987654362331" } } } });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);

        }

        [Fact(DisplayName = "Send Otp Message to User Successful")]
        public void UnitTest13()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.SendOtpSms(new Msg91SmsOtpRequest() { mobile = "9876543214", otp = 123456, template_id = "temp Message" });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
        }

        [Fact(DisplayName = "Send resend Otp Message to User Successful")]
        public void UnitTest14()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.ResendOtpSms(new Msg91ResendSmsOtpRequest() { mobile = 98765432148 });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
        }

        [Fact(DisplayName = "verify Otp Message to User Successful")]
        public void UnitTest15()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.VerifyOtpSms(new Msg91VerifyOtpRequest() { mobile = "98765432148", otp = "123456" });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
        }

        [Fact(DisplayName = "Send email Otp Message to User Successful")]
        public void UnitTest16()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.SendOtpOnEmail(new Msg91EmailOtpRequest() { email = "kuldeep@gmail.com", otp = "123456" });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
        }

        [Fact(DisplayName = "Send single Message to User Successful")]
        public void UnitTest17()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.SendSingleSms(new Msg91SingleSmsRequest() { mobiles = "98765432311", message = "TEMP mwssage 123456789", route = "1", country = 91 });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
        }

        [Fact(DisplayName = "Send bulk sms Message to User Successful")]
        public void UnitTest18()
        {
            MockHttpFailResponse();
            var ActualResult = msg91.SendBulkSms(new Msg91BulkSmsRequest() { country = "91", route = "1", });
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
        }

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