using System;
using System.Collections.Generic;
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
            var events = GetAllEvents();
            return View(new MainModel { Events = events });
        }

        private IEnumerable<TimelineEventModel> GetAllEvents()
        {
            return timelineEventsRepository.GetAll().Select(x => new TimelineEventModel
            {
                Text = x.Text,
                Date = x.Date,
                Positive = x.Positive,
                Negative = x.Negative,
                Id = x.Id
            });
        }

        public IActionResult AddEvent()
        {
            return View(new TimelineEventModel { Date = DateTime.Today });
        }

        public IActionResult Voting()
        {
            var events = GetAllEvents();
            return View(new MainModel { Events = events });
        }

        [HttpPost]
        public IActionResult Vote(VoteModel vote)
        {
            timelineEventsRepository.Vote(vote.EventId, vote.IsPositive);
            return Json("");
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