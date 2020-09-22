using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using VisitorRegistration.Models.Domain;
using VisitorRegistration.Models.GroupedByDurationViewModels;
using VisitorRegistration.Models.VisitorViewModels;

namespace VisitorRegistration.Controllers
{
    public class VisitorController : Controller
    {
        private readonly IVisitorRepository _visitorRepository;
        private readonly IStringLocalizer<VisitorViewModel> _localizer;
        public VisitorController(IVisitorRepository visitorRepository, IStringLocalizer<VisitorViewModel> localizer)
        {
            _visitorRepository = visitorRepository;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View(new VisitorViewModel(_localizer));
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
            IEnumerable<Visitor> visitors = _visitorRepository.GetCurrentVisitors();
            return View(visitors);
        }

        public IActionResult GroupedByDuration(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }
            IEnumerable<Visitor> visitors = _visitorRepository.GetAllByDate(date).Where(v => !v.Left.Date.Equals(DateTime.MinValue.Date));
            IEnumerable<IGrouping<double, Visitor>> queryByDuration = 
                from visitor in visitors
                // where !visitor.Left.Date.Equals(DateTime.MinValue.Date)
                group visitor by Math.Ceiling(visitor.Left.Subtract(visitor.Entered).TotalHours);
            var model = new GroupedByDurationViewModel();
            model.datePickerViewModel = new DatePickerViewModel() { Date = DateTime.Today };
            model.resultSetModel = queryByDuration;
            return View(model);
        }

        [HttpPost]
        public IActionResult GroupedByDuration(GroupedByDurationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return GroupedByDuration(viewModel.datePickerViewModel.Date);
            }
            return View(viewModel);
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
