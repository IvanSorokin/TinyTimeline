using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces.Repositories;
using Domain.Objects;
using Microsoft.AspNetCore.Mvc;
using TinyTimeline.ModelBuilding;
using TinyTimeline.Models;

namespace TinyTimeline.Controllers
{
    public class InteractionController : Controller
    {
        private readonly ITimelineEventModelBuilder timelineEventModelBuilder;
        private readonly ITimelineEventsRepository timelineEventsRepository;

        public InteractionController(ITimelineEventsRepository timelineEventsRepository,
                                     ITimelineEventModelBuilder timelineEventModelBuilder)
        {
            this.timelineEventsRepository = timelineEventsRepository;
            this.timelineEventModelBuilder = timelineEventModelBuilder;
        }

        private IEnumerable<TimelineEventModel> GetAllEvents()
            => timelineEventModelBuilder.DateSortedBuild(timelineEventsRepository.GetAll());

        public IActionResult AddEvent() => View(new TimelineEventModel {Date = DateTime.Today});

        public IActionResult Voting() => View(new MainModel {Events = GetAllEvents().ToList()});

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