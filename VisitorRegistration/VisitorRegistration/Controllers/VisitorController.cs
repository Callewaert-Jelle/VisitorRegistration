using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using VisitorRegistration.Models.Domain;
using VisitorRegistration.Models.GroupedByDurationViewModels;
using VisitorRegistration.Models.HistoryViewModel;
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
                    var repo = _visitorRepository;
                    Thread aNewThread = new Thread(
                        () => {
                            var id = visitor.VisitorId;
                            // after 16h automatically log out
                            // Thread.Sleep(16 * 60 * 60 * 1000);
                            Thread.Sleep(10 * 1000);
                            LogOutAutomatically(id, repo);
                        }
                    );
                    //aNewThread.Start();
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

        [Authorize(Policy = "AdminOnly")]
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
            var model = new GroupedByDurationViewModel
            {
                datePickerViewModel = new DatePickerViewModel() { Date = DateTime.Today },
                resultSetModel = queryByDuration
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GroupedByDuration(GroupedByDurationViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return GroupedByDuration(viewModel.datePickerViewModel.Date);
            }
            return View(viewModel);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult History(DateTime date)
        {
            if (date == DateTime.MinValue)
            {
                date = DateTime.Now;
            }
            IEnumerable<Visitor> visitors = _visitorRepository.GetAllByDate(date);
            HistoryViewModel model = new HistoryViewModel()
            {
                DatePickerViewModel = new DatePickerViewModel() { Date = DateTime.Today },
                Visitors = visitors
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult History(HistoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                return History(viewModel.DatePickerViewModel.Date);
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SetCulture(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        private void LogOutAutomatically(int id, IVisitorRepository repo)
        {
            Visitor v = repo.GetBy(id);
            v.LogOut();
            repo.SaveChanges();
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
