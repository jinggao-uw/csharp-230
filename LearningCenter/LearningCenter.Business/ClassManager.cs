using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCenter.Repository;

namespace LearningCenter.Business
{
    public interface IClassManager
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

    public class ClassManager : IClassManager
    {
        private readonly IClassRepository classRepository;

        public ClassManager(IClassRepository classRepository)
        {
            this.classRepository = classRepository;
        }
        
        public ClassModel[] ListAll()
        {
            var allClasses = classRepository.ListAll();

            var newArray = Array.ConvertAll(allClasses, (c) => {
                return new ClassModel()
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    ClassDescription = c.ClassDescription,
                    ClassPrice = c.ClassPrice
                };
            });

            return newArray;
        }
    }
}
