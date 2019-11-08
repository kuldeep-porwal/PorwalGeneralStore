using Moq;
using PorwalGeneralStore.BusinessLayer.Interface.Sms;
using PorwalGeneralStore.BusinessLayer.Interface.Users;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Sms;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.DataModel.Response.Sms;
using PorwalGeneralStore.Utility.JWTTokenGenerator;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace XUnitTestUserBizUnitTest
{
    public class UserBizUnitTest
    {
        private readonly IUserBiz _userBiz;
        private readonly Mock<IUserLayer> _userLayer;
        private readonly Mock<IJwtBuilder> _jwtBuilder;
        private readonly Mock<ISmsBiz> _smsBiz;
        public UserBizUnitTest()
        {
            _userLayer = new Mock<IUserLayer>();
            _jwtBuilder = new Mock<IJwtBuilder>();
            _smsBiz = new Mock<ISmsBiz>();
            _userBiz = new UserBiz(_userLayer.Object, _jwtBuilder.Object, _smsBiz.Object);
        }

        [Fact(DisplayName = "Business -: null Object Error ")]
        public void UnitTest1()
        {
            var ActualResult = _userBiz.AuthenticateUser(null);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid userName ")]
        [InlineData(null, null)]
        public void UnitTest2(string userName, string password)
        {
            var ActualResult = _userBiz.AuthenticateUser(new LoginForm()
            {
                UserName = userName,
                Password = password
            });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid password ")]
        [InlineData("kuldeep", null)]
        public void UnitTest3(string userName, string password)
        {
            var ActualResult = _userBiz.AuthenticateUser(new LoginForm()
            {
                UserName = userName,
                Password = password
            });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.First().Code == 1001);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "Business -: Handling Exception in AuthenticateUser")]
        public void UnitTest4()
        {
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Throws(new Exception());

            var exception = Record.Exception(() =>
            {
                _userBiz.AuthenticateUser(new LoginForm()
                {
                    UserName = "7894561230",
                    Password = "Porwal"
                });
            });

            Assert.Null(exception);
        }

        [Theory(DisplayName = "Business -: Validation Message When UserIformation is not available")]
        [InlineData("kuldeep", "Password")]
        public void UnitTest5(string userName, string password)
        {
            var ActualResult = _userBiz.AuthenticateUser(new LoginForm()
            {
                UserName = userName,
                Password = password
            });

            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.Null(ActualResult.Response);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Get Correct Response Object")]
        [InlineData("7894563210", "Password")]
        public void UnitTest6(string userName, string password)
        {
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1, CustomerName = "Test Customer", City = "Test City", FirstName = "Test FirstName", LastName = "Test LastName", Phone = "987654321" });
            _jwtBuilder
                .Setup(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
                .Returns(new JwtTokenResponse()
                {
                    StatusCode = 200,
                    ErrorList = null,
                    TokenDetail = new JwtToken()
                    {
                        Type = "Bearer",
                        Value = "7895643215rewrf1sd2f1sd3r15efs3d21f5rr1fsd23f13sd21f32sd1f32",
                        CreatedAt = DateTime.UtcNow
                    }
                });
            var ActualResult = _userBiz.AuthenticateUser(new LoginForm()
            {
                UserName = userName,
                Password = password
            });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.NotNull(ActualResult.Response);
            Assert.Null(ActualResult.ErrorList);
        }

        [Theory(DisplayName = "Business -: Get Valid user Id")]
        [InlineData("7894563210", "Password")]
        public void UnitTest7(string userName, string password)
        {
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1, CustomerName = "Test Customer", City = "Test City", FirstName = "Test FirstName", LastName = "Test LastName", Phone = "987654321" });
            _jwtBuilder
                            .Setup(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
                            .Returns(new JwtTokenResponse()
                            {
                                StatusCode = 200,
                                ErrorList = null,
                                TokenDetail = new JwtToken()
                                {
                                    Type = "Bearer",
                                    Value = "7895643215rewrf1sd2f1sd3r15efs3d21f5rr1fsd23f13sd21f32sd1f32",
                                    CreatedAt = DateTime.UtcNow
                                }
                            });

            var ActualResult = _userBiz.AuthenticateUser(new LoginForm()
            {
                UserName = userName,
                Password = password
            });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.NotNull(ActualResult.Response);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.Response.UserId == 1);
        }

        [Theory(DisplayName = "Business -: Get Valid user Id and Blank Token Response")]
        [InlineData("7894563210", "Password")]
        public void UnitTest8(string userName, string password)
        {
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1, CustomerName = "Test Customer", City = "Test City", FirstName = "Test FirstName", LastName = "Test LastName", Phone = "987654321" });
            _jwtBuilder
               .Setup(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
                .Returns(new JwtTokenResponse()
                {
                    StatusCode = 200,
                    ErrorList = null,
                    TokenDetail = new JwtToken()
                    {
                        Type = "Bearer",
                        Value = "7895643215rewrf1sd2f1sd3r15efs3d21f5rr1fsd23f13sd21f32sd1f32",
                        CreatedAt = DateTime.UtcNow
                    }
                });

            var ActualResult = _userBiz.AuthenticateUser(new LoginForm()
            {
                UserName = userName,
                Password = password
            });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.NotNull(ActualResult.Response);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.Response.UserId == 1);
            Assert.NotNull(ActualResult.Response.TokenDetail);
        }

        [Theory(DisplayName = "Business -: Get Valid user Id and Valid Token Response")]
        [InlineData("7894563210", "Password")]
        public void UnitTest9(string userName, string password)
        {
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1, CustomerName = "Test Customer", City = "Test City", FirstName = "Test FirstName", LastName = "Test LastName", Phone = "987654321" });
            _jwtBuilder
                .Setup(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
                .Returns(new JwtTokenResponse()
                {
                    StatusCode = 200,
                    ErrorList = null,
                    TokenDetail = new JwtToken()
                    {
                        Type = "Bearer",
                        Value = "7895643215rewrf1sd2f1sd3r15efs3d21f5rr1fsd23f13sd21f32sd1f32",
                        CreatedAt = DateTime.UtcNow
                    }
                });
            var ActualResult = _userBiz.AuthenticateUser(new LoginForm()
            {
                UserName = userName,
                Password = password
            });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.NotNull(ActualResult.Response);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.Response.UserId == 1);
            Assert.NotNull(ActualResult.Response.TokenDetail);
            Assert.True(!string.IsNullOrEmpty(ActualResult.Response.TokenDetail.Type));
            Assert.True(!string.IsNullOrEmpty(ActualResult.Response.TokenDetail.Value));
            Assert.True(ActualResult.Response.TokenDetail.CreatedAt != DateTime.MinValue);
        }

        [Theory(DisplayName = "Business -: Invalid MobileNumber Validation Error Should Come")]
        [InlineData("Invalid Mobile Number", "Password")]
        [InlineData("", "Password")]
        [InlineData(" ", "Password")]
        [InlineData(null, "Password")]
        [InlineData("12345667", "Password")]
        [InlineData("123456dd", "Password")]
        [InlineData("1234567890q", "Password")]
        [InlineData("123456789098", "Password")]
        [InlineData("fd4454dr4red", "Password")]
        public void UnitTest10(string userName, string password)
        {
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1, CustomerName = "Test Customer", City = "Test City", FirstName = "Test FirstName", LastName = "Test LastName", Phone = "987654321" });
            _jwtBuilder
                .Setup(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
                .Returns(new JwtTokenResponse()
                {
                    StatusCode = 200,
                    ErrorList = null,
                    TokenDetail = new JwtToken()
                    {
                        Type = "Bearer",
                        Value = "7895643215rewrf1sd2f1sd3r15efs3d21f5rr1fsd23f13sd21f32sd1f32",
                        CreatedAt = DateTime.UtcNow
                    }
                });

            var ActualResult = _userBiz.AuthenticateUser(new LoginForm() { UserName = userName, Password = password });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid MobileNumber Validation Error Should Come")]
        [InlineData("Invalid Mobile Number")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("12345667")]
        [InlineData("123456dd")]
        [InlineData("1234567890q")]
        [InlineData("123456789098")]
        [InlineData("fd4454dr4red")]
        public void UnitTest11(string userName)
        {
            var ActualResult = _userBiz.AuthenticateUserByMobileNumber(new OtpLoginForm()
            {
                MobileNumber = userName,
                Otp = "Fake Otp"
            });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid Otp Validation Error Should Come")]
        [InlineData("Invalid Otp")]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        [InlineData("12345667")]
        [InlineData("123456dd")]
        [InlineData("1234567890q")]
        [InlineData("123456789098")]
        [InlineData("fd4454dr4red")]
        public void UnitTest12(string otp)
        {
            var ActualResult = _userBiz.AuthenticateUserByMobileNumber(new OtpLoginForm()
            {
                MobileNumber = "7894563210",
                Otp = otp
            });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Otp Varification Successfull Message Should Come.")]
        [InlineData("1234566712", "Password")]
        public void UnitTest13(string mobileNumber, string otp)
        {
            _smsBiz.Setup(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>())).Returns(new SmsApiResponse()
            {
                StatusCode = 200,
                Response = new SmsResponse() { Message = "Suceess" }
            });

            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(true);
            _userLayer.Setup(x => x.GetUserDetailByMobileNumber(It.IsAny<string>())).Returns(new UserInformation() { UserId = 1, CustomerName = "Test Customer", City = "Test City", FirstName = "Test FirstName", LastName = "Test LastName", Phone = "987654321" });

            _jwtBuilder
                .Setup(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
                .Returns(new JwtTokenResponse()
                {
                    StatusCode = 200,
                    ErrorList = null,
                    TokenDetail = new JwtToken()
                    {
                        Type = "Bearer",
                        Value = "7895643215rewrf1sd2f1sd3r15efs3d21f5rr1fsd23f13sd21f32sd1f32",
                        CreatedAt = DateTime.UtcNow
                    }
                });

            var ActualResult = _userBiz.AuthenticateUserByMobileNumber(new OtpLoginForm()
            {
                MobileNumber = mobileNumber,
                Otp = otp,
                CountryCode = 91
            });

            _smsBiz.Verify(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>()), Times.Once);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.Null(ActualResult.ErrorList);
            Assert.NotNull(ActualResult.Response);

        }

        [Theory(DisplayName = "Business -: Otp Varification UnSuccessfull Validation Message Should Come.")]
        [InlineData("1234566711", "Password")]
        public void UnitTest14(string mobileNumber, string otp)
        {
            _smsBiz.Setup(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>())).Returns(new SmsApiResponse()
            {
                StatusCode = 400,
                Response = new SmsResponse() { Message = "Error" }
            });

            var ActualResult = _userBiz.AuthenticateUserByMobileNumber(new OtpLoginForm()
            {
                MobileNumber = mobileNumber,
                Otp = otp
            });

            _smsBiz.Verify(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>()), Times.Never);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Mobile Number Verification SuccessFull.")]
        [InlineData("1234566711", "Otp")]
        public void UnitTest15(string mobileNumber, string otp)
        {
            _smsBiz.Setup(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>())).Returns(new SmsApiResponse()
            {
                StatusCode = 200,
                Response = new SmsResponse() { Message = "Suceess" }
            });

            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(true);
            _userLayer.Setup(x => x.GetUserDetailByMobileNumber(It.IsAny<string>())).Returns(new UserInformation() { UserId = 1, CustomerName = "Test Customer", City = "Test City", FirstName = "Test FirstName", LastName = "Test LastName", Phone = "987654321" });

            _jwtBuilder
                .Setup(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
                .Returns(new JwtTokenResponse()
                {
                    StatusCode = 200,
                    ErrorList = null,
                    TokenDetail = new JwtToken()
                    {
                        Type = "Bearer",
                        Value = "7895643215rewrf1sd2f1sd3r15efs3d21f5rr1fsd23f13sd21f32sd1f32",
                        CreatedAt = DateTime.UtcNow
                    }
                });

            var ActualResult = _userBiz.AuthenticateUserByMobileNumber(new OtpLoginForm()
            {
                MobileNumber = mobileNumber,
                Otp = otp,
                CountryCode=91
            });

            _userLayer.Verify(x => x.isExistPhoneNumber(It.IsAny<string>()), Times.Once);
            _smsBiz.Verify(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>()), Times.Once);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.Null(ActualResult.ErrorList);
            Assert.NotNull(ActualResult.Response);

        }

        [Theory(DisplayName = "Business -: Mobile Number Verification UnSuccessFull.")]
        [InlineData("1234566127", "Password")]
        public void UnitTest16(string mobileNumber, string otp)
        {
            _smsBiz.Setup(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>())).Returns(new SmsApiResponse()
            {
                StatusCode = 200,
                Response = new SmsResponse() { Message = "Suceess" }
            });

            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(false);

            var ActualResult = _userBiz.AuthenticateUserByMobileNumber(new OtpLoginForm()
            {
                MobileNumber = mobileNumber,
                Otp = otp,
                CountryCode = 91
            });

            _userLayer.Verify(x => x.isExistPhoneNumber(It.IsAny<string>()), Times.Once);
            _smsBiz.Verify(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>()), Times.Never);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Generate Jwt Token for Mobile Number.")]
        [InlineData("1234566711", "Otp")]
        public void UnitTest17(string mobileNumber, string otp)
        {
            _smsBiz.Setup(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>())).Returns(new SmsApiResponse()
            {
                StatusCode = 200,
                Response = new SmsResponse() { Message = "Suceess" }
            });

            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(true);
            _userLayer.Setup(x => x.GetUserDetailByMobileNumber(It.IsAny<string>())).Returns(new UserInformation() { UserId = 1, CustomerName = "Test Customer", City = "Test City", FirstName = "Test FirstName", LastName = "Test LastName", Phone = "987654321" });

            _jwtBuilder
                .Setup(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()))
                .Returns(new JwtTokenResponse()
                {
                    StatusCode = 200,
                    ErrorList = null,
                    TokenDetail = new JwtToken()
                    {
                        Type = "Bearer",
                        Value = "7895643215rewrf1sd2f1sd3r15efs3d21f5rr1fsd23f13sd21f32sd1f32",
                        CreatedAt = DateTime.UtcNow
                    }
                });

            var ActualResult = _userBiz.AuthenticateUserByMobileNumber(new OtpLoginForm()
            {
                MobileNumber = mobileNumber,
                Otp = otp,
                CountryCode=91
            });

            _userLayer.Verify(x => x.isExistPhoneNumber(It.IsAny<string>()), Times.Once);
            _smsBiz.Verify(x => x.VerifyOtpSms(It.IsAny<VerifyOtpRequest>()), Times.Once);
            _userLayer.Verify(x => x.GetUserDetailByMobileNumber(It.IsAny<string>()), Times.Once);
            _jwtBuilder.Verify(x => x.GetJWTToken(It.IsAny<Dictionary<string, string>>(), It.IsAny<string>(), It.IsAny<DateTime?>()), Times.Once);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.NotNull(ActualResult.Response);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.Response.UserId == 1);
            Assert.NotNull(ActualResult.Response.TokenDetail);
            Assert.True(!string.IsNullOrEmpty(ActualResult.Response.TokenDetail.Type));
            Assert.True(!string.IsNullOrEmpty(ActualResult.Response.TokenDetail.Value));
            Assert.True(ActualResult.Response.TokenDetail.CreatedAt != DateTime.MinValue);
        }
    }
}
