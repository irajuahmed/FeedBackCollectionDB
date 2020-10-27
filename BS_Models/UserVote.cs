using System;
using System.Collections.Generic;
using System.Text;

namespace BS_Models
{
    public class UserVote
    {
        public DateTime ActionDate { get; set; }
        public string VotesCode { get; set; }
        public string CommentsCode { get; set; }

        public string UserCode { get; set; }
        public string ActionType { get; set; }
        public int VoteType { get; set; } //VoteType 1 = Like, 2 = Dislike
        public int LikedCount { get; set; } 
        public int DislikedCount { get; set; } 
    }
}
