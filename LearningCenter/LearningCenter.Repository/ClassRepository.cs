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
        public ClassModel[] ListAll()
        {
            return DatabaseAccessor.Instance.Classes.Select(t =>
                                                   new ClassModel
                                                   {
                                                       ClassId = t.ClassId,
                                                       ClassName = t.ClassName,
                                                       ClassDescription = t.ClassDescription,
                                                       ClassPrice = t.ClassPrice
                                                   })
                       .ToArray();
        }
    }
}
