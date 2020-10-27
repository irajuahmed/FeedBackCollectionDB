/// *************************************************************************************************
/// || Creation History ||
/// -------------------------------------------------------------------------------------------------
/// Copyright : BrainStation
/// NameSpace : Security.Controllers
/// Class     : AuthenticationController
/// Inherits  : ControllerBase
/// Author    : Md. Habibur Rahman Jewel
/// Purpose   : This controller will receive Login, Registration, Reset Password , Change Password Request. Which will come from User end
/// Creation Date : 26th September 2020

/// ==================================================================================================
///  || Modification History ||
///  -------------------------------------------------------------------------------------------------
///  Sl No. Date:           Author:                     Ver: Area of Change:
///  01     
/// **************************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainStation_BE.BLL;
using BS_Interfaces;
using BS_Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace BrainStation_BE.Controllers
{
    //[EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthentication _iAuthentication;
        public AuthenticationController(IAuthentication iAuthentication)
        {
            _iAuthentication = iAuthentication;
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        [Route("Register")]
        public IActionResult CreateUser(UserInfo objUserInfo)
        {
            string vout = string.Empty;
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();

            #region Check validation if every required field are given
            Validation objValidation = new Validation();
            vout = objValidation.ValidateUserRegistration(objUserInfo);
            #endregion
            if (vout.Contains("OK"))
            {
                DateTime utcDate = DateTime.UtcNow;

                objUserInfo.ActionDate = utcDate;
                objUserInfo.ActionType = "INSERT";

                response = _iAuthentication.CreateUser(objUserInfo);
            }

            return Ok(response);
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel objLoginModel)
        {
            string vout = string.Empty;
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();

            #region Check validation if username and password are given
            Validation objValidation = new Validation();
            vout = objValidation.ValidateLoginCredential(objLoginModel);
            #endregion

            if (vout.Contains("OK"))
            {
                response = _iAuthentication.ValidateAsync(objLoginModel);
                if (response.Status.Contains("OK"))
                {
                    string usercode = _iAuthentication.GetUserId(objLoginModel.UserName);
                    List<RoleInfo> objRoleInfoList = _iAuthentication.GetUserRoleList(objLoginModel.UserName);

                    vout = "Login Successfully";
                    //Api response object
                    response.Status = "OK";
                    response.Message = "Login Successfully";
                    response.Result = new
                    {
                        message = vout,
                        UserID = usercode,
                        Email = objLoginModel.UserName,
                        Roles = objRoleInfoList
                    };
                    return Ok(response);
                }
                else
                {
                    //Api response object
                    response.Status = "FAILED";
                    response.Message = "Incalid User Name or Password";
                    response.Result = null;

                    return Ok(response);
                }
            }
            else
            {
                //Api response object
                response.Status = "FAILED";
                response.Message = vout;
                response.Result = null;

                return Ok(response);
            }
        }
    }
}
