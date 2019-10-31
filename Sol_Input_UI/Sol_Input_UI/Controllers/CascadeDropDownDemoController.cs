using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sol_Input_UI.Models.CascadeDropDown;
using Sol_Input_UI.ViewModel;

namespace Sol_Input_UI.Controllers
{
    public class CascadeDropDownDemoController : Controller
    {
        #region Constructor

        public CascadeDropDownDemoController()
        {
            CascadeDropdownVM = new CascadeDropdownViewModel();
        }

        #endregion Constructor

        #region Property

        [BindProperty]
        public CascadeDropdownViewModel CascadeDropdownVM { get; set; }

        #endregion Property

        private async Task<List<CountryModel>> GetCountryData()
        {
            return await Task.Run(() =>
            {
                var countryList = new List<CountryModel>();
                countryList.Add(new CountryModel()
                {
                    CountryId = 1,
                    CountryName = "India"
                });
                countryList.Add(new CountryModel()
                {
                    CountryId = 2,
                    CountryName = "Japan"
                });

                return countryList;
            });
        }

        private async Task BindCountry()
        {
            var countryList = await this.GetCountryData();

            CascadeDropdownVM
                .ListCountry = (
                                        countryList
                                        ?.AsEnumerable()
                                        ?.Select((leCountryModel) => new SelectListItem()
                                        {
                                            Value = Convert.ToString(leCountryModel.CountryId),
                                            Text = leCountryModel.CountryName
                                        })
                                        ?.ToList()

                    );
        }

        private async Task<List<CityModel>> GetCityData()
        {
            return await Task.Run(() =>
            {
                var cityList = new List<CityModel>();
                cityList.Add(new CityModel()
                {
                    CityId = 1,
                    CityName = "Mumbai",
                    CountryId = 1
                });
                cityList.Add(new CityModel()
                {
                    CityId = 2,
                    CityName = "Delhi",
                    CountryId = 1
                });

                cityList.Add(new CityModel()
                {
                    CityId = 3,
                    CityName = "Tokyo",
                    CountryId = 2
                });

                cityList.Add(new CityModel()
                {
                    CityId = 4,
                    CityName = "Kobe",
                    CountryId = 2
                });

                return cityList;
            });
        }

        private async Task BindCity(int countryId)
        {
            var cityList = (await this.GetCityData())
                                ?.Where((leCityModel) => leCityModel.CountryId == countryId)
                                ?.ToList();

            CascadeDropdownVM
                .ListCity = (
                                    cityList
                                    ?.AsEnumerable()
                                    ?.Select((leCityModel) => new SelectListItem()
                                    {
                                        Value = Convert.ToString(leCityModel.CityId),
                                        Text = leCityModel.CityName
                                    })
                                    ?.ToList()

                        );
        }

        private async Task<List<LocationModel>> GetLocationData()
        {
            return await Task.Run(() =>
            {
                var locationList = new List<LocationModel>();
                locationList.Add(new LocationModel()
                {
                    LocationId = 1,
                    LocationName = "Dadar",
                    CityId = 1
                });
                locationList.Add(new LocationModel()
                {
                    LocationId = 2,
                    LocationName = "Borivali",
                    CityId = 1
                });
                locationList.Add(new LocationModel()
                {
                    LocationId = 3,
                    LocationName = "Karol Bhag",
                    CityId = 2
                });
                locationList.Add(new LocationModel()
                {
                    LocationId = 4,
                    LocationName = "Vasant Kunj",
                    CityId = 2
                });
                locationList.Add(new LocationModel()
                {
                    LocationId = 5,
                    LocationName = "Shibuya",
                    CityId = 3
                });
                locationList.Add(new LocationModel()
                {
                    LocationId = 6,
                    LocationName = "Minato",
                    CityId = 3
                });
                locationList.Add(new LocationModel()
                {
                    LocationId = 7,
                    LocationName = "Shine Kobe",
                    CityId = 4
                });
                locationList.Add(new LocationModel()
                {
                    LocationId = 8,
                    LocationName = "Tanigami",
                    CityId = 4
                });

                return locationList;
            });
        }

        private async Task BindLocation(int cityId)
        {
            var locationList = (
                                        await this.GetLocationData()
                                    )
                                    ?.AsEnumerable()
                                    ?.Where((leLocationModel) => leLocationModel.CityId == cityId)
                                    ?.ToList();

            CascadeDropdownVM
                .ListLocation =
                            (
                                locationList
                                ?.AsEnumerable()
                                ?.Select((leLocationModel) => new SelectListItem()
                                {
                                    Value = Convert.ToString(leLocationModel.LocationId),
                                    Text = leLocationModel.LocationName
                                })
                                ?.ToList()

                            );
        }

        #region Action

        public async Task<IActionResult> Index()
        {
            // Bind Country
            await this.BindCountry();

            return View(CascadeDropdownVM);
        }

        [HttpPost]
        public async Task<IActionResult> OnCountryChange()
        {
            // Bind Country
            await this.BindCountry();

            // Bind Selected Country Id in Country Model where After Postack View Will hold Selected DropDown Value
            //CascadeDropdownVM.Country = new CountryModel();
            //CascadeDropdownVM.Country.CountryId = CascadeDropdownVM.Country.CountryId;

            // Bind City
            await this.BindCity(CascadeDropdownVM.Country.CountryId);

            return View("Index", CascadeDropdownVM);
        }

        [HttpPost]
        public async Task<IActionResult> onCityChange()
        {
            // Bind Country
            await this.BindCountry();

            // Bind City
            await this.BindCity(CascadeDropdownVM.Country.CountryId);

            // Bind Location
            await this.BindLocation(CascadeDropdownVM.City.CityId);

            return View("Index", CascadeDropdownVM);
        }

        #endregion Action
    }
}