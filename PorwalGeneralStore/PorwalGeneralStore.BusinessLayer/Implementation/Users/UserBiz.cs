﻿using PorwalGeneralStore.BusinessLayer.Interface.Sms;
using PorwalGeneralStore.DataAccessLayer.Interface.Users;
using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Sms;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.DataModel.Response.Sms;
using PorwalGeneralStore.DataModel.Response.Users;
using PorwalGeneralStore.Utility;
using PorwalGeneralStore.Utility.JWTTokenGenerator;
using PorwalGeneralStore.Utility.JWTTokenGenerator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PorwalGeneralStore.BusinessLayer.Interface.Users
{
    public class UserBiz : IUserBiz
    {
        private readonly IUserLayer _userLayer;
        private readonly IJwtBuilder _jwtBuilder;
        private readonly ISmsBiz _smsBiz;

        public UserBiz(IUserLayer userLayer, IJwtBuilder jwtBuilder, ISmsBiz smsBiz)
        {
            _userLayer = userLayer;
            _jwtBuilder = jwtBuilder;
            _smsBiz = smsBiz;
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

                if (!Regex.IsMatch(loginForm.UserName, RegexPattern.mobile_number_validation_Patterns.GetCombinedPattern()))
                {
                    loginFormResponse.StatusCode = 400;
                    loginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(loginForm.UserName),
                            Message=nameof(loginForm.UserName)+" should be valid. Format -: xxxxxxxxxx "
                        }
                    };
                    return loginFormResponse;
                }

                UserInformation userInformation = _userLayer.GetUserDetail(loginForm);
                if (userInformation != null)
                {
                    JwtTokenResponse jwtTokenResponse = GetJWTToken(userInformation);
                    if (jwtTokenResponse.StatusCode == 200)
                    {
                        JwtToken tokenDetail = jwtTokenResponse.TokenDetail;
                        loginFormResponse.StatusCode = 200;
                        loginFormResponse.Response = new LoginResponse();
                        loginFormResponse.Response.UserId = userInformation.UserId;
                        if (tokenDetail != null)
                        {
                            loginFormResponse.Response.TokenDetail = new Token()
                            {
                                Type = tokenDetail.Type,
                                Value = tokenDetail.Value,
                                CreatedAt = tokenDetail.CreatedAt,
                                ExpiredAt = tokenDetail.ExpiredAt
                            };
                        }
                    }
                    else
                    {
                        loginFormResponse.StatusCode = 400;
                        loginFormResponse.ErrorList = jwtTokenResponse
                                                        .ErrorList
                                                        .Select(x => new LoginValidationResponse()
                                                        {
                                                            FieldName = x.FieldName,
                                                            Message = x.Message,
                                                            Code = x.Code
                                                        }).ToList();
                    }
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

        public LoginFormResponse AuthenticateUserByMobileNumber(OtpLoginForm otpLoginForm)
        {
            LoginFormResponse otpLoginFormResponse = new LoginFormResponse()
            {
                StatusCode = 200
            };

            if (otpLoginForm == null)
            {
                otpLoginFormResponse.StatusCode = 400;
                otpLoginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message="Request Object can't be blank."
                        }
                    };
                return otpLoginFormResponse;
            }

            if (string.IsNullOrWhiteSpace(otpLoginForm.MobileNumber))
            {
                otpLoginFormResponse.StatusCode = 400;
                otpLoginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message="MobileNumber can't be blank."
                        }
                    };
                return otpLoginFormResponse;
            }

            if (string.IsNullOrWhiteSpace(otpLoginForm.Otp))
            {
                otpLoginFormResponse.StatusCode = 400;
                otpLoginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message="Otp can't be blank."
                        }
                    };
                return otpLoginFormResponse;
            }

            if (otpLoginForm.CountryCode <= 0)
            {
                otpLoginFormResponse.StatusCode = 400;
                otpLoginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message="CountryCode can't be blank."
                        }
                    };
                return otpLoginFormResponse;
            }

            if (!Regex.IsMatch(otpLoginForm.MobileNumber, RegexPattern.mobile_number_validation_Patterns.GetCombinedPattern()))
            {
                otpLoginFormResponse.StatusCode = 400;
                otpLoginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(otpLoginForm.MobileNumber),
                            Message=nameof(otpLoginForm.MobileNumber)+" should be valid. Format -: xxxxxxxxxx "
                        }
                    };
                return otpLoginFormResponse;
            }

            bool isMobileNumberExist = _userLayer.isExistPhoneNumber(otpLoginForm.MobileNumber);
            if (isMobileNumberExist)
            {
                SmsApiResponse smsApiResponse = _smsBiz.VerifyOtpSms(new VerifyOtpRequest()
                {
                    Mobile = otpLoginForm.MobileNumber,
                    Otp = otpLoginForm.Otp,
                    CountryCode = otpLoginForm.CountryCode
                });

                if (smsApiResponse.StatusCode == 200)
                {
                    UserInformation userInformation = _userLayer.GetUserDetailByMobileNumber(otpLoginForm.MobileNumber);
                    if (userInformation != null)
                    {
                        JwtTokenResponse jwtTokenResponse = GetJWTToken(userInformation);
                        if (jwtTokenResponse.StatusCode == 200)
                        {
                            JwtToken tokenDetail = jwtTokenResponse.TokenDetail;
                            otpLoginFormResponse.StatusCode = 200;
                            otpLoginFormResponse.Response = new LoginResponse();
                            otpLoginFormResponse.Response.UserId = userInformation.UserId;
                            if (tokenDetail != null)
                            {
                                otpLoginFormResponse.Response.TokenDetail = new Token()
                                {
                                    Type = tokenDetail.Type,
                                    Value = tokenDetail.Value,
                                    CreatedAt = tokenDetail.CreatedAt,
                                    ExpiredAt = tokenDetail.ExpiredAt
                                };
                            }
                        }
                        else
                        {
                            otpLoginFormResponse.StatusCode = 400;
                            otpLoginFormResponse.ErrorList = jwtTokenResponse
                                                            .ErrorList
                                                            .Select(x => new LoginValidationResponse()
                                                            {
                                                                FieldName = x.FieldName,
                                                                Message = x.Message,
                                                                Code = x.Code
                                                            }).ToList();
                        }
                    }
                }
                else
                {
                    otpLoginFormResponse.StatusCode = 400;
                    otpLoginFormResponse.ErrorList = smsApiResponse.ErrorList.Select(x => new LoginValidationResponse()
                    {
                        Code = x.Code,
                        Message = x.Message
                    }).ToList();
                }
            }
            else
            {
                otpLoginFormResponse.StatusCode = 400;
                otpLoginFormResponse.ErrorList = new List<LoginValidationResponse>()
                    {
                        new LoginValidationResponse()
                        {
                            Code=1001,
                            Message=nameof(otpLoginForm.MobileNumber)+" not found."
                        }
                    };
            }

            return otpLoginFormResponse;
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

        public SignUpFormResponse RegistorUser(SignUpForm signUpForm)
        {
            SignUpFormResponse signUpFormResponse = new SignUpFormResponse()
            {
                StatusCode = 200
            };
            try
            {

                if (signUpForm == null)
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm),
                            Message=nameof(signUpForm)+" can't be null"
                        }
                    };
                    return signUpFormResponse;
                }

                if (string.IsNullOrWhiteSpace(signUpForm.FirstName))
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.FirstName),
                            Message=nameof(signUpForm.FirstName)+" can't be blank"
                        }
                    };
                    return signUpFormResponse;
                }

                if (string.IsNullOrWhiteSpace(signUpForm.LastName))
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.LastName),
                            Message=nameof(signUpForm.LastName)+" can't be blank"
                        }
                    };
                    return signUpFormResponse;
                }

                if (string.IsNullOrWhiteSpace(signUpForm.UserName))
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.UserName),
                            Message=nameof(signUpForm.UserName)+" can't be blank"
                        }
                    };
                    return signUpFormResponse;
                }

                if (string.IsNullOrWhiteSpace(signUpForm.Password))
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.Password),
                            Message=nameof(signUpForm.Password)+" can't be blank"
                        }
                    };
                    return signUpFormResponse;
                }

                if (string.IsNullOrWhiteSpace(signUpForm.City))
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.City),
                            Message=nameof(signUpForm.City)+" can't be blank"
                        }
                    };
                    return signUpFormResponse;
                }

                if (string.IsNullOrWhiteSpace(signUpForm.MobileNumber))
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.MobileNumber),
                            Message=nameof(signUpForm.MobileNumber)+" can't be blank"
                        }
                    };
                    return signUpFormResponse;
                }

                if (!Regex.IsMatch(signUpForm.MobileNumber, RegexPattern.mobile_number_validation_Patterns.GetCombinedPattern()))
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.MobileNumber),
                            Message=nameof(signUpForm.MobileNumber)+" should be valid. Format -: xxxxxxxxxx "
                        }
                    };
                    return signUpFormResponse;
                }

                if (!Regex.IsMatch(signUpForm.Password, RegexPattern.password_validation_pattern))
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.Password),
                            Message=nameof(signUpForm.Password)+" should be valid. Format -: atleast one uppercase,one lowercase, one special character and one digit "
                        }
                    };
                    return signUpFormResponse;
                }

                bool isMobileNumberExist = _userLayer.isExistPhoneNumber(signUpForm.MobileNumber);
                if (isMobileNumberExist)
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            FieldName=nameof(signUpForm.MobileNumber),
                            Message=nameof(signUpForm.MobileNumber)+" already exist , please use different number."
                        }
                    };
                    return signUpFormResponse;
                }

                bool isRegisteredSuccessfully = _userLayer.RegisterUser(signUpForm);
                if (isRegisteredSuccessfully)
                {
                    signUpFormResponse.StatusCode = 200;
                    signUpFormResponse.ErrorList = null;
                    signUpFormResponse.Message = "User is Successfully Registered.";
                }
                else
                {
                    signUpFormResponse.StatusCode = 400;
                    signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            Message="Error While creating account on server , please try after some time."
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                signUpFormResponse.StatusCode = 400;
                signUpFormResponse.ErrorList = new List<SignUpValidationResponse>()
                    {
                        new SignUpValidationResponse()
                        {
                            Code=1001,
                            Message="Error While creating account on server , please try after some time."+ex.Message
                        }
                    };
            }

            return signUpFormResponse;
        }

        public MobileNumberVerificationResponse VerifyUserAccount(string mobileNumber)
        {
            MobileNumberVerificationResponse signUpFormResponse = new MobileNumberVerificationResponse()
            {
                StatusCode = 200
            };

            if (string.IsNullOrWhiteSpace(mobileNumber))
            {
                signUpFormResponse.StatusCode = 400;
                signUpFormResponse.ErrorList = new List<MobileNumberValidationResponse>()
                    {
                        new MobileNumberValidationResponse()
                        {
                            Code=1001,
                            Message=nameof(mobileNumber)+" can't be blank"
                        }
                    };
                return signUpFormResponse;
            }

            if (!Regex.IsMatch(mobileNumber, RegexPattern.mobile_number_validation_Patterns.GetCombinedPattern()))
            {
                signUpFormResponse.StatusCode = 400;
                signUpFormResponse.ErrorList = new List<MobileNumberValidationResponse>()
                    {
                        new MobileNumberValidationResponse()
                        {
                            Code=1001,
                            Message=nameof(mobileNumber)+" should be valid. Format -: xxxxxxxxxx "
                        }
                    };
                return signUpFormResponse;
            }

            bool isMobileNumberExist = _userLayer.isExistPhoneNumber(mobileNumber);
            if (isMobileNumberExist)
            {
                signUpFormResponse.StatusCode = 200;
                signUpFormResponse.Message = "mobileNumber is exist";
                return signUpFormResponse;
            }
            else
            {
                signUpFormResponse.StatusCode = 400;
                signUpFormResponse.ErrorList = new List<MobileNumberValidationResponse>()
                    {
                        new MobileNumberValidationResponse()
                        {
                            Code=1001,
                            Message=nameof(mobileNumber)+" not found."
                        }
                    };
            }

            return signUpFormResponse;
        }
    }
}
