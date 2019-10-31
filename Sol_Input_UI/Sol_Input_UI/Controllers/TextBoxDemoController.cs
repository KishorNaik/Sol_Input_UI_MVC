using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sol_Input_UI.Models;
using Sol_Input_UI.ViewModel;

namespace Sol_Input_UI.Controllers
{
    public class TextBoxDemoController : Controller
    {
        #region Property

        [BindProperty]
        public UserViewModel UserVM { get; set; }

        #endregion Property

        #region Private Method

        private void BindUserModel()
        {
            UserVM = new UserViewModel()
            {
                Users = new TextBoxDemoModel()
            };

            UserVM.Users.FirstName = "Kishor";
            UserVM.Users.LastName = "Naik";
        }

        #endregion Private Method

        #region Actions

        public IActionResult Index()
        {
            BindUserModel();

            return View(UserVM);
        }

        public IActionResult OnSubmit()
        {
            return View("Index");
        }

        #endregion Actions
    }
}