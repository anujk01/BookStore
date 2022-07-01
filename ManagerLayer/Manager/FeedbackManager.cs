using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Manager
{
    public class FeedbackManager : IFeedbackManager
    {
        private readonly IFeedbackRepository manager;
        public FeedbackManager(IFeedbackRepository manager)
        {
            this.manager = manager;

        }


        public async Task<FeedbackModel> AddFeedback(FeedbackModel addFeedback)
        {
            try
            {
                return await this.manager.AddFeedback(addFeedback);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<FeedbackModel> GetAllFeedback()
        {
            try
            {
                return this.manager.GetAllFeedback();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
