using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IWishlistManager
    {
        Task<WishlistModel> AddToWishlist(WishlistModel addToWishlist);
        Task<bool> DeleteWishlist(WishlistModel deleteWishlist);
        IEnumerable<WishlistModel> GetAllWishlist();
    }
}
