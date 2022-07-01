using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IAddressRepository
    {
        Task<AddressModel> AddAddress(AddressModel addAddress);
        Task<AddressModel> UpdateAddress(AddressModel updateAddress);
        IEnumerable<AddressModel> GetAddress();
    }
}
