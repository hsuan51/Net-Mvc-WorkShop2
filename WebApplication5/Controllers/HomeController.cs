using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost()]
        public JsonResult Index(Models.bookSearchArgs args)
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getSearchBookData(args);
            return Json(result);
        }
        [HttpPost()]
        public JsonResult SearchBookClassName()
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getBookClassName();
            return Json(result);
        }
        [HttpPost()]
        public JsonResult SearchBookKeeperName()
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getBookKeeperName();
            return Json(result);
        }
        [HttpPost()]
        public JsonResult SearchBookStatusName()
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getBookStatusName();
            return Json(result);
        }
        [HttpPost()]
        public JsonResult InsertBook(Models.bookSearchArgs args)
        {
            Models.bookService bookService = new Models.bookService();
            var resultBookId = bookService.insertBook(args);
            var result = bookService.getOneBookData(resultBookId);
            return Json(result);
        }
        [HttpPost()]
        public JsonResult DeleteBook(Models.book book)
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.deleteBook(book.BOOK_ID);
            return Json(result);
        }
        [HttpPost()]
        public JsonResult UpdateBook(Models.bookSearchArgs args)
        {
            Models.bookService bookService = new Models.bookService();
            var resultBookId = bookService.updateBook(args);
            var result = bookService.getOneBookData(resultBookId);
            return Json(result);
        }
        [HttpPost()]
        public JsonResult SearchOneBook(Models.book book)
        {
            Models.bookService bookService = new Models.bookService();
            var result = bookService.getOneBookData(book.BOOK_ID);
            return Json(result);
        }
    }
}