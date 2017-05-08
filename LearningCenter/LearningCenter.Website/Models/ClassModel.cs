using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LearningCenter.Website.Models
{
    public class ClassModel : System.Web.Mvc.SelectListItem
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassDescription { get; set; }
        public decimal ClassPrice { get; set; }

        public ClassModel()
        {
        }

        public ClassModel(int id, string name, string desc, decimal price)
        {
            ClassId = id;
            ClassName = name;
            ClassDescription = desc;
            ClassPrice = price;
        }
    }
}