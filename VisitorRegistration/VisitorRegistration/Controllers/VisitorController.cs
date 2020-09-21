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
            ViewData["VisitorTypes"] = GetVisitorTypesAsSelectList();
            return View(new VisitorViewModel());
        }

        public IActionResult LogOut()
        {
            return View();
        }

        private SelectList GetVisitorTypesAsSelectList()
        {
            //// temporary
            //Array values = Enum.GetValues(typeof(VisitorTypes));
            //List<ListItem> items = new List<ListItem>(values.Length);

            //foreach(var i in values)
            //{
            //    items.Add(new ListItem
            //    {
            //        Text = Enum.GetName(typeof(VisitorTypes), i),
            //        Value = ((int)i).ToString()
            //    });
            //}
            return null;
        }
    }
}
