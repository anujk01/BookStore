using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Interface
{
    public interface IBookRepository
    {
        Task<BookModel> AddBook(BookModel addBook);
        Task<BookModel> UpdateBook(BookModel updateBook);
        Task<bool> DeleteBook(BookModel deleteBook);
        IEnumerable<BookModel> GetAllBook();
        BookModel GetBookByID(string bookID);
    }
}
