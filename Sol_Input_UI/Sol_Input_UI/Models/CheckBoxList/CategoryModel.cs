using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Input_UI.Models.CheckBoxList
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }

        public String CategoryName { get; set; }

        public bool SelectedCheckBox { get; set; }
    }
}