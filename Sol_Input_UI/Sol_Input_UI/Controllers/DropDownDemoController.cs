using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sol_Input_UI.Models.DropDown;
using Sol_Input_UI.ViewModel;

namespace Sol_Input_UI.Controllers
{
    public class DropDownDemoController : Controller
    {
        #region Property

        [BindProperty]
        public DropDownDemoViewModel DropDownDemoVM { get; set; }

        #endregion Property

        #region Private Method

        // Get Country Data
        private async Task<IEnumerable<DropDownDemoModel>> GetCountryData()
        {
            return await Task.Run(() =>
            {
                List<DropDownDemoModel> listDropDownModel = new List<DropDownDemoModel>();
                listDropDownModel.Add(new DropDownDemoModel() { CountryId = 1, CountryName = "India" });
                listDropDownModel.Add(new DropDownDemoModel() { CountryId = 2, CountryName = "Japan" });
                listDropDownModel.Add(new DropDownDemoModel() { CountryId = 3, CountryName = "China" });

                return listDropDownModel;
            });
        }

        private async Task BindCountryDropDown()
        {
            //Get country Data
            var listCountryData = await this.GetCountryData();

            if (DropDownDemoVM == null)
            {
                // Create an instance of DropDowViewModel
                DropDownDemoVM = new DropDownDemoViewModel();
            }

            // Bind Country Data
            DropDownDemoVM
                .ListDropDownDemo = listCountryData
                                            ?.AsEnumerable()
                                            ?.Select((leDropDownDemoModel) => new SelectListItem()
                                            {
                                                Value = Convert.ToString(leDropDownDemoModel.CountryId),
                                                Text = leDropDownDemoModel.CountryName
                                            })
                                            ?.ToList();

            var setDropDownValue = TempData["SetDropDown"] as string;

            if (setDropDownValue == "true")
            {
                DropDownDemoVM.DropDownDemo = new DropDownDemoModel();
                DropDownDemoVM.DropDownDemo.CountryId = 2;
            }
        }

        private async Task<DropDownDemoModel> GetCountryName(int countryId)
        {
            var countryListData = await this.GetCountryData();

            var countryData = countryListData
                                    ?.AsEnumerable()
                                    ?.FirstOrDefault((leDropDownModel) => leDropDownModel.CountryId == countryId);

            return countryData;
        }

        #endregion Private Method

        #region Action

        public async Task<IActionResult> Index()
        {
            // Bind Country Dropw Down List
            await this.BindCountryDropDown();

            return View(DropDownDemoVM);
        }

        [HttpPost]
        public async Task<IActionResult> OnSubmit()
        {
            // Get Selected Drop Down Item from Button Click
            var countryData = await this.GetCountryName(Convert.ToInt32(DropDownDemoVM.DropDownDemo.CountryId));
            ViewBag.CountryName = countryData.CountryName;

            // Bind DropDown
            await this.BindCountryDropDown();

            return View("Index", DropDownDemoVM);
        }

        [HttpPost]
        public IActionResult OnSetDropDown()
        {
            TempData["SetDropDown"] = "true";
            TempData.Keep();

            return RedirectToAction("Index");
        }

        #endregion Action
    }
}