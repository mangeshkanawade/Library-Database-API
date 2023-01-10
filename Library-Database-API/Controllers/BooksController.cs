using Library_Database_API.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Database_API.Controllers {
    public class BooksController : ApiController {

        [HttpGet]
        public HttpResponseMessage BooksCategoriesAndPublicationsList() {
            HttpResponseMessage result = null;
            try {
                Books books = new Books();
                BooksViewModel viewModel = new BooksViewModel();
                viewModel.BooksCategoriesList = books.BookCategoriesGetList();
                viewModel.BooksPublicationsList = books.BookPublicationsGetList();
                result = Request.CreateResponse(HttpStatusCode.Created, viewModel);

            } catch (Exception ex) {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        [HttpPost]
        public HttpResponseMessage BooksList(BooksViewModel booksViewModel) {
            HttpResponseMessage result = null;

            try {
                // For Getting Data From Database according to User need

                Books books = new Books();
                books.BookName = booksViewModel.BookName;
                books.BookCategoryId = booksViewModel.BookCategoryId;
                books.BookPublisherId = booksViewModel.BookPublisherId;
                books.PageSize = booksViewModel.PageSize;
                books.PageNumber = booksViewModel.PageNumber;

                // For Sending Data

                BooksViewModel viewModel = new BooksViewModel();
                viewModel.BooksCategoriesList = books.BookCategoriesGetList();
                viewModel.BooksPublicationsList = books.BookPublicationsGetList();
                viewModel.BooksList = books.GetList();
                viewModel.TotalRecords = books.TotalRecords;
                result = Request.CreateResponse(HttpStatusCode.Created, viewModel);

            } catch (Exception ex) {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }


        [HttpPost]
        public HttpResponseMessage AddBook(BooksViewModel booksViewModel) {
            HttpResponseMessage result = null;

            try {
                // For Getting Data From Database according to User need

                Books books = new Books();
                books.BookId = booksViewModel.BookId;
                books.BookName = booksViewModel.BookName;
                books.BookCategoryId = booksViewModel.BookCategoryId;
                books.BookPublisherId = booksViewModel.BookPublisherId;
                books.BookQuantity = booksViewModel.BookQuantity;
                books.IsActive = booksViewModel.IsActive;
                books.Save();

                result = Request.CreateResponse(HttpStatusCode.Created, true);

            } catch (Exception ex) {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
        [HttpPost]
        public HttpResponseMessage GetBookDetails(BooksViewModel booksViewModel) {
            HttpResponseMessage result = null;

            try {
                // For Getting Data From Database according to User need

                Books books = new Books();
                if (booksViewModel.BookId > 0) {
                    books.BookId = booksViewModel.BookId;
                    books.Load();
                }
                result = Request.CreateResponse(HttpStatusCode.Created, books);

            } catch (Exception ex) {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }

        [HttpPost]
        public HttpResponseMessage DeleteBook(BooksViewModel booksViewModel) {
            HttpResponseMessage result = null;

            try {
                // For Getting Data From Database according to User need

                Books books = new Books();
                if (booksViewModel.BookId > 0) {
                    books.BookId = booksViewModel.BookId;
                    books.Load();
                    books.Delete();
                    
                }
                result = Request.CreateResponse(HttpStatusCode.Created, books);

            } catch (Exception ex) {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            return result;
        }
    }
}
