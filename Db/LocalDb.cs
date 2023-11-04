using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp2.Models;

namespace WpfApp2.Db
{
    internal class LocalDb
    {
        public LocalDb()
        {
            Init();
        }

        private List<Student> Students;

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            Students = new List<Student>();
            for (int i = 0; i<5; i++)
            {
                Students.Add(new Student()
                {
                    Id = i,
                    Name = $"Sample{i}"
                }) ;
            }
        }

        public List<Student> GetStudents()
        {
            return Students;
        }

        public void AddStudent(Student student)
        {
            if(student.Id == 0 && this.GetStudentById(student.Id) != null)
            {
                int maxId = Students.OrderByDescending(x => x.Id).First().Id;
                student.Id = maxId + 1;
            }
            Students.Add(student);
        }

        public void DelStudent(int studentId)
        {
            var model = Students.FirstOrDefault(x => x.Id == studentId);
            if (model != null)
            {
                Students.Remove(model);
            }
        }

        public List<Student> GetStudentsByName(string name)
        {
            return Students.Where(q => q.Name.Contains(name)).ToList();
        }

        public Student GetStudentById(int id)
        {
            var model = Students.FirstOrDefault(t => t.Id == id);
            if (model != null)
            {
                return new Student()
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
            return null;
        }

        public int GetNextId()
        {
            return Students.OrderByDescending(x =>x.Id).First().Id + 1;
        }
    }
}
