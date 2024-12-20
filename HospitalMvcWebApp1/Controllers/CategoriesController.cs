using HospitalMvcWebApp1.Models;
using Microsoft.AspNetCore.Mvc;

namespace HospitalMvcWebApp1.Controllers
{
    public class CategoriesController : Controller
    {
        public ActionResult GetMembers()
        {
            IEnumerable<DoctorsRegistrationModel> doctors = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7258/api/");

                  
                var responseTask = client.GetAsync("Doctors");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<DoctorsRegistrationModel>>();
                    readTask.Wait();

                    doctors = readTask.Result;
                }
                else
                {
                    //Error response received   
                    doctors = Enumerable.Empty<DoctorsRegistrationModel>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(doctors);
        }
       

    }
}
