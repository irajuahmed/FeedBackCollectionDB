using BS_Interfaces;
using BS_Models;
using ConnectionGateway;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace BS_DataContext
{
    public class NewsfeedDC : INewsfeed
    {
        IDBContext _habibDbContext;
        public NewsfeedDC(IDBContext habibDbContext)
        {
            _habibDbContext = habibDbContext;
        }

        public ResponseApi<dynamic> GetPostWithDetails()
        {
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();
            List<UserPost> userPostList = new List<UserPost>();
           

            string vComTxt = @"SELECT PostCode,P.UserCode,P.ActionDate,P.ActionType,Post,UserFullname
                                FROM UserPost P
								JOIN UserInfo U ON P.usercode = U.UserCode
								WHERE P.ActionType <> 'DELETE' ORDER BY P.ActionDate DESC";
            
            SqlConnection connection = _habibDbContext.GetConn();
            connection.Open();
            SqlDataReader dr;
            SqlCommand objDbCommand = new SqlCommand(vComTxt, connection);

            dr = objDbCommand.ExecuteReader();
            while (dr.Read())
            {
                UserPost objApplicationInfo = new UserPost();

                objApplicationInfo.PostCode = dr["PostCode"].ToString();
                objApplicationInfo.UserCode = dr["UserCode"].ToString();
                objApplicationInfo.ActionDate = dr.GetDateTime(dr.GetOrdinal("ActionDate"));
                objApplicationInfo.ActionType = dr["ActionType"].ToString();
                objApplicationInfo.Post = dr["Post"].ToString();
                objApplicationInfo.UserFullName = dr["UserFullname"].ToString();
                objApplicationInfo.CommentsList = GetCommentByPostCode(objApplicationInfo.PostCode, connection);
                userPostList.Add(objApplicationInfo);
                
            }
            dr.Close();
            response.Status = "OK";
            response.Message = "Found";
            response.Result = userPostList;

            return response;
        }

        public List<UserComment> GetCommentByPostCode(string pPostCode, SqlConnection connection)
        {
            List<UserComment> commentList = new List<UserComment>();
            

            string vComTxt = @"select CommentsCode,C.UserCode,C.ActionDate,C.ActionType,Comments,PostCode,U.UserFullName
            from UserComments C 
			JOIN UserInfo U ON C.usercode = U.UserCode
			where PostCode = '" + pPostCode + "' AND C.ActionType <> 'DELETE' ORDER BY C.ActionDate DESC";

            SqlCommand objDbCommand = new SqlCommand(vComTxt, connection);
            SqlDataReader dr;
            dr = objDbCommand.ExecuteReader();
            while (dr.Read())
            {
                UserComment obj = new UserComment();
                obj.CommentsCode = dr["CommentsCode"].ToString();
                obj.UserCode = dr["UserCode"].ToString();
                obj.ActionDate = dr.GetDateTime(dr.GetOrdinal("ActionDate"));
                obj.ActionType = dr["ActionType"].ToString();
                obj.Comments = dr["Comments"].ToString();
                obj.PostCode = dr["PostCode"].ToString();
                obj.UserFullName = dr["UserFullname"].ToString();
                obj.VotetypeList = GetVotetypeByCommentsCode(obj.CommentsCode, connection);
                commentList.Add(obj);

            }
            dr.Close();
            return commentList;

        }

        public UserVote GetVotetypeByCommentsCode(string pCommentsCode, SqlConnection connection)
        {
            UserVote obj = new UserVote();
            string vComTxt = @"SELECT DISTINCT (SELECT Count(VoteType) FROM UserVotes WHERE CommentsCode = '"+pCommentsCode+"' AND VoteType = 1) Liked" +
                " ,(SELECT Count(VoteType) FROM UserVotes WHERE CommentsCode = '" + pCommentsCode + "' AND VoteType = 2) Disliked " +
                "from UserVotes where CommentsCode = '" + pCommentsCode + "'";

            SqlCommand objDbCommand = new SqlCommand(vComTxt, connection);
            SqlDataReader dr;
            dr = objDbCommand.ExecuteReader();
            if (dr.Read())
            {
                //obj = new UserVote();
                obj.LikedCount = Convert.ToInt32(dr["Liked"].ToString());
                obj.DislikedCount = Convert.ToInt32(dr["Disliked"].ToString());

            }

            dr.Close();
            return obj;

        }

        public ResponseApi<dynamic> UserComment(UserComment objPost)
        {
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();
            int vResult1 = 0;

            SqlConnection connection = _habibDbContext.GetConn();
            connection.Open();

            string sSqlUser = @"INSERT INTO UserComments(CommentsCode,UserCode,ActionDate,ActionType,Comments,PostCode)
            VALUES(@CommentsCode,@UserCode,@ActionDate,@ActionType,@Comments,@PostCode)";

            SqlCommand sqluser = new SqlCommand(sSqlUser, connection);
            Guid userId = Guid.NewGuid();
            sqluser.Parameters.AddWithValue("CommentsCode", userId);
            sqluser.Parameters.AddWithValue("UserCode", objPost.UserCode);
            sqluser.Parameters.AddWithValue("ActionDate", objPost.ActionDate);
            sqluser.Parameters.AddWithValue("ActionType", objPost.ActionType);
            sqluser.Parameters.AddWithValue("Comments", objPost.Comments);
            sqluser.Parameters.AddWithValue("PostCode", objPost.PostCode);

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                sqluser.Transaction = transaction;
                try
                {
                    vResult1 = sqluser.ExecuteNonQuery();
                    if (vResult1 > 0)
                    {
                        transaction.Commit();
                        response.Status = "OK";
                        response.Message = "Comments Submitted Successfully";
                        response.Result = null;
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

            return response;
        }

        public ResponseApi<dynamic> UserPost(UserPost objPost)
        {
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();
            int vResult1 = 0;

            SqlConnection connection = _habibDbContext.GetConn();
            connection.Open();

            string sSqlUser = @"INSERT INTO UserPost(PostCode,UserCode,ActionDate,ActionType,Post)
                    VALUES(@PostCode,@UserCode,@ActionDate,@ActionType,@Post)";

            SqlCommand sqluser = new SqlCommand(sSqlUser, connection);
            Guid userId = Guid.NewGuid();
            sqluser.Parameters.AddWithValue("PostCode", userId);
            sqluser.Parameters.AddWithValue("UserCode", objPost.UserCode);
            sqluser.Parameters.AddWithValue("ActionDate", objPost.ActionDate);
            sqluser.Parameters.AddWithValue("ActionType", objPost.ActionType);
            sqluser.Parameters.AddWithValue("Post", objPost.Post);

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                sqluser.Transaction = transaction;
                try
                {
                    vResult1 = sqluser.ExecuteNonQuery();
                    if (vResult1 > 0)
                    {
                        transaction.Commit();
                        response.Status = "OK";
                        response.Message = "Post Submitted Successfully";
                        response.Result = null;
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

            return response;
        }

        public ResponseApi<dynamic> UserVote(UserVote objPost)
        {
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();
            int vResult1 = 0;

            SqlConnection connection = _habibDbContext.GetConn();
            connection.Open();

            string sSqlUser = @"INSERT INTO UserVotes(VotesCode,UserCode,ActionDate,ActionType,VoteType,CommentsCode)
                    VALUES(@VotesCode,@UserCode,@ActionDate,@ActionType,@VoteType,@CommentsCode)";

            SqlCommand sqluser = new SqlCommand(sSqlUser, connection);
            Guid userId = Guid.NewGuid();
            sqluser.Parameters.AddWithValue("VotesCode", userId);
            sqluser.Parameters.AddWithValue("UserCode", objPost.UserCode);
            sqluser.Parameters.AddWithValue("ActionDate", objPost.ActionDate);
            sqluser.Parameters.AddWithValue("ActionType", objPost.ActionType);
            sqluser.Parameters.AddWithValue("VoteType", objPost.VoteType);
            sqluser.Parameters.AddWithValue("CommentsCode", objPost.CommentsCode);

            using (SqlTransaction transaction = connection.BeginTransaction())
            {
                sqluser.Transaction = transaction;
                try
                {
                    vResult1 = sqluser.ExecuteNonQuery();
                    if (vResult1 > 0)
                    {
                        transaction.Commit();
                        response.Status = "OK";
                        response.Message = "Votes Submitted Successfully";
                        response.Result = null;
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

            return response;
        }
    }
}
