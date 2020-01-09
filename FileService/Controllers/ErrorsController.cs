using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FileService.Controllers
{
    public class ErrorsController : Controller
    {

        [Route("Exception")]
        public IActionResult Exception()
        {
            TempData["message"] = "Došlo je do neočekivane greške.";

            return RedirectToAction("Errors");
        }

        [Route("ErrorCode/{statusCode}")]
        public IActionResult SomeError(int statusCode)
        {
            if(statusCode == 404)
                TempData["message"] = "Ne postoji stranica.";
            else
                TempData["message"] = "Došlo je do neočekivane greške.";

            return RedirectToAction("Errors");
        }

        [Route("Errors")]
        public IActionResult SendError(string ErrorMessage)
        {
            TempData["message"] = ErrorMessage;

            return RedirectToAction("Errors");
        }

        // metoda za prikazivanje poruka
        [Route("Error")]
        public IActionResult Errors()
        {
            ViewBag.ErrorMessage = TempData["message"];

            return View();
        }




    }


}