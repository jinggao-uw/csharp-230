using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCenter.Repository
{
    public interface IClassRepository
    {
        ClassModel[] ListAll();
        ClassModel[] ListForUser(int userId);
        bool AddClassForUser(int classId, int userId);
    }

    public class ClassModel
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }
    }
    public class ClassRepository : IClassRepository
    {
        public bool AddClassForUser(int classId, int userId)
        {
            string query = "INSERT INTO UserClass (ClassId, UserID) VALUES(" + classId + ", " + userId + ")";
            try
            {
                DatabaseAccessor.Instance.Database.ExecuteSqlCommand(query);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public ClassModel[] ListAll()
        {
            return DatabaseAccessor.Instance.Classes.Select(t =>
                new ClassModel
                {
                    ClassId = t.ClassId,
                    ClassName = t.ClassName,
                    ClassDescription = t.ClassDescription,
                    ClassPrice = t.ClassPrice
                }).ToArray();
        }

        public ClassModel[] ListForUser(int userId)
        {
            return DatabaseAccessor.Instance.Users.First(t => t.UserId == userId)
                .Classes.Select(t =>
                new ClassModel
                {
                    ClassId = t.ClassId,
                    ClassName = t.ClassName,
                    ClassDescription = t.ClassDescription,
                    ClassPrice = t.ClassPrice
                }).ToArray();
         }
    }
}
