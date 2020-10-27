using BS_Models;
using System;
using System.Collections.Generic;

namespace BS_Interfaces
{
    public interface IAuthentication
    {
        ResponseApi<dynamic> CreateUser(UserInfo objUserProfle);
        ResponseApi<dynamic> ValidateAsync(LoginModel objLoginModel);
        string GetUserId(string userName);
        List<RoleInfo> GetUserRoleList(string userName);
        
    }
}
