using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StorageMVC2.Models
{
    public class DropdownViewModel 
    {
        [Display(Name = "choose product")]
        public string SelectedProductName { get; set; }

        public IEnumerable<Product> product { get; set; }

        public IEnumerable<SelectListItem> SelectList { get; set; }
    }
}

