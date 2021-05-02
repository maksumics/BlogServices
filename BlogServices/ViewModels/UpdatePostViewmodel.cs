using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BlogServices.ViewModels
{
    public class UpdatePostViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
    }
}