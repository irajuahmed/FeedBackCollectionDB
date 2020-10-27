using BS_Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BS_Interfaces
{
    public interface INewsfeed
    {
        ResponseApi<dynamic> UserPost(UserPost objPost);
        ResponseApi<dynamic> UserComment(UserComment objPost);
        ResponseApi<dynamic> UserVote(UserVote objPost);
        ResponseApi<dynamic> GetPostWithDetails();
    }
}
