using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sol_Input_UI.Models.CheckBoxList;
using Sol_Input_UI.ViewModel;

namespace Sol_Input_UI.Controllers
{
    public class CheckBoxListDemoController : Controller
    {
        #region Constructor

        public CheckBoxListDemoController()
        {
            CheckBoxListVM = new CheckBoxListViewModel();
        }

        #endregion Constructor

        #region Property

        [BindProperty]
        public CheckBoxListViewModel CheckBoxListVM { get; set; }

        #endregion Property

        #region Private Method

        private async Task<List<CategoryModel>> GetCategory()
        {
            return await Task.Run(() =>
            {
                var categoryList = new List<CategoryModel>();
                categoryList.Add(new CategoryModel() { CategoryId = 1, CategoryName = "Samsung", SelectedCheckBox = false });

                categoryList.Add(new CategoryModel() { CategoryId = 2, CategoryName = "IPhone", SelectedCheckBox = false });

                categoryList.Add(new CategoryModel() { CategoryId = 3, CategoryName = "Microsoft", SelectedCheckBox = false });

                return categoryList;
            });
        }

        private async Task BindCategory()
        {
            var categoryList = await this.GetCategory();

            CheckBoxListVM.CategoryList = categoryList;
        }

        #endregion Private Method

        #region Action

        public async Task<IActionResult> Index()
        {
            await this.BindCategory();

            return View(CheckBoxListVM);
        }

        [HttpPost]
        public IActionResult OnSubmit()
        {
            //Get Selected CheckBox Data
            var categoryList = CheckBoxListVM.CategoryList;

            ViewBag.Data = categoryList;

            return View("Index", CheckBoxListVM);
        }

        #endregion Action
    }
}