using Moq;
using PorwalGeneralStore.BusinessLayer.Interface.Users;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.DataModel.Response.Users;
using PorwalGeneralStore.Utility.JWTTokenGenerator;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace XUnitTestUserBizUnitTest
{
    public class UserBizRegistorUserUnitTest
    {
        private readonly IUserBiz _userBiz;
        private readonly Mock<IUserLayer> _userLayer;
        private readonly Mock<IJwtBuilder> _jwtBuilder;
        public UserBizRegistorUserUnitTest()
        {
            _userLayer = new Mock<IUserLayer>();
            _jwtBuilder = new Mock<IJwtBuilder>();
            _userBiz = new UserBiz(_userLayer.Object, _jwtBuilder.Object);
        }

        [Fact(DisplayName = "Business -: null Object Error ")]
        public void UnitTest1()
        {
            var ActualResult = _userBiz.RegistorUser(null);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid Username Validation Error Should Come")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UnitTest2(string userName)
        {
            var ActualResult = _userBiz.RegistorUser(new SignUpForm() { UserName = userName });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid Password Validation Error Should Come")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UnitTest3(string password)
        {
            var ActualResult = _userBiz.RegistorUser(new SignUpForm() { Password = password });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid FirstName Validation Error Should Come")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UnitTest4(string firstName)
        {
            var ActualResult = _userBiz.RegistorUser(new SignUpForm() { FirstName = firstName });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid LastName Validation Error Should Come")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UnitTest5(string lastName)
        {
            var ActualResult = _userBiz.RegistorUser(new SignUpForm() { LastName = lastName });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid MobileNumber Validation Error Should Come")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UnitTest6(string mobileNumber)
        {
            var ActualResult = _userBiz.RegistorUser(new SignUpForm() { MobileNumber = mobileNumber });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Invalid City Validation Error Should Come")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void UnitTest7(string city)
        {
            var ActualResult = _userBiz.RegistorUser(new SignUpForm() { City = city });

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Fact(DisplayName = "Business -: Signup response Object should not come null ")]
        public void UnitTest8()
        {
            var ActualResult = _userBiz.RegistorUser(null);
            Assert.True(ActualResult.GetType() == typeof(SignUpFormResponse));
            Assert.NotNull(ActualResult);
        }

        [Theory(DisplayName = "Business -: Mobile Number is Already Registered validation error should come if mobile number is exist.")]
        [MemberData(nameof(signUpFormData))]
        public void UnitTest9(SignUpForm input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(true);

            var ActualResult = _userBiz.RegistorUser(input);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Mobile Number is Already Registered validation error should not come if mobile number is not exist.")]
        [MemberData(nameof(signUpFormDataWithValidMobileNumber))]
        public void UnitTest10(SignUpForm input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(false);
            _userLayer.Setup(x => x.RegisterUser(It.IsAny<SignUpForm>())).Returns(true);

            var ActualResult = _userBiz.RegistorUser(input);

            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.Null(ActualResult.ErrorList);
        }

        [Theory(DisplayName = "Business -: user should register successfully.")]
        [MemberData(nameof(signUpFormDataWithValidMobileNumber))]
        public void UnitTest11(SignUpForm input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(false);
            _userLayer.Setup(x => x.RegisterUser(It.IsAny<SignUpForm>())).Returns(true);

            var ActualResult = _userBiz.RegistorUser(input);

            _userLayer.Verify(x => x.RegisterUser(input), Times.Once);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.Equal("User is Successfully Registered.", ActualResult.Message);
            Assert.Null(ActualResult.ErrorList);
        }

        [Theory(DisplayName = "Business -: validation error come when user is not register successfully.")]
        [MemberData(nameof(signUpFormDataWithValidMobileNumber))]
        public void UnitTest12(SignUpForm input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(false);
            _userLayer.Setup(x => x.RegisterUser(It.IsAny<SignUpForm>())).Returns(false);

            var ActualResult = _userBiz.RegistorUser(input);

            _userLayer.Verify(x => x.RegisterUser(input), Times.Once);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: validation error come when any exception is rised.")]
        [MemberData(nameof(signUpFormDataWithValidMobileNumber))]
        public void UnitTest13(SignUpForm input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(false);
            _userLayer.Setup(x => x.RegisterUser(It.IsAny<SignUpForm>())).Throws(new Exception());

            var ActualResult = _userBiz.RegistorUser(input);

            _userLayer.Verify(x => x.RegisterUser(input), Times.Once);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Handling Exception in RegistorUser.")]
        [MemberData(nameof(signUpFormDataWithValidMobileNumber))]
        public void UnitTest14(SignUpForm input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(false);
            _userLayer.Setup(x => x.RegisterUser(It.IsAny<SignUpForm>())).Throws(new Exception());

            var ActualResult = Record.Exception(() => _userBiz.RegistorUser(input));

            _userLayer.Verify(x => x.RegisterUser(input), Times.Once);
            Assert.Null(ActualResult);
        }

        [Theory(DisplayName = "Business -: Invalid MobileNumber Validation Error Should Come")]
        [MemberData(nameof(signUpFormData))]
        public void UnitTest15(SignUpForm input)
        {
            _userLayer.Setup(x => x.RegisterUser(It.IsAny<SignUpForm>())).Returns(true);
            var ActualResult = _userBiz.RegistorUser(input);

            _userLayer.Verify(x => x.RegisterUser(input), Times.Never());
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Valid MobileNumber no Error Should Come")]
        [MemberData(nameof(signUpFormDataWithValidMobileNumber))]
        public void UnitTest16(SignUpForm input)
        {
            _userLayer.Setup(x => x.RegisterUser(It.IsAny<SignUpForm>())).Returns(true);
            var ActualResult = _userBiz.RegistorUser(input);

            _userLayer.Verify(x => x.RegisterUser(input), Times.Once);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.Null(ActualResult.ErrorList);
        }

        [Theory(DisplayName = "Business -: Valid Response should come if mobile number is exist on server")]
        [InlineData("0123456789")]
        public void UnitTest17(string input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(true);
            var ActualResult = _userBiz.VerifyUserAccount(input);

            _userLayer.Verify(x => x.isExistPhoneNumber(input), Times.Once);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 200);
            Assert.Equal("mobileNumber is exist", ActualResult.Message);
            Assert.Null(ActualResult.ErrorList);
        }

        [Theory(DisplayName = "Business -: Validation Response should come if mobile number is not exist on server")]
        [InlineData("0123456789")]
        public void UnitTest18(string input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(false);
            var ActualResult = _userBiz.VerifyUserAccount(input);

            _userLayer.Verify(x => x.isExistPhoneNumber(input), Times.Once);
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        [Theory(DisplayName = "Business -: Validation Response should come if mobile number is not valid")]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("123456677")]
        [InlineData("not valide")]
        [InlineData("+784512dere1d1")]
        [InlineData("fcc dd d d d")]
        [InlineData("4zzx4ss4s4ss4")]
        [InlineData("9874125000w")]
        public void UnitTest19(string input)
        {
            _userLayer.Setup(x => x.isExistPhoneNumber(It.IsAny<string>())).Returns(false);
            var ActualResult = _userBiz.VerifyUserAccount(input);

            _userLayer.Verify(x => x.isExistPhoneNumber(input), Times.Never());
            Assert.NotNull(ActualResult);
            Assert.True(ActualResult.StatusCode == 400);
            Assert.True(string.IsNullOrWhiteSpace(ActualResult.Message));
            Assert.NotNull(ActualResult.ErrorList);
            Assert.True(ActualResult.ErrorList.Count > 0);
        }

        public static IEnumerable<object[]> signUpFormData => new List<object[]>
                                                                                {
                                                                                    new object[] { GetTempSignupFormData },
                                                                                };
        public static IEnumerable<object[]> signUpFormDataWithValidMobileNumber => new List<object[]>
                                                                                {
                                                                                    new object[] { GetTempSignupFormDataForMobileNumber("0123456789") },
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
                    MobileNumber = "0123456789a",
                    Password = "Password",
                    UserName = "UserName"
                };
            }
        }

        public static SignUpForm GetTempSignupFormDataForMobileNumber(string mobileNumber)
        {
            return new SignUpForm()
            {
                FirstName = "FirstName",
                City = "City",
                LastName = "LastName",
                MobileNumber = mobileNumber,
                Password = "Password@123",
                UserName = "UserName"
            };
        }
    }
}
