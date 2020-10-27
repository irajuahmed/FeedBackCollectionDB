using System;
using System.Collections.Generic;
using System.Text;

namespace BS_Models
{
    public class UserComment
    {
        public DateTime ActionDate { get; set; }
        public string CommentsCode { get; set; }
        public string PostCode { get; set; }

        public string UserCode { get; set; }
        public string ActionType { get; set; }
        public string Comments { get; set; }
        public string UserFullName { get; set; }

        public UserVote VotetypeList;
    }
}
