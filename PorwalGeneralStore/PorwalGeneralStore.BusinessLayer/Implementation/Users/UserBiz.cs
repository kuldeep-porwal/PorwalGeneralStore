using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.DataModel.Response.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Interface.Users
{
    public class UserBiz : IUserBiz
    {
        private readonly IUserLayer _userLayer;

        public UserBiz(IUserLayer userLayer)
        {
            _userLayer = userLayer;
        }

        public LoginFormResponse AuthenticateUser(LoginForm loginForm)
        {
            LoginFormResponse loginFormResponse = new LoginFormResponse()
            {
                StatusCode = 200
            };

            try
            {
                if (loginForm == null)
                {
                    loginFormResponse.StatusCode = 400;
                    loginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message="Request Object can't be blank."
                        }
                    };
                    return loginFormResponse;
                }

                if (string.IsNullOrWhiteSpace(loginForm.UserName))
                {
                    loginFormResponse.StatusCode = 400;
                    loginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message="UserName can't be blank."
                        }
                    };
                    return loginFormResponse;
                }

                if (string.IsNullOrWhiteSpace(loginForm.Password))
                {
                    loginFormResponse.StatusCode = 400;
                    loginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message="Password can't be blank."
                        }
                    };
                    return loginFormResponse;
                }

                UserInformation userInformation = _userLayer.GetUserDetail(loginForm);
                if (userInformation != null)
                {
                    loginFormResponse.StatusCode = 200;
                    loginFormResponse.Response = new LoginResponse();
                    loginFormResponse.Response.UserId = userInformation.UserId;
                    loginFormResponse.Response.TokenDetail = GetJWTToken(userInformation);
                }
                else
                {
                    loginFormResponse.StatusCode = 400;
                    loginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code =1001,
                            Message="User Not Found"
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                loginFormResponse.StatusCode = 400;
                loginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message="User Authentication Failed"+ex.Message
                        }
                    };
            }
            return loginFormResponse;
        }

        public Token GetJWTToken(UserInformation userInformation)
        {
            Token token = null;
            if (userInformation != null)
            {
                token = new Token();
                token.Type = "Bearer";
                token.Value = "JWT Token";
                token.CreatedAt = DateTime.UtcNow;

            }
            return token;
        }
    }
}
