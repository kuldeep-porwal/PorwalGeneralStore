using System;
using InMemoryDbContext;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.EdmxModel;
using System.Linq;
using Xunit;

namespace XUnitTestUserDataAccessLayerUnitTest
{
    public class UserDataAccessLayerUnitTest
    {
        private readonly IUserLayer _userLayer;
        private readonly PorwalGeneralStoreContext _porwalGeneralStoreContext;
        public readonly Startup startup;
        public UserDataAccessLayerUnitTest()
        {
            startup = new Startup();
            _porwalGeneralStoreContext = startup._inMemoryContext;
            _userLayer = new UserLayer(_porwalGeneralStoreContext);
        }

        [Theory(DisplayName = "DataAccessLayer - Get Userinformation")]
        [InlineData("KuldeeP", "12345")]
        [InlineData("Aman PAL", "Pal123")]
        [InlineData("AmAn PaL", "Pal123")]
        [InlineData("AMAN PAL", "Pal123")]
        public void UnitTest1(string userName, string password)
        {
            var ExpectedResult = _porwalGeneralStoreContext
                                  .CustomerInfo
                                  .FirstOrDefault(
                                        x =>
                                            x.CustomerName.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
                                            x.Password.Equals(password));

            var ActualResult = _userLayer.GetUserDetail(new LoginForm()
            {
                UserName = userName,
                Password = password
            });

            Assert.NotNull(ActualResult);
            Assert.True(ExpectedResult.Id == ActualResult.UserId);
            Assert.True(ExpectedResult.CustomerName == ActualResult.CustomerName);
            Assert.True(ExpectedResult.FirstName == ActualResult.FirstName);
            Assert.True(ExpectedResult.LastName == ActualResult.LastName);
            Assert.True(ExpectedResult.City == ActualResult.City);
            Assert.True(ExpectedResult.Phone == ActualResult.Phone);
        }


        [Theory(DisplayName = "DataAccessLayer - Get Userinformation null")]
        [InlineData("Kuldeep1", "12345")]
        public void UnitTest2(string userName, string password)
        {
            var ActualResult = _userLayer.GetUserDetail(new LoginForm()
            {
                UserName = userName,
                Password = password
            });
            Assert.Null(ActualResult);
        }
    }
}
