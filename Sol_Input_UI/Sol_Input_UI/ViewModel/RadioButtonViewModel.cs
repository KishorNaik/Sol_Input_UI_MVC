using Sol_Input_UI.Models.RadioButton;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Input_UI.ViewModel
{
    public class RadioButtonViewModel
    {
        //public string SelectedCategory { get; set; }
        public CategoryModel Category { get; set; }

        public List<CategoryModel> ListCategory { get; set; }
    }
}