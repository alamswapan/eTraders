using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace LILI_CRM.Web.Areas.PCV.ViewModel
{
    public class ArticleSubCategoryModel: BaseViewModel
    {

        [DisplayName("Article Category")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Article Category")]
        public long CategoryId { get; set; }
        public string ArticleCategory { get; set; }
        
        [DisplayName("Sub Category")]
        [Required(ErrorMessage = "Sub-Category Name is required.")]
        [MaxLength(100, ErrorMessage = "Exceeding maximum length.")]
        public string SubCategoryName { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Sub-Category Description is required.")]
        [MaxLength(200, ErrorMessage = "Exceeding maximum length.")]
        public string Description { get; set; }

        
        public IEnumerable<SelectListItem> ddlArticleCategory { get; set; }

    }
}