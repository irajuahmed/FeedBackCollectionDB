using BS_Interfaces;
using BS_Models;
using ConnectionGateway;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BS_DataContext
{
    public class AuthenticationDC : IAuthentication
    {
        IDBContext _habibDbContext;
        public AuthenticationDC(IDBContext habibDbContext)
        {
            _habibDbContext = habibDbContext;
        }

        public ResponseApi<dynamic> CreateUser(UserInfo objUserInfo)
        {
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();
            int vResult = 0;
            int vResult1 = 0;
            int vResult2 = 0;
            int vResult3 = 0;
            bool vResult4 = false;
            //string connectionstring = GetDefaultConnectionString();

            string vOut = string.Empty; ;
            StringBuilder vComText = new StringBuilder();
            string vComText1 = string.Empty;
            string existinguser = string.Empty;

            Hashtable htExistingRoles = new Hashtable();
            Hashtable htNewUserRoleList = new Hashtable();

            SqlConnection connection = _habibDbContext.GetConn();
            connection.Open();

            existinguser = GetUserId(objUserInfo.UserName);
            if (String.IsNullOrEmpty(existinguser))
            {
                MembershipProvider MembershipProvider = new MembershipProvider();
                string passwordSaltedText = MembershipProvider.GeneratePasswordSaltingText();

                string vPass = objUserInfo.Password;
                string saltedPassword = MembershipProvider.SaltText(vPass, passwordSaltedText);
                string saltedPasswordAnswer = MembershipProvider.SaltText(objUserInfo.PasswordAnswer, passwordSaltedText);
                string sSqlUser = @"INSERT INTO [dbo].[User] 
                                    (UserId,UserName,
                                    UserCode,ActionDate,ActionType)
                                    VALUES
                                    (@UserId,@UserName,
                                    @UserCode,@ActionDate,@ActionType)";

                SqlCommand sqluser = new SqlCommand(sSqlUser, connection);
                Guid userId = Guid.NewGuid();
                sqluser.Parameters.AddWithValue("UserId", userId);
                sqluser.Parameters.AddWithValue("UserName", objUserInfo.UserName);
                sqluser.Parameters.AddWithValue("UserCode", Guid.NewGuid());
                sqluser.Parameters.AddWithValue("ActionDate", objUserInfo.ActionDate);
                sqluser.Parameters.AddWithValue("ActionType", "Insert");

                string sSqlMembership = @"INSERT INTO Membership
                                        (UserId,IsLockedOut,IsFirstLogin,
                                        LastLoginDate,LastPasswordChangeDate,FailedPassAtmptCount,
                                        LastLockoutDate,FailedPassAnsAtmptCount,PasswordSalt,Email,
                                        PasswordQuestion,PasswordAnswer,
                                        UserCode,ActionDate,ActionType)
                                        VALUES
                                        (@UserId,@IsLockedOut,@IsFirstLogin,
                                        @LastLoginDate,@LastPasswordChangeDate,@FailedPassAtmptCount,
                                        @LastLockoutDate,@FailedPassAnsAtmptCount,@PasswordSalt,@Email,
                                        @PasswordQuestion,@PasswordAnswer,
                                        @UserCode,@ActionDate,@ActionType)
                                        ";


                SqlCommand sqlmembership = new SqlCommand(sSqlMembership, connection);
                sqlmembership.Parameters.AddWithValue("UserId", userId.ToString());
                sqlmembership.Parameters.AddWithValue("IsLockedOut", 0);
                sqlmembership.Parameters.AddWithValue("IsFirstLogin", 1);
                sqlmembership.Parameters.AddWithValue("LastLoginDate", new DateTime(1800, 1, 1));
                sqlmembership.Parameters.AddWithValue("LastPasswordChangeDate", new DateTime(1800, 1, 1));
                sqlmembership.Parameters.AddWithValue("FailedPassAtmptCount", 0);
                sqlmembership.Parameters.AddWithValue("LastLockoutDate", new DateTime(1800, 1, 1));
                sqlmembership.Parameters.AddWithValue("FailedPassAnsAtmptCount", 0);
                sqlmembership.Parameters.AddWithValue("PasswordSalt", MembershipProvider.EncodeToBase64String(passwordSaltedText));
                sqlmembership.Parameters.AddWithValue("Email", objUserInfo.Email);
                sqlmembership.Parameters.AddWithValue("PasswordQuestion", "abc");
                sqlmembership.Parameters.AddWithValue("PasswordAnswer", MembershipProvider.EncodeText(saltedPasswordAnswer));
                sqlmembership.Parameters.AddWithValue("UserCode", Guid.NewGuid());
                sqlmembership.Parameters.AddWithValue("ActionDate", DateTime.Now);
                sqlmembership.Parameters.AddWithValue("ActionType", "Insert");

                string sSqlUserPassword = @"INSERT INTO UserPassword
                                            (UserId,Password,
                                            UserCode,ActionDate,ActionType)
                                            VALUES
                                            (@UserId,@Password,
                                            @UserCode,@ActionDate,@ActionType)";

                SqlCommand sqlpassword = new SqlCommand(sSqlUserPassword, connection);

                sqlpassword.Parameters.AddWithValue("UserId", userId.ToString());
                sqlpassword.Parameters.AddWithValue("Password", MembershipProvider.EncodeText(saltedPassword));
                sqlpassword.Parameters.AddWithValue("UserCode", Guid.NewGuid());
                sqlpassword.Parameters.AddWithValue("ActionDate", DateTime.Now);
                sqlpassword.Parameters.AddWithValue("ActionType", "Insert");

                vComText.Append("INSERT INTO UserInfo (UserId,UserCode,ActionDate,ActionType,UserFullName)");
                vComText.Append(" VALUES");
                vComText.Append("(@UserId,@UserCode,@ActionDate,@ActionType,@UserFullName)");


                SqlCommand sqlCommand = new SqlCommand(vComText.ToString(), connection);


                sqlCommand.Parameters.AddWithValue("@UserId", userId);
                sqlCommand.Parameters.AddWithValue("UserCode", userId.ToString());
                sqlCommand.Parameters.AddWithValue("ActionDate", objUserInfo.ActionDate);
                sqlCommand.Parameters.AddWithValue("ActionType", "Insert");
                sqlCommand.Parameters.AddWithValue("UserFullName", objUserInfo.UserFullName);


                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    sqluser.Transaction = transaction;
                    sqlCommand.Transaction = transaction;
                    sqlmembership.Transaction = transaction;
                    sqlpassword.Transaction = transaction;
                    try
                    {
                        vResult1 = sqluser.ExecuteNonQuery();
                        if (vResult1 > 0)
                        {
                            vResult = sqlCommand.ExecuteNonQuery();
                            if (vResult > 0)
                            {
                                vResult2 = sqlmembership.ExecuteNonQuery();
                                if (vResult2 > 0)
                                {
                                    vResult3 = sqlpassword.ExecuteNonQuery();
                                    if (vResult3 > 0)
                                    {
                                        vResult4 = AddUsersToRoles(objUserInfo, userId, connection, transaction);
                                        //vResult4 = sqlrole.ExecuteNonQuery();
                                        if (vResult4 == true)
                                        {
                                            transaction.Commit();
                                            response.Status = "OK";
                                            response.Message = "Registration Successfully";
                                            response.Result = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        response.Status = "FAILED";
                        response.Message = ex.Message;
                        response.Result = null;
                    }
                    finally
                    {
                        connection.Dispose();
                        connection.Close();
                    }
                }
            }
            else
            {
                response.Status = "FAILED";
                response.Message = "This user Id already exists!";
                response.Result = null;
            }
            
            return response;
        }

        public string GetUserId(string userName)
        {
            string userId = string.Empty;
            string sSql = @"SELECT UserId
							FROM [dbo].[User]                             
                            WHERE UserName = @UserName";


            SqlConnection connection = _habibDbContext.GetConn();
            SqlDataReader dr;
            connection.Open();
            SqlCommand sqluserid = new SqlCommand(sSql, connection);
            sqluserid.Parameters.AddWithValue("UserName", userName);
            try
            {
                dr = sqluserid.ExecuteReader();
                while (dr.Read())
                {
                    userId = Convert.ToString(dr["UserId"]);
                }
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return userId;
        }

        public static bool AddUsersToRoles(UserInfo objUserInfo, Guid userId, SqlConnection connection, SqlTransaction transaction)
        {
            int vResult = 0;
            bool vOut = false;
            string sSqlUserRole = @"INSERT INTO UserInRole
                                            (UserCode,ActionDate,
                                            ActionType,UserId,RoleId,UserRoleId,IsDeleted)
                                            VALUES
                                            (@UserCode,@ActionDate,@ActionType,@UserId,@RoleID,@UserRoleId,
                                            @IsDeleted)";

            if (objUserInfo.UserType == 1) //1 = systemadmin
            {
                SqlCommand sqlrole = new SqlCommand(sSqlUserRole, connection);
                sqlrole.Parameters.AddWithValue("UserCode", userId.ToString());
                sqlrole.Parameters.AddWithValue("ActionDate", DateTime.Now);
                sqlrole.Parameters.AddWithValue("ActionType", "INSERT");
                sqlrole.Parameters.AddWithValue("UserId", userId.ToString());
                sqlrole.Parameters.AddWithValue("RoleID", "EA3FDBAE-CC08-4FE0-8EB6-95DB14985D49"); //SysAdmin Role Code
                sqlrole.Parameters.AddWithValue("UserRoleId", Guid.NewGuid());
                sqlrole.Parameters.AddWithValue("IsDeleted", 0);
                sqlrole.Transaction = transaction;
                vResult = sqlrole.ExecuteNonQuery();
            }
            else if(objUserInfo.UserType == 2) //2 = Basic User
            {
                SqlCommand sqlrole = new SqlCommand(sSqlUserRole, connection);
                sqlrole.Parameters.AddWithValue("UserCode", userId.ToString());
                sqlrole.Parameters.AddWithValue("ActionDate", DateTime.Now);
                sqlrole.Parameters.AddWithValue("ActionType", "Insert");
                sqlrole.Parameters.AddWithValue("UserId", userId.ToString());
                sqlrole.Parameters.AddWithValue("RoleID", "0025FA6B-BC0D-4989-8A0A-F5B4200E9915"); //Basic User Role Code
                sqlrole.Parameters.AddWithValue("UserRoleId", Guid.NewGuid());
                sqlrole.Parameters.AddWithValue("IsDeleted", 0);
                sqlrole.Transaction = transaction;
                vResult = sqlrole.ExecuteNonQuery();
            }
            if (vResult > 0)
            {
                vOut = true;
            }
            return vOut;
        }

        public ResponseApi<dynamic> ValidateAsync(LoginModel context)
        {
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();
            string vOut = ": Exception Occured !";
            string saltedText = string.Empty;
            string saltpassword = string.Empty;
            string concatpassword = string.Empty;
            string checkpassword = string.Empty;
            int isActive = 0;
            MembershipProvider lsMembershipProvider = new MembershipProvider();
            SqlConnection connection = _habibDbContext.GetConn();
            connection.Open();
            SqlDataReader dr;
            saltedText = GetPasswordSalt(context.UserName);
            string sSql = @"SELECT Password,ISNULL(IsLockedOut,0) IsLockedOut
                            FROM UserPassword
                            INNER JOIN Membership
                            ON UserPassword.UserId = Membership.UserId
                            INNER JOIN [dbo].[User]
                            ON UserPassword.UserId = [dbo].[User].UserId
                            WHERE UserName = @UserName";

            SqlCommand sqlPassword = new SqlCommand(sSql, connection);
            sqlPassword.Parameters.AddWithValue("UserName", context.UserName);

            dr = sqlPassword.ExecuteReader();
            while (dr.Read())
            {
                isActive = Convert.ToInt32(dr["IsLockedOut"].ToString());
                saltpassword = Convert.ToString(dr["Password"]);
            }
            checkpassword = lsMembershipProvider.EncodeText(lsMembershipProvider.SaltText(context.Password, saltedText));

            if (saltpassword == checkpassword)
            {

                if (isActive < 1)
                {
                    //vOut = "";
                    response.Status = "OK";
                    response.Message = "Login Successfully";
                    response.Result = null;
                }
                else if (isActive == 1)
                {
                    //vOut = "Invalid user";
                    response.Status = "FAILED";
                    response.Message = "Invalid user";
                    response.Result = null;
                }
            }
            else
            {
                response.Status = "FAILED";
                response.Message = "Invalid user";
                response.Result = null;
            }

            connection.Close();
            return response;

        }

        public string GetPasswordSalt(string username)
        {
            string vOut = ": Exception Occured !";
            string saltedText = string.Empty;
            MembershipProvider lsMembershipProvider = new MembershipProvider();
            string sSqlsalt = @"SELECT Membership.PasswordSalt
                            FROM [dbo].[User]
                            INNER Join Membership
                            ON [dbo].[User].UserId = Membership.UserId
                            WHERE UserName = @UserName";
            SqlConnection connection = _habibDbContext.GetConn();
            connection.Open();
            SqlDataReader dr;
            SqlCommand command = new SqlCommand(sSqlsalt, connection);
            command.Parameters.AddWithValue("UserName", username);
            try
            {
                dr = command.ExecuteReader();
                while (dr.Read())
                {
                    saltedText = Convert.ToString(dr["PasswordSalt"]);
                }
            }
            catch (Exception ex)
            {

                vOut = "Password salt not found";
                throw ex;
            }
            saltedText = lsMembershipProvider.DecodeFromBase64String(saltedText);

            return saltedText;

        }

        public List<RoleInfo> GetUserRoleList(string userName)
        {
            List<RoleInfo> objRoleInfoList = new List<RoleInfo>();
            RoleInfo objRoleInfo;
            string sSql = @"SELECT DISTINCT Role.RoleId, Role.RoleName FROM Role INNER JOIN UserInRole ON Role.RoleId = UserInRole.RoleId INNER JOIN [dbo].[User] ON UserInRole.UserId = @UserId and UserInRole.IsDeleted ='0'  ORDER BY RoleName";

            SqlConnection connection = _habibDbContext.GetConn();
            SqlDataReader dr;
            connection.Open();
            SqlCommand sqlRole = new SqlCommand(sSql, connection);
            sqlRole.Parameters.AddWithValue("UserId", GetUserId(userName));
            try
            {
                dr = sqlRole.ExecuteReader();
                while (dr.Read())
                {
                    objRoleInfo = new RoleInfo();
                    objRoleInfo.RoleId_PK = dr["RoleId"].ToString();
                    objRoleInfo.RoleName = dr["RoleName"].ToString();
                    objRoleInfoList.Add(objRoleInfo);
                }
            }
            catch (FormatException ex)
            {
                throw new FormatException(ex.Message);
            }
            finally
            {
                connection.Close();
            }


            return objRoleInfoList;
        }

        
    }
}
