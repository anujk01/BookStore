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
    public class AddressRepository : IAddressRepository
    {
        private readonly IMongoCollection<AddressModel> Address;

        private readonly IConfiguration configuration;

        public AddressRepository(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Address = database.GetCollection<AddressModel>("Address");
        }


        public async Task<AddressModel> AddAddress(AddressModel addAddress)
        {
            try
            {
                var ifExists = await this.Address.Find(x => x.AddressID == addAddress.AddressID).SingleOrDefaultAsync();
                if (ifExists == null)
                {
                    await this.Address.InsertOneAsync(addAddress);
                    return addAddress;
                }
                return null;
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
                var result = await this.Address.Find(x => x.AddressID == updateAddress.AddressID).FirstOrDefaultAsync();
                if (result != null)
                {
                    await this.Address.UpdateOneAsync(x => x.AddressID == updateAddress.AddressID,
                        Builders<AddressModel>.Update.Set(x => x.AddressTypeID, updateAddress.AddressTypeID)
                        .Set(x => x.FullAddress, updateAddress.FullAddress)
                        .Set(x => x.City, updateAddress.City)
                        .Set(x => x.State, updateAddress.State)
                        .Set(x => x.PinCode, updateAddress.PinCode));
                        return result;
                }
                else
                {
                    await this.Address.InsertOneAsync(updateAddress);
                    return updateAddress;
                }
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
                return Address.Find(FilterDefinition<AddressModel>.Empty).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
