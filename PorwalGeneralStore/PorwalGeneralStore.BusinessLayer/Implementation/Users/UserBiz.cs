using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.DataModel.Response.Users;
using PorwalGeneralStore.Utility.JWTTokenGenerator;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.BusinessLayer.Interface.Users
{
    public class UserBiz : IUserBiz
    {
        private readonly IUserLayer _userLayer;
        private readonly IJwtBuilder _jwtBuilder;

        public UserBiz(IUserLayer userLayer, IJwtBuilder jwtBuilder)
        {
            _userLayer = userLayer;
            _jwtBuilder = jwtBuilder;
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
                    //loginFormResponse.Response.TokenDetail = GetJWTToken(userInformation);
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

        public JwtTokenResponse GetJWTToken(UserInformation userInformation)
        {
            JwtTokenResponse jwtTokenResponse = new JwtTokenResponse()
            {
                StatusCode = 200
            };

            if (userInformation == null)
            {
                jwtTokenResponse.StatusCode = 400;
                jwtTokenResponse.ErrorList = new List<JwtValidation>()
                {
                    new JwtValidation()
                    {
                        FieldName=nameof(userInformation),
                        Message=nameof(userInformation)+" can't be blank."
                    }
                };
                return jwtTokenResponse;
            }

            if (string.IsNullOrWhiteSpace(userInformation.CustomerName))
            {
                jwtTokenResponse.StatusCode = 400;
                jwtTokenResponse.ErrorList = new List<JwtValidation>()
                {
                    new JwtValidation()
                    {
                        FieldName=nameof(userInformation.CustomerName),
                        Message=nameof(userInformation.CustomerName)+" can't be blank."
                    }
                };
                return jwtTokenResponse;
            }

            if (userInformation.UserId <= 0)
            {
                jwtTokenResponse.StatusCode = 400;
                jwtTokenResponse.ErrorList = new List<JwtValidation>()
                {
                    new JwtValidation()
                    {
                        FieldName=nameof(userInformation.UserId),
                        Message=nameof(userInformation.UserId)+" can't less then or equal to 0."
                    }
                };
                return jwtTokenResponse;
            }

            Dictionary<string, string> claimList = new Dictionary<string, string>()
                {
                    { "UserId",userInformation.UserId.ToString()},
                    { "UserName",userInformation.CustomerName.ToString()}
                };

            jwtTokenResponse = _jwtBuilder.GetJWTToken(claimList, "Local Customer", null);
            return jwtTokenResponse;
        }
    }
}
