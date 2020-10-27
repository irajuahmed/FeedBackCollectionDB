using System;
using System.Collections.Generic;
using System.Text;

namespace BS_Models
{
    public class UserPost
    {
        public DateTime ActionDate { get; set; }
        public string PostCode { get; set; }

        public string UserCode { get; set; }
        public string ActionType { get; set; }
        public string Post { get; set; }
        public string UserFullName { get; set; }

        public List<UserComment> CommentsList;
    }
}
