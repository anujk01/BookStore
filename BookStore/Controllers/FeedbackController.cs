using ManagerLayer.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackManager feedback;

        public FeedbackController(IFeedbackManager feedback)
        {
            this.feedback = feedback;
        }


        [HttpPost]
        [Route("addFeedback")]

        public async Task<IActionResult> AddFeedback([FromBody] FeedbackModel addFeedback)
        {
            try
            {
                var result = await this.feedback.AddFeedback(addFeedback);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<FeedbackModel> { Status = true, Message = "Feedback Added Succesfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Feedback Not Added" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getAllFeedback")]

        public IEnumerable<FeedbackModel> GetAllFeedback()
        {
            try
            {
                var result = this.feedback.GetAllFeedback();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
