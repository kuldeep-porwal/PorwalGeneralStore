using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.EdmxModel;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;

namespace PorwalGeneralStore.DataAccessLayer.Interface.Users
{
    public class UserLayer : IUserLayer
    {
        private readonly PorwalGeneralStoreContext context;
        public UserLayer(PorwalGeneralStoreContext _context)
        {
            context = _context;
        }

        public UserInformation GetUserDetail(LoginForm loginForm)
        {
            UserInformation userInformation = null;

            CustomerInfo customerInfo = context.CustomerInfo.FirstOrDefault(
                                        x => x.Phone.Equals(loginForm.UserName, StringComparison.OrdinalIgnoreCase) &&
                                            x.Password.Equals(loginForm.Password));
            if (customerInfo != null)
            {
                userInformation = new UserInformation()
                {
                    UserId = customerInfo.Id,
                    City = customerInfo.City,
                    CustomerName = customerInfo.CustomerName,
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    Phone = customerInfo.Phone
                };
            }
            return userInformation;
        }

        public UserInformation GetUserDetailByMobileNumber(string MobileNumber)
        {
            UserInformation userInformation = null;

            CustomerInfo customerInfo = context.CustomerInfo.FirstOrDefault(
                                        x => x.Phone.Equals(MobileNumber, StringComparison.OrdinalIgnoreCase));
            if (customerInfo != null)
            {
                userInformation = new UserInformation()
                {
                    UserId = customerInfo.Id,
                    City = customerInfo.City,
                    CustomerName = customerInfo.CustomerName,
                    FirstName = customerInfo.FirstName,
                    LastName = customerInfo.LastName,
                    Phone = customerInfo.Phone
                };
            }
            return userInformation;
        }

        public bool isExistPhoneNumber(string phoneNumber)
        {
            return context.CustomerInfo.Any(x => x.Phone.Equals(phoneNumber, StringComparison.OrdinalIgnoreCase));
        }

        public bool RegisterUser(SignUpForm signUpForm)
        {
            CustomerInfo customerInfo = new CustomerInfo()
            {
                CustomerName = signUpForm.UserName,
                Phone = signUpForm.MobileNumber,
                City = signUpForm.City,
                FirstName = signUpForm.FirstName,
                LastName = signUpForm.LastName,
                Password = signUpForm.Password
            };

            context.Entry<CustomerInfo>(customerInfo).State = EntityState.Added;
            return context.SaveChanges() > 0;
        }
    }
}
