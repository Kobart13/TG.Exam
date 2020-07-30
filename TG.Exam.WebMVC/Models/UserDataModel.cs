using System;
using System.ComponentModel.DataAnnotations;

namespace TG.Exam.WebMVC.Models
{
    public class UserDataModel
    {
        public User UserData { get; set; }

        [Display(Name = "Fetch method")]
        public String FetchMethod { get; set; }
    }
}
