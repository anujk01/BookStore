using ManagerLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookManager books;

        public BookController(IBookManager books)
        {
            this.books = books;
        }


        [HttpPost]
        [Route("addBook")]

        public async Task<IActionResult> AddBook([FromBody] BookModel addBook)
        {
            try
            {
                var result = await this.books.AddBook(addBook);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<BookModel> { Status = true, Message = "Book Added Succesfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Book Not Added" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }



        [HttpPut]
        [Route("updateBook")]

        public async Task<IActionResult> UpdateBook([FromBody] BookModel updateBook)
        {
            try
            {
                var result = await this.books.UpdateBook(updateBook);
                if (result != null)
                {
                    return this.Ok(new ResponseModel<BookModel> { Status = true, Message = "Book Updated Successfully", Data = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Update Failed" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpDelete]
        [Route("deleteBook")]
        public async Task<IActionResult> DeleteBook(BookModel deleteBook)
        {
            try
            {
                bool result = await this.books.DeleteBook(deleteBook);
                if (result == true)
                {
                    return this.Ok(new ResponseModel<BookModel> { Status = true, Message = "Book Deleted Successfully" });
                }
                else
                {
                    return this.BadRequest(new ResponseModel<BookModel> { Status = false, Message = "Book Not Deleted" });
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        [Route("getAllBooks")]

        public IEnumerable<BookModel> GetAllBook()
        {
            try
            {
                var result = this.books.GetAllBook();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        [HttpGet]
        [Route("getBookByID")]
        public BookModel GetBookByID(string bookID)
        {
            try
            {
                var result = this.books.GetBookByID(bookID);
                if(result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

    }
}
