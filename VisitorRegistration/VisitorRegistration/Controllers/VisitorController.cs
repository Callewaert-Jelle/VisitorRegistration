using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using VisitorRegistration.Models.Domain;
using VisitorRegistration.Models.VisitorViewModels;

namespace VisitorRegistration.Controllers
{
    public class VisitorController : Controller
    {
        private readonly IVisitorRepository _visitorRepository;
        public VisitorController(IVisitorRepository visitorRepository)
        {
            _visitorRepository = visitorRepository;
        }

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
                    _visitorRepository.Add(visitor);
                    _visitorRepository.SaveChanges();
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
            Visitor visitor = _visitorRepository.GetBy(1);

            IEnumerable<Visitor> visitors = _visitorRepository.GetCurrentVisitors();
            return View(visitors);
        }

        [HttpPost]
        public IActionResult LogOut(int id)
        {
            Visitor visitor = _visitorRepository.GetBy(id);
            try
            {
                visitor.LogOut();
                _visitorRepository.SaveChanges();
                TempData["message"] = $"Successfully logged out: {visitor.LastName} {visitor.Name}";
                return View(nameof(Index));
            }
            catch(Exception e)
            {

            }
            return View(nameof(LogOut));
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Consult()
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
