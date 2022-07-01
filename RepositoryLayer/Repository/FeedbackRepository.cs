using Microsoft.Extensions.Configuration;
using ModelLayer;
using MongoDB.Driver;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IMongoCollection<FeedbackModel> Feedback;

        private readonly IConfiguration configuration;

        public FeedbackRepository(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Feedback = database.GetCollection<FeedbackModel>("Feedback");
        }

        public async Task<FeedbackModel> AddFeedback(FeedbackModel addFeedback)
        {
            try
            {
                var ifExists = await this.Feedback.Find(x => x.FeedbackID == addFeedback.FeedbackID).SingleOrDefaultAsync();
                if (ifExists == null)
                {
                    await this.Feedback.InsertOneAsync(addFeedback);
                    return addFeedback;
                }
                return null;
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
                return Feedback.Find(FilterDefinition<FeedbackModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
