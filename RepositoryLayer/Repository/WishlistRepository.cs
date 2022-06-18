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
    public class WishlistRepository : IWishlistRepository
    {
        private readonly IMongoCollection<WishlistModel> Wishlist;

        private readonly IConfiguration configuration;

        public WishlistRepository(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Wishlist = database.GetCollection<WishlistModel>("Wishlist");
        }


        public async Task<WishlistModel> AddToWishlist(WishlistModel addToWishlist)
        {
            try
            {
                var result = await this.Wishlist.Find(x => x.WishlistID == addToWishlist.WishlistID).SingleOrDefaultAsync();
                if (result == null)
                {
                    await this.Wishlist.InsertOneAsync(addToWishlist);
                    return addToWishlist;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteWishlist(WishlistModel deleteWishlist)
        {
            try
            {
                var result = await this.Wishlist.FindOneAndDeleteAsync(x => x.WishlistID == deleteWishlist.WishlistID);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<WishlistModel> GetAllWishlist()
        {
            try
            {
                return Wishlist.Find(FilterDefinition<WishlistModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
