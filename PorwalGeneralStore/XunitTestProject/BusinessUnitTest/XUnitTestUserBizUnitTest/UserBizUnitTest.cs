using Moq;
using PorwalGeneralStore.BusinessLayer.Interface.Users;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.Utility.JWTTokenGenerator;
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

        [Fact(DisplayName = "Business -: Handling Exception in ReadMultipleProduct")]
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
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1 });

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
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1 });

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
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1 });

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
            _userLayer.Setup(x => x.GetUserDetail(It.IsAny<LoginForm>())).Returns(new UserInformation() { UserId = 1 });

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
