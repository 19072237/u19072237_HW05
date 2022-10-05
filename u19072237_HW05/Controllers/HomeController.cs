using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using u19072237_HW05.ViewModels;
using u19072237_HW05.Models;



namespace u19072237_HW05.Controllers
{

    public class HomeController : Controller
    {

        //Displaying Data--------------------------------------------------------------------------------
        private DataService dataService = DataService.getInstance();

        public ActionResult ViewBooks()
        {         
            List<Book> DBbooks = dataService.GetBooks();
            List<Author> DBauthors = dataService.GetAuthors();
            List<Typ> DBtypes = dataService.GetTypes();
            String DBstatus = dataService.GetStatus(DBbooks.Select(b => b.BookID).FirstOrDefault());

            List <BookVM> Booksview = new List<BookVM>();

            BookVM createBooks(Book book)
            {
                BookVM books = new BookVM()
                {
                    Book = book,
                    Author = DBauthors.Where(a => a.AuthorID == book.AutherID).FirstOrDefault(),
                    Type = DBtypes.Where(t => t.TypeID == book.TypeID).FirstOrDefault(),
                    Status = DBstatus
                };
                return books;
            }

            foreach (var book in DBbooks)
            {
                Booksview.Add(createBooks(book));
            }

            return View(Booksview);
        }

        public ActionResult ViewBookDetails(int bookId)
        {
            List<Borrow> DBborrows = dataService.GetBorrows();
            List<Student> DBstudents = dataService.GetStudents();
            List<Book> DBbooks = dataService.GetBooks();

            BorrowsVM borrows = new BorrowsVM
            {
                Borrows = DBborrows.Where(a => a.BookID == bookId).ToList(),
                Students = DBstudents,
                Book = DBbooks.Where(b=>b.BookID == bookId).FirstOrDefault(),
            };

            return View(borrows);
                 
        }

        public ActionResult ViewStudents(int bookId)
        {
            List<Student> DBstudents = dataService.GetStudents().ToList();
            List<Book> BDbooks = dataService.GetBooks().Where(b => b.BookID == bookId).ToList();

            StudentVM students = new StudentVM()
            {
                Students = DBstudents,
                Books = BDbooks.FirstOrDefault(),
            };

            return View(students);
        }



        // Search Books Queries------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        public ActionResult SearchBook(string BookName, string AuthorName, string TypeName)
        {
            List<Book> DBbooks = dataService.GetBooks();
            List<Author> DBauthors = dataService.GetAuthors();
            List<Typ> DBtypes = dataService.GetTypes();
            

            List <BookVM> Booksview = new List<BookVM>();

            BookVM createBooks(Book book)
            {
                BookVM books = new BookVM()
                {
                    Book = book,
                    Author = DBauthors.Where(a => a.AuthorID == book.AutherID).FirstOrDefault(),
                    Type = DBtypes.Where(t => t.TypeID == book.TypeID).FirstOrDefault(),
                    
                };
                return books;
            }


            if (BookName.Length > 0 && AuthorName.Length == 0 && TypeName.Length == 0)
            {

                foreach (var book in DBbooks.Where(b => b.Name.Contains(BookName) || BookName == null).ToList())
                {
                    Booksview.Add(createBooks(book));
                }
            }

            else if (BookName.Length > 0  && AuthorName.Length >0 && TypeName.Length == 0)
            {

                foreach (var book in DBbooks.Where(b => b.AutherID == DBauthors.Where(a => a.Surname == AuthorName).FirstOrDefault().AuthorID).Where(b => b.Name.Contains(BookName)).ToList())
                {
                    Booksview.Add(createBooks(book));
                }

            }

            else if (BookName.Length>0 && AuthorName.Length>0 && TypeName.Length>0)
            {

                foreach (var book in DBbooks.Where(b => b.AutherID == DBauthors.Where(a => a.Surname == AuthorName).FirstOrDefault().AuthorID).Where(b => b.TypeID == DBtypes.Where(a => a.Name == TypeName).FirstOrDefault().TypeID).Where(b => b.Name.Contains(BookName)).ToList())
                {
                    Booksview.Add(createBooks(book));
                }

            }

            else if (BookName.Length ==0 && AuthorName.Length >0 && TypeName.Length ==0)
            {

                foreach (var book in DBbooks.Where(b => b.AutherID == DBauthors.Where(a => a.Surname == AuthorName).FirstOrDefault().AuthorID).Where(b => b.Name.Contains(BookName)).ToList())
                {
                    Booksview.Add(createBooks(book));
                }
            }

            else if (BookName.Length == 0 && AuthorName.Length == 0 && TypeName.Length >0)
            {

                foreach (var book in DBbooks.Where(b => b.TypeID == DBtypes.Where(a => a.Name == TypeName).FirstOrDefault().TypeID).Where(b => b.Name.Contains(BookName)).ToList())
                {
                    Booksview.Add(createBooks(book));
                }

            }

            else if (BookName.Length >0 && AuthorName.Length == 0 && TypeName.Length > 0)
            {

                foreach (var book in DBbooks.Where(b => b.TypeID == DBtypes.Where(a => a.Name == TypeName).FirstOrDefault().TypeID).Where(b => b.Name.Contains(BookName)).ToList())
                {
                    Booksview.Add(createBooks(book));
                }

            }

            else if (BookName.Length == 0 && AuthorName.Length > 0 && TypeName.Length > 0)
            {

                foreach (var book in DBbooks.Where(b => b.AutherID == DBauthors.Where(a => a.Surname == AuthorName).FirstOrDefault().AuthorID).Where(b => b.TypeID == DBtypes.Where(a => a.Name == TypeName).FirstOrDefault().TypeID))
                {
                    Booksview.Add(createBooks(book));
                }

            }

            return View("ViewBooks", Booksview);

        }



        //Search Students-----------
        public  ActionResult SearchStudents(string StudentName, string ClassName)
        {
            List<Student> DBstudents = dataService.GetStudents().ToList();
            List<Book> BDbooks = dataService.GetBooks();
            StudentVM student1 = null;



            if (StudentName.Length>0 && ClassName.Length == 0)
            {
                student1 = new StudentVM()
                {
                    Books= BDbooks.FirstOrDefault(),
                    Students = DBstudents.Where(s=>s.Name.Contains(StudentName)).ToList(),
                };


            }
            else if( StudentName.Length==0 && ClassName.Length > 0)
            {
               student1 = new StudentVM()
                {
                   Books = BDbooks.FirstOrDefault(),
                   Students = DBstudents.Where(s => s.Class.Contains(ClassName)).ToList(),
               };
            }

            else if (StudentName.Length >0 && StudentName.Length > 0)
            {
                student1 = new StudentVM()
                {
                    Books = BDbooks.FirstOrDefault(),
                    Students = DBstudents.Where(s => s.Name.Contains(StudentName)).Where(s=>s.Class.Contains(ClassName)).ToList(),
                };

            }

            return View("ViewStudents", student1);

            
           

           
           

           
        }

       










    }
}