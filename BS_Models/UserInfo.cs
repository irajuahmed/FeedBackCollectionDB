using System;
using System.Collections.Generic;

namespace BS_Models
{
    public class UserInfo
    {

        public string UserID { get; set; }

        public string UserName { get; set; }
        public string DomainUserName { get; set; }
        public string Email { get; set; }
        public string DomainEmail { get; set; }
        public string Password { get; set; }
        public int CompanyCode { get; set; }
        public int WorkingUnitCode { get; set; }

        public DateTime ActionDate { get; set; }
        public string ActionType { get; set; }

        public string PasswordAnswer { get; set; }

        public string UserFullName { get; set; }

        public List<UserInRole> UserInRole_VW { get; set; }
        public int UserType { get; set; }//1 = Authority Role, 2 = Student

    }
}
