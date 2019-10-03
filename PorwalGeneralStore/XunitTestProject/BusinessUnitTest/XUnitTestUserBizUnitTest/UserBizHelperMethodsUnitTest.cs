using Moq;
using PorwalGeneralStore.BusinessLayer.Interface.Users;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.Utility.JWTTokenGenerator;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestUserBizUnitTest
{
    public class UserBizHelperMethodsUnitTest
    {
        private readonly UserBiz _userBiz;
        private readonly Mock<IUserLayer> _userLayer;
        private readonly Mock<IJwtBuilder> _jwtBuilder;
        public UserBizHelperMethodsUnitTest()
        {
            _userLayer = new Mock<IUserLayer>();
            _jwtBuilder = new Mock<IJwtBuilder>();
            _userBiz = new UserBiz(_userLayer.Object, _jwtBuilder.Object);
        }

        [Fact(DisplayName = "UserBizHelperMethod -: Invalid Token Object ")]
        public void UnitTest1()
        {
            var ActualResult = _userBiz.GetJWTToken(null);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.TokenDetail);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "UserBizHelperMethod -: Valid Token Object ")]
        public void UnitTest2()
        {
            _jwtBuilder.Setup(x => x.GetJWTToken(
                                    It.IsAny<Dictionary<string, string>>(),
                                    It.IsAny<string>(),
                                    It.IsAny<DateTime?>())
                                    ).Returns(new JwtTokenResponse()
                                    {
                                        StatusCode = 200,
                                        ErrorList = null,
                                        TokenDetail = new JwtToken()
                                        {
                                            Type = "Bearer",
                                            Value = "12345667890oliyrfdg4dbh65rds34edght65dsw24rdvg54edfhy654ewscvbhytr",
                                            CreatedAt = DateTime.UtcNow
                                        }
                                    });


            var ActualResult = _userBiz.GetJWTToken(new UserInformation() { UserId = 1, CustomerName = "test" });
            Assert.NotNull(ActualResult);
        }

        [Fact(DisplayName = "UserBizHelperMethod -: Valid Token Response ")]
        public void UnitTest3()
        {
            _jwtBuilder.Setup(x => x.GetJWTToken(
                                    It.IsAny<Dictionary<string, string>>(),
                                    It.IsAny<string>(),
                                    It.IsAny<DateTime?>())
                                    ).Returns(new JwtTokenResponse()
                                    {
                                        StatusCode = 200,
                                        ErrorList = null,
                                        TokenDetail = new JwtToken()
                                        {
                                            Type = "Bearer",
                                            Value = "12345667890oliyrfdg4dbh65rds34edght65dsw24rdvg54edfhy654ewscvbhytr",
                                            CreatedAt = DateTime.UtcNow
                                        }
                                    });

            var ActualResult = _userBiz.GetJWTToken(new UserInformation() { UserId = 1, CustomerName = "test" });

            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.TokenDetail);
            Assert.Null(ActualResult.ErrorList);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.TokenDetail.Type));
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.TokenDetail.Value));
            Assert.True(ActualResult.TokenDetail.CreatedAt != DateTime.MinValue);
        }

        [Theory(DisplayName = "UserBizHelperMethod -: Validate UserInformation Object ")]
        [MemberData(nameof(UserInformationData))]
        public void UnitTest4(UserInformation userInformation)
        {
            var ActualResult = _userBiz.GetJWTToken(userInformation);

            Assert.NotNull(ActualResult);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.Null(ActualResult.TokenDetail);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        public static IEnumerable<object[]> UserInformationData =>
        new List<object[]>
        {
            new object[] { null },
            new object[] { new UserInformation() },
            new object[] { new UserInformation() { } },
            new object[] { new UserInformation() { UserId=1} },
            new object[] { new UserInformation() { CustomerName="kuldeep"} },
            new object[] { new UserInformation() { FirstName="kuldeep",LastName="Porwal"} },
        };
    }
}
