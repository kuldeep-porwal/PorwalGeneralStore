using System;
using InMemoryDbContext;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.EdmxModel;
using System.Linq;
using Xunit;
using System.Collections.Generic;

namespace XUnitTestUserDataAccessLayerUnitTest
{
    public class UserDataAccessLayerUnitTest
    {
        private IUserLayer _userLayer;
        private PorwalGeneralStoreContext _porwalGeneralStoreContext;
        public Startup startup;
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

        [Theory(DisplayName = "DataAccessLayer - Get True is Mobile Number Exist False if not Exist")]
        [InlineData("No Exist")]
        [InlineData("Exist")]
        public void UnitTest3(string mobileNumber)
        {
            var ExpectedResult = _porwalGeneralStoreContext
                                  .CustomerInfo
                                  .Any(x => x.Phone.Equals(mobileNumber, StringComparison.OrdinalIgnoreCase));
            var ActualResult = _userLayer.isExistPhoneNumber(mobileNumber);
            Assert.True(ActualResult == ExpectedResult);
        }

        [Theory(DisplayName = "DataAccessLayer -: save user in Database.")]
        [MemberData(nameof(signUpFormData))]
        public void UnitTest4(SignUpForm input)
        {
            startup = new Startup("PorwalGeneralStore_RegisterUser_DB", false);
            _porwalGeneralStoreContext = startup._inMemoryContext;
            _userLayer = new UserLayer(_porwalGeneralStoreContext);
            var ActualResult = _userLayer.RegisterUser(input);
            var ExpectedResult = _porwalGeneralStoreContext
                                  .CustomerInfo
                                  .Any(x => x.Phone.Equals(input.MobileNumber, StringComparison.OrdinalIgnoreCase));

            Assert.True(ActualResult);
            Assert.True(ExpectedResult);
        }
        public static IEnumerable<object[]> signUpFormData => new List<object[]>
                                                                                {
                                                                                    new object[] { GetTempSignupFormData },
                                                                                };
        public static SignUpForm GetTempSignupFormData
        {
            get
            {
                return new SignUpForm()
                {
                    FirstName = "FirstName",
                    City = "City",
                    LastName = "LastName",
                    MobileNumber = "MobileNumber",
                    Password = "Password",
                    UserName = "UserName"
                };
            }
        }
    }
}
