using Microsoft.AspNetCore.Mvc.Rendering;
using Sol_Input_UI.Models.CascadeDropDown;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sol_Input_UI.ViewModel
{
    public class CascadeDropdownViewModel
    {
        public CountryModel Country { get; set; }

        public List<SelectListItem> ListCountry { get; set; }

        public CityModel City { get; set; }

        public List<SelectListItem> ListCity { get; set; }

        public LocationModel Location { get; set; }

        public List<SelectListItem> ListLocation { get; set; }
    }
}