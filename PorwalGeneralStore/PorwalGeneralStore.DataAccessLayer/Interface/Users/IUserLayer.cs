using PorwalGeneralStore.DataModel.Public.Business;
using PorwalGeneralStore.DataModel.Request.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace PorwalGeneralStore.DataAccessLayer.Interface.Users
{
    public interface IUserLayer
    {
        UserInformation GetUserDetail(LoginForm loginForm);
    }
}
