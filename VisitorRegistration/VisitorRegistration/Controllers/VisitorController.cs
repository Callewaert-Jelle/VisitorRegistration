using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using VisitorRegistration.Models.Domain;
using VisitorRegistration.Models.VisitorViewModels;

namespace VisitorRegistration.Controllers
{
    public class VisitorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new VisitorViewModel());
        }

        [HttpPost]
        public IActionResult Register(VisitorViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Visitor visitor = new Visitor();
                    MapViewModelToVisitor(viewModel, visitor);
                    // add to database
                    TempData["message"] = $"Successfully registered: {visitor.LastName} {visitor.Name}";
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Register), viewModel);
        }

        public IActionResult LogOut()
        {
            return View();
        }

        private void MapViewModelToVisitor(VisitorViewModel viewModel, Visitor visitor)
        {
            visitor.Name = viewModel.Name;
            visitor.LastName = viewModel.LastName;
            visitor.VisitorType = viewModel.VisitorType;
            visitor.Company = viewModel.Company;
            visitor.LicensePlate = viewModel.LicensePlate;
        }
    }
}
