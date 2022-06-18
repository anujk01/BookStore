using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Manager
{
    public class WishlistManager : IWishlistManager
    {
        private readonly IWishlistRepository manager;
        public WishlistManager(IWishlistRepository manager)
        {
            this.manager = manager;

        }

        public async Task<WishlistModel> AddToWishlist(WishlistModel addToWishlist)
        {
            try
            {
                return await this.manager.AddToWishlist(addToWishlist);
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
                return await this.manager.DeleteWishlist(deleteWishlist);
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
                return this.manager.GetAllWishlist();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
