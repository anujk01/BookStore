using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Manager
{
    public class AddressManager : IAddressManager
    {
        private readonly IAddressRepository manager;
        public AddressManager(IAddressRepository manager)
        {
            this.manager = manager;

        }


        public async Task<AddressModel> AddAddress(AddressModel addAddress)
        {
            try
            {
                return await this.manager.AddAddress(addAddress);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<AddressModel> UpdateAddress(AddressModel updateAddress)
        {
            try
            {
                return await this.manager.UpdateAddress(updateAddress);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<AddressModel> GetAddress()
        {
            try
            {
                return this.manager.GetAddress();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

