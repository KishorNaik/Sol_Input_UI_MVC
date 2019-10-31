using Microsoft.AspNetCore.Mvc.Rendering;
using Sol_Input_UI.Models.DropDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Input_UI.ViewModel
{
    public class DropDownDemoViewModel
    {
        public DropDownDemoModel DropDownDemo { get; set; }

        public List<SelectListItem> ListDropDownDemo { get; set; }
    }
}