using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrainStation_BE.BLL;
using BS_Interfaces;
using BS_Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BrainStation_BE.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class NewsfeedController : ControllerBase
    {
        private readonly INewsfeed _iAuthentication;
        public NewsfeedController(INewsfeed iAuthentication)
        {
            _iAuthentication = iAuthentication;
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        [Route("Post")]
        public IActionResult Post(UserPost objPost)
        {
            string vout = string.Empty;
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();

            #region Check validation if every required field are given
            Validation objValidation = new Validation();
            vout = objValidation.ValidatePost(objPost);
            #endregion
            if (vout.Contains("OK"))
            {
                DateTime utcDate = DateTime.UtcNow;

                objPost.ActionDate = utcDate;
                objPost.ActionType = "INSERT";

                response = _iAuthentication.UserPost(objPost);
            }

            return Ok(response);
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        [Route("Comment")]
        public IActionResult Comment(UserComment objPost)
        {
            string vout = string.Empty;
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();

            #region Check validation if every required field are given
            Validation objValidation = new Validation();
            vout = objValidation.ValidateComment(objPost);
            #endregion
            if (vout.Contains("OK"))
            {
                DateTime utcDate = DateTime.UtcNow;

                objPost.ActionDate = utcDate;
                objPost.ActionType = "INSERT";

                response = _iAuthentication.UserComment(objPost);
            }

            return Ok(response);
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpPost]
        [Route("Vote")]
        public IActionResult Vote(UserVote objPost)
        {
            string vout = string.Empty;
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();

            #region Check validation if every required field are given
            Validation objValidation = new Validation();
            vout = objValidation.ValidVote(objPost);
            #endregion
            if (vout.Contains("OK"))
            {
                DateTime utcDate = DateTime.UtcNow;

                objPost.ActionDate = utcDate;
                objPost.ActionType = "INSERT";

                response = _iAuthentication.UserVote(objPost);
            }
            else
            {
                response.Status = "FAILED";
                response.Message = vout;
                response.Result = null;
            }

            return Ok(response);
        }

        [EnableCors("AllowSpecificOrigin")]
        [HttpGet]
        [Route("GetPostWithDetails")]
        public IActionResult GetPostWithDetails()
        {
            ResponseApi<dynamic> response = new ResponseApi<dynamic>();
            UserPost objApplicationInfo = new UserPost();
            response = _iAuthentication.GetPostWithDetails();
            return Ok(response);
        }
    }
}
