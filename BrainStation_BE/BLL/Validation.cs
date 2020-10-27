using BS_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BrainStation_BE.BLL
{
    public class Validation
    {
        #region Registration validation
        internal string ValidateUserRegistration(UserInfo objUserProfle)
        {
            string vMsg = "OK";
            if (string.IsNullOrEmpty(objUserProfle.UserName))
            {
                vMsg = "Please provide the data with correct format in Name field.";
            }

            if (string.IsNullOrEmpty(objUserProfle.Email))
            {
                vMsg = "Please provide the data with correct format in Email Address field..";
            }
            if (string.IsNullOrEmpty(objUserProfle.Password))
            {
                vMsg = "Please provide the data with correct format in Password field.";
            }

            return vMsg;
        }
        #endregion

        #region login validation
        public string ValidateLoginCredential(LoginModel objLogin)
        {
            string vMsg = "OK";
            if (string.IsNullOrEmpty(objLogin.UserName))
            {
                vMsg = "Please input user name";
            }
            if (string.IsNullOrEmpty(objLogin.Password))
            {
                vMsg = "Please input your password";
            }


            return vMsg;
        }
        #endregion

        #region Post validation
        internal string ValidatePost(UserPost objUserProfle)
        {
            string vMsg = "OK";
            if (string.IsNullOrEmpty(objUserProfle.UserCode))
            {
                vMsg = "User Code not found";
            }

            if (string.IsNullOrEmpty(objUserProfle.Post))
            {
                vMsg = "Please provide some post text";
            }
            return vMsg;
        }
        #endregion
        
        #region Comment validation
        internal string ValidateComment(UserComment objUserProfle)
        {
            string vMsg = "OK";
            if (string.IsNullOrEmpty(objUserProfle.UserCode))
            {
                vMsg = "User Code not found";
            }

            if (string.IsNullOrEmpty(objUserProfle.PostCode))
            {
                vMsg = "Please Comment a specific Post";
            }
            if (string.IsNullOrEmpty(objUserProfle.Comments))
            {
                vMsg = "Please provide your comments";
            }
            return vMsg;
        }
        #endregion
        #region Vote validation
        internal string ValidVote(UserVote objUserProfle)
        {
            string vMsg = "OK";
            if (string.IsNullOrEmpty(objUserProfle.UserCode))
            {
                vMsg = "User Code not found";
            }
            if (string.IsNullOrEmpty(objUserProfle.CommentsCode))
            {
                vMsg = "Please Comment a specific Post";
            }
            return vMsg;
        }
        #endregion
    }
}
