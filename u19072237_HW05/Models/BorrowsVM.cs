using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using u19072237_HW05.Models;
using System.Linq.Expressions;

namespace u19072237_HW05.ViewModels
{
    public class BorrowsVM
    {
        private Random Gen = new Random();

        public List<Borrow> Borrows { get; set; }

        public List<Student> Students { get; set; }

        public Book Book { get; set; }


        public String GetStudent(int StudentId)
        {
            List<Student> students = Students.Where(s => s.StudentID == StudentId).ToList();
            return students[Gen.Next(students.Count)].Name + " " + students[Gen.Next(students.Count)].Surname;
        }


    }
}