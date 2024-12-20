using HospitalMvcWebApp1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace HospitalMvcWebApp1.Controllers
{
    public class HospitalRegister : Controller
    {

        public ActionResult GetHospital()
        {
            IEnumerable<Hospitalregistration> Hospital = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7258/api/Registration");

                //Called Member default GET All records  
                //GetAsync to send a GET request   
                // PutAsync to send a PUT request  
                var responseTask = client.GetAsync("Registration");
                responseTask.Wait();

                //To store result of web api response.   
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<Hospitalregistration>>();
                    readTask.Wait();

                    Hospital = readTask.Result;
                }
                else
                {
                    //Error response received   
                    Hospital = Enumerable.Empty<Hospitalregistration>();
                    ModelState.AddModelError(string.Empty, "Server error try after some time.");
                }
            }
            return View(Hospital);


        }


          public IActionResult Create ()
          {
            return View();
          }


        [HttpPost]
        public IActionResult create(Hospitalregistration hospital)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7258/api/Registration");

                //HTTP POST
                var postTask = client.PostAsJsonAsync<Hospitalregistration>("Registration", hospital);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("GetHospital");
                }
            }

            ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

            return View(hospital);
        }

        //public IActionResult Update()
        //{
        //    return View();
        //}


        //[HttpPut]
        //public IActionResult update(Hospitalregistration hospital)
        //{
        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:7258/api/Registration");

        //        //HTTP POST
        //        var putTask = client.PutAsJsonAsync<Hospitalregistration>("Registration", hospital);
        //        putTask.Wait();

        //        var result = putTask.Result;
        //        if (result.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("GetHospital");
        //        }
        //    }

        //    ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

        //    return View(hospital);
        //}



        [HttpGet]
        public ActionResult Edit(Guid id)
        {
           

           Hospitalregistration Hospital = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7258");
                var endpoint = $"/api/Registration/{id}";
               
               
                var responseTask = client.GetAsync(endpoint);
                responseTask.Wait();

               
                var result = responseTask.Result;

                //If success received   
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<Hospitalregistration>();
                    readTask.Wait();

                    Hospital = readTask.Result;
                }
               
            }
            return View(Hospital);

        }


        [HttpPost]
        public IActionResult Edit( Hospitalregistration hospital)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return View(hospital);
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7258/api/Registration");

                try
                {
                    // HTTP PUT to update existing hospital registration
                    var putTask = client.PutAsJsonAsync<Hospitalregistration>($"Registration/{hospital.Id}", hospital);
                    putTask.Wait();

                    var result = putTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Hospital details updated successfully.";
                        return RedirectToAction("GetHospital");
                    }
                    else
                    {
                        // Handle unsuccessful response
                        ModelState.AddModelError(string.Empty, "Failed to update hospital details. Please try again.");
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception and add a user-friendly error message
                    ModelState.AddModelError(string.Empty, "An error occurred while updating hospital details.");
                }
            }

            // If we got this far, something failed
            return View(hospital);
        }


    }
}
