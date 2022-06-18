using ManagerLayer.Interface;
using ModelLayer;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Manager
{
    public class BookManager : IBookManager
    {
        private readonly IBookRepository manager;
        public BookManager(IBookRepository manager)
        {
            this.manager = manager;

        }


        public async Task<BookModel> AddBook(BookModel addBook)
        {
            try
            {
                return await this.manager.AddBook(addBook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BookModel> UpdateBook(BookModel updateBook)
        {
            try
            {
                return await this.manager.UpdateBook(updateBook);
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
                return await this.manager.DeleteBook(deleteBook);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<BookModel> GetAllBook()
        {
            try
            {
                return this.manager.GetAllBook();
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
                return this.manager.GetBookByID(bookID);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
