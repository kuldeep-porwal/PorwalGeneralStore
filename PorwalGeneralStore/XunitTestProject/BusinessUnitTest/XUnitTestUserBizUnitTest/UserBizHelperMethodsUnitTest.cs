using Moq;
using PorwalGeneralStore.BusinessLayer.Interface.Users;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Public.Business;
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
        public UserBizHelperMethodsUnitTest()
        {
            _userLayer = new Mock<IUserLayer>();
            _userBiz = new UserBiz(_userLayer.Object);
        }

        [Fact(DisplayName = "UserBizHelperMethod -: Invalid Token Object ")]
        public void UnitTest1()
        {
            var ActualResult = _userBiz.GetJWTToken(null);

            Assert.Null(ActualResult);
        }

        [Fact(DisplayName = "UserBizHelperMethod -: Valid Token Object ")]
        public void UnitTest2()
        {
            var ActualResult = _userBiz.GetJWTToken(new UserInformation());
            Assert.NotNull(ActualResult);
        }

        [Fact(DisplayName = "UserBizHelperMethod -: Valid Token Response ")]
        public void UnitTest3()
        {
            var ActualResult = _userBiz.GetJWTToken(new UserInformation());

            Assert.NotNull(ActualResult);
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Type));
            Assert.True(!string.IsNullOrWhiteSpace(ActualResult.Value));
            Assert.True(ActualResult.CreatedAt != DateTime.MinValue);
        }
    }
}
