using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Users;
using PorwalGeneralStore.EdmxModel;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

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
                                        x => x.CustomerName.Equals(loginForm.UserName, StringComparison.OrdinalIgnoreCase) &&
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
    }
}
