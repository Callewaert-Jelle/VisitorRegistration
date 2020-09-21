﻿using Microsoft.AspNetCore.Mvc;
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
            IEnumerable<Visitor> visitors = _visitorRepository.GetCurrentVisitors();
            return View(visitors);
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
