using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sol_Input_UI.Models.JqueryDataTable;
using Newtonsoft.Json;

namespace Sol_Input_UI.Controllers
{
    public class JqueryDataTableController : Controller
    {

        #region Private Methods
        private async Task<IEnumerable<CustomerModel>> GetCustomerDataAsync()
        {
            try
            {
                return await Task.Run(() => {

                    var customerList = new List<CustomerModel>();
                    customerList.Add(new CustomerModel()
                    {
                        customerId = 1,
                        firstName = "Kishor",
                        lastName = "Naik"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 2,
                        firstName = "Darshan",
                        lastName = "Bordekar"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 3,
                        firstName = "Uday",
                        lastName = "Singh"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 4,
                        firstName = "Hitendra",
                        lastName = "Kohle"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 5,
                        firstName = "Shyam",
                        lastName = "Badgujjar"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 6,
                        firstName = "Priyanka",
                        lastName = "Shinde"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 7,
                        firstName = "Ashwini",
                        lastName = "Parmar"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 8,
                        firstName = "Sai",
                        lastName = "Yermal"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 9,
                        firstName = "Neha",
                        lastName = "Dhotre"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 10,
                        firstName = "Madhuri",
                        lastName = "M"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 11,
                        firstName = "Amruta",
                        lastName = "M"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 12,
                        firstName = "Ritu",
                        lastName = "Panchal"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 13,
                        firstName = "Namrata",
                        lastName = "Koregaonkar"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 14,
                        firstName = "Rajshree",
                        lastName = "Mhatre"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 15,
                        firstName = "Sonal",
                        lastName = "Joshi"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 16,
                        firstName = "Ashwini",
                        lastName = "B"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 16,
                        firstName = "Swati",
                        lastName = "Mane"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 17,
                        firstName = "Ambika",
                        lastName = "Garg"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 18,
                        firstName = "Vijeth",
                        lastName = "Shetty"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 19,
                        firstName = "Mayur",
                        lastName = "Patil"
                    });

                    customerList.Add(new CustomerModel()
                    {
                        customerId = 20,
                        firstName = "Chandan",
                        lastName = "Soni"
                    });


                    return customerList;
                });
            }
            catch(Exception)
            {
                throw;
            }
        }
        #endregion 


        #region Actions
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async  Task<IActionResult> LoadData()
        {

            var draw = Convert.ToInt32( HttpContext.Request.Form["draw"].FirstOrDefault());

            // Skiping number of Rows count
            var start = Request.Form["start"].FirstOrDefault();
            // Paging Length 10,20
            var length =Request.Form["length"].FirstOrDefault();
            // Sort Column Name
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            // Sort Column Direction ( asc ,desc)
            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
            // Search Value from (Search box)
            var searchValue = Request.Form["search[value]"].FirstOrDefault();

            //Paging Size (10,20,50,100)
            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var customerList = await this.GetCustomerDataAsync();

            //Sorting
            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
            {
                //customerList = customerList.OrderBy(sortColumn + " " + sortColumnDirection);


                customerList =
                    customerList
                    ?.AsEnumerable()
                    ?.OrderBy((leOrder) => leOrder.firstName)
                    ?.ToList();
            }
            //Search
            if (!string.IsNullOrEmpty(searchValue))
            {
                customerList = customerList.Where(m => m.firstName == searchValue || m.lastName == searchValue);
            }

            //total number of rows count 
            recordsTotal = customerList.Count();
            //Paging 
            var data = customerList.Skip(skip).Take(pageSize).ToList();
            //var data = customerList;

            //Returning Json Data

            dynamic jsonResult= Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

            return jsonResult;
        }

        #endregion 
    }
}