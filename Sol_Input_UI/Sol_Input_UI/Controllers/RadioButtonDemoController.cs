using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sol_Input_UI.Models.RadioButton;
using Sol_Input_UI.ViewModel;

namespace Sol_Input_UI.Controllers
{
    public class RadioButtonDemoController : Controller
    {
        #region Constructor

        public RadioButtonDemoController()
        {
            RadioButtonVM = new RadioButtonViewModel();
        }

        #endregion Constructor

        #region Property

        [BindProperty]
        public RadioButtonViewModel RadioButtonVM { get; set; }

        #endregion Property

        #region Private Methds

        private async Task<List<CategoryModel>> GetCategory()
        {
            return await Task.Run(() =>
            {
                var categoryList = new List<CategoryModel>();
                categoryList.Add(new CategoryModel() { CategoryId = 1, CategoryName = "Samsung" });

                categoryList.Add(new CategoryModel() { CategoryId = 2, CategoryName = "IPhone" });

                categoryList.Add(new CategoryModel() { CategoryId = 3, CategoryName = "Microsoft" });

                return categoryList;
            });
        }

        private async Task BindCategory()
        {
            var categoryList = await this.GetCategory();

            RadioButtonVM.ListCategory = categoryList;
        }

        private async Task SearchCategory(int categoryId)
        {
            var categoryList = await GetCategory();

            ViewBag.Data = categoryList
                                ?.AsEnumerable()
                                ?.FirstOrDefault((leCategoryModel) => leCategoryModel.CategoryId == categoryId)
                                ?.CategoryName;
        }

        #endregion Private Methds

        #region Action

        public async Task<IActionResult> Index()
        {
            await this.BindCategory();

            return View(RadioButtonVM);
        }

        [HttpPost]
        public async Task<IActionResult> OnSubmit()
        {
            await this.BindCategory();

            await this.SearchCategory(RadioButtonVM.Category.CategoryId);

            return View("Index", RadioButtonVM);
        }

        #endregion Action
    }
}