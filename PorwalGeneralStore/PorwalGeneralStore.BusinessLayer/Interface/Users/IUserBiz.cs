using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.DataModel.Response.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Interface.Users
{
    public interface IUserBiz
    {
        LoginFormResponse AuthenticateUser(LoginForm loginForm);

        LoginFormResponse AuthenticateUserByMobileNumber(OtpLoginForm otpLoginForm);

        SignUpFormResponse RegistorUser(SignUpForm signUpForm);

        MobileNumberVerificationResponse VerifyUserAccount(string mobileNumber);
    }
}
