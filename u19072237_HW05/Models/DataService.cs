using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using u19072237_HW05.Models;
using u19072237_HW05.ViewModels;


namespace u19072237_HW05.Models
{
    public class DataService
    {
        private static DataService instance;
        public static DataService getInstance()
        {
            if (instance == null)
            {
                instance = new DataService();
            }
            return instance;
        }


        public string ConnectionString = "Data Source=DESKTOP-4V0J98O\\SQLEXPRESS;Initial Catalog=Library;Integrated Security=True";




        public List<Book> GetBooks()
        {
            List<Book> books = new List<Book>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("select * from books", connection))
                {
                    using(SqlDataReader  rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Book book = new Book
                            {
                                BookID = Convert.ToInt32(rdr["bookId"]),
                                Name = Convert.ToString(rdr["name"]),
                                AutherID = Convert.ToInt32(rdr["authorId"]),
                                TypeID = Convert.ToInt32(rdr["typeId"]),
                                PageCount= Convert.ToInt32(rdr["pagecount"]),
                                Point= Convert.ToInt32(rdr["point"]),
                            };
                            books.Add(book); 
                        }   
                    }
                }
                connection.Close();
            }
            return books;
        }




        public List<Author> GetAuthors()
        {
            List<Author> authors = new List<Author>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from authors", connection))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Author author = new Author
                            {
                                AuthorID = Convert.ToInt32(rdr["authorId"]),
                                Name = Convert.ToString(rdr["name"]),
                                Surname = Convert.ToString(rdr["surname"])
                            };
                            authors.Add(author);
                        } 
                    }
                }
                connection.Close();
            }
            return authors;
        }






        public List<Borrow> GetBorrows()
        {
            List<Borrow> borrows = new List<Borrow>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from borrows", connection))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                           Borrow borrow = new Borrow
                            {
                                BorrowID = Convert.ToInt32(rdr["borrowId"]),
                                StudentID = Convert.ToInt32(rdr["studentId"]),
                                BookID = Convert.ToInt32(rdr["bookId"]),
                                TakenDate = Convert.ToDateTime(rdr["takenDate"]),
                                BroughtDate = Convert.ToDateTime(rdr["broughtDate"])
                            };
                            borrows.Add(borrow);
                        }
                    }
                }
                connection.Close();
            }
            return borrows; 
        }





        public List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from students", connection))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                           Student student = new Student
                            {
                                StudentID = Convert.ToInt32(rdr["studentId"]),
                                Name = Convert.ToString(rdr["name"]),
                                Surname = Convert.ToString(rdr["surname"]),
                                BirthDate = Convert.ToDateTime(rdr["birthdate"]),
                                Gender = Convert.ToString(rdr["gender"]),
                                Class = Convert.ToString(rdr["class"]),
                                Point = Convert.ToInt32(rdr["point"])
                            };
                            students.Add(student);
                        }
                    }
                }
                connection.Close();
            }

            return students;

        }

        public List<Typ> GetTypes()
        {
            List<Typ> types = new List<Typ>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("Select * from types", connection))
                {
                    using (SqlDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Typ type = new Typ
                            {
                                TypeID = Convert.ToInt32(rdr["typeId"]),
                                Name = Convert.ToString(rdr["name"])
                            };
                            types.Add(type);
                        }
                    }
                }
                connection.Close();
            }
            return types;

        }



        public string GetStatus(int bookId)
        {
            string Status = null;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("select * from borrows where bookId=" + bookId, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string rdr = reader["broughtDate"].ToString();
                            if (rdr ==  null)
                            {
                                Status = "Out";
                               
                            }

                            else
                            {
                                Status = "Available";
                            }
                        }
                    }
                }
                connection.Close();
            }

            return Status;
        }



        //Borrow Fuction...............................................
        public void BorrowBook(int StudentId, int BookId, int BorowId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            try
            {
                connection.Open();
                SqlCommand myInsertCommand = new SqlCommand("insert into borrows(studentId, bookId, takenDate, broughtDate) VALUES (" + StudentId + "," + BookId + ",'" + DateTime.Now + "','" + null + "')", connection);

                myInsertCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                connection.Close();
            }
        }



        //Return Book Function------------------
        public void ReturnBook(int BorrowId)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            DateTime now = DateTime.Now;
            try
            {
                connection.Open();
                SqlCommand myInsertCommand = new SqlCommand("Update borrows Set broughtDate='" + now + "' WHERE borrowId=" + BorrowId, connection);

                myInsertCommand.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }

            finally
            {
                connection.Close();
            }

        }


    }


}









