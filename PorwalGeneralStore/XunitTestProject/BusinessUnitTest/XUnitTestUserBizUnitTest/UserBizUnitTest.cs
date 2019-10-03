using Moq;
using PorwalGeneralStore.BusinessLayer.Interface.Users;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Users;
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
        public UserBizUnitTest()
        {
            _userLayer = new Mock<IUserLayer>();
            _jwtBuilder = new Mock<IJwtBuilder>();
            _userBiz = new UserBiz(_userLayer.Object, _jwtBuilder.Object);
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
                    UserName = "kuldeep",
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
        [InlineData("kuldeep", "Password")]
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
        [InlineData("kuldeep", "Password")]
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
        [InlineData("kuldeep", "Password")]
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
        [InlineData("kuldeep", "Password")]
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
    }
}
