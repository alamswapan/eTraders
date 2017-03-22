using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class ArticleCategoryModel: BaseViewModel
    {

        [DisplayName("Article Category")]
        [Required(ErrorMessage = "Category Name is required.")]
        [MaxLength(100, ErrorMessage = "Exceeding maximum length.")]
        public string CategoryName { get; set; }

        [DisplayName("Description")]
        [MaxLength(200, ErrorMessage = "Exceeding maximum length.")]
        public string Description { get; set; }

    }
  }