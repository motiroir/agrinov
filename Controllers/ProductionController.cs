using System;
using System.Collections.Generic;
using System.Diagnostics;
using AgriNov.Models;
using AgriNov.Models.ProductionModel;
using AgriNov.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AgriNov.Models.SharedStatus;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Reflection;



namespace AgriNov.Controllers
{
    public class ProductionController : Controller
    {

        private readonly BDDContext _DBContext;
        private readonly ServiceProduction sP = new ServiceProduction();

        [HttpGet]
        public IActionResult CreateProduction()
        {
            return View();
        }


        [HttpPost]
        public IActionResult CreateProduction(Production production)
        {
            if (ModelState.IsValid)
            {
                
                    this.sP.InsertProduction(production);
                    return RedirectToAction("Index", "Home");
                

            }

            return View(production);
        }

        [HttpGet]
        public IActionResult UpdateProduction(int id)
        {
            if (id > 0)
            {
               
                Production production = sP.GetProductions().FirstOrDefault(production => production.Id == id);

                if (production != null)
                {
                    return View(production);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public IActionResult UpdateProduction(int id, Production production)
        {
            if (id != production.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                production.DateLastModified = DateTime.Now;

                this.sP.UpdateProduction(production);
                this.sP.Save();

                return RedirectToAction("Index", "Home");
            }

            return View(production);
        }

        


    }
}
