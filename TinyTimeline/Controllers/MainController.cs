using System;
using System.Linq;
using DataAccess.Interfaces.Repositories;
using Domain.Objects;
using Microsoft.AspNetCore.Mvc;
using TinyTimeline.Models;

namespace TinyTimeline.Controllers
{
    public class MainController : Controller
    {
        private readonly ITimelineEventsRepository timelineEventsRepository;

        public MainController(ITimelineEventsRepository timelineEventsRepository)
        {
            this.timelineEventsRepository = timelineEventsRepository;
        }

        public IActionResult Index()
        {
            var events = timelineEventsRepository.GetAll().Select(x => new TimelineEventModel
            {
                Text = x.Text,
                Date = x.Date
            });
            return View(new MainModel { Events = events });
        }

        public IActionResult AddEvent()
        {
            return View(new TimelineEventModel { Date = DateTime.Today });
        }

        [HttpPost]
        public IActionResult SaveEvent(TimelineEventModel model)
        {
            timelineEventsRepository.Save(new TimelineEvent
            {
                Id = Guid.NewGuid(),
                Date = model.Date.Date,
                Text = model.Text
            });
            return RedirectToAction("AddEvent");
        }
    }
}