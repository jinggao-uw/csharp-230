using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LearningCenter.Website.Models
{
    public class ClassListViewModel
    {
        public ClassListViewModel()
        {
        }

        [Display(Name = "Selected class")]
        public int SelectedClass { get; set; }

        public ClassModel[] classes;
    }
}