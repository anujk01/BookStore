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
    public class BookRepository : IBookRepository
    {
        private readonly IMongoCollection<BookModel> Books;

        private readonly IConfiguration configuration;

        public BookRepository(IDBSetting db, IConfiguration configuration)
        {
            this.configuration = configuration;
            var client = new MongoClient(db.ConnectionString);
            var database = client.GetDatabase(db.DatabaseName);
            Books = database.GetCollection<BookModel>("Books");
        }

        public async Task<BookModel> AddBook(BookModel addBook)
        {
            try
            {
                var ifExists = await this.Books.Find(x => x.BookID == addBook.BookID).SingleOrDefaultAsync();
                if(ifExists == null)
                {
                    await this.Books.InsertOneAsync(addBook);
                    return addBook;
                }
                return null;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BookModel> UpdateBook(BookModel updateBook)
        {
            try
            {
                var ifExists = await this.Books.Find(x => x.BookID == updateBook.BookID).FirstOrDefaultAsync();
                if(ifExists !=null)
                {
                    await this.Books.UpdateOneAsync(x => x.BookID == updateBook.BookID,
                        Builders<BookModel>.Update.Set(x => x.BookName, updateBook.BookName)
                        .Set(x => x.Description, updateBook.Description)
                        .Set(x => x.AuthorName, updateBook.AuthorName)
                        .Set(x => x.Rating, updateBook.Rating)
                        .Set(x => x.TotalRating, updateBook.TotalRating)
                        .Set(x => x.DiscountPrice, updateBook.DiscountPrice));
                    return ifExists;
                }
                else
                {
                    await this.Books.InsertOneAsync(updateBook);
                    return updateBook;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> DeleteBook(BookModel deleteBook)
        {
            try
            {
                var ifExists = await this.Books.FindOneAndDeleteAsync(x => x.BookID == deleteBook.BookID);
                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BookModel> GetAllBook()
        {
            try
            {
                return Books.Find(FilterDefinition<BookModel>.Empty).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public BookModel GetBookByID(string bookID)
        {
            try
            {
                return Books.Find(x => x.BookID == bookID).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
