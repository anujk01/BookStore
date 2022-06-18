using ModelLayer;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interface
{
    public interface IBookManager
    {
        Task<BookModel> AddBook(BookModel addBook);
        Task<BookModel> UpdateBook(BookModel updateBook);
        Task<bool> DeleteBook(BookModel deleteBook);
        IEnumerable<BookModel> GetAllBook();
        BookModel GetBookByID(string bookID);
    }
}
