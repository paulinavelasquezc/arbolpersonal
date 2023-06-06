using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace arboldecisiones.ViewModels
{
    public class TreeMultimediaViewModels
    {
        [Display(Name = "Descripción Multimedia")]
        [Required(ErrorMessage = "Debes ingresar {0}")]
        [StringLength(100, ErrorMessage = "El campo {0} debe estar entre {1} y {2} caracteres", MinimumLength = 5)]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Url { get; set; }

        [Display(Name = "Url:")]
        [Required(ErrorMessage = "Debes ingresar la {0}")]
        public HttpPostedFileBase MultimediaFile { get; set; }
    }
}