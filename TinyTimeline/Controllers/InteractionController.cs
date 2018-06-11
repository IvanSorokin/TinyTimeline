using System;
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
        private readonly ITimelineEventModelBuilder eventModelBuilder;
        private readonly ITimelineEventsRepository timelineEventsRepository;

        public InteractionController(ITimelineEventsRepository timelineEventsRepository,
                                     ITimelineEventModelBuilder eventModelBuilder)
        {
            this.timelineEventsRepository = timelineEventsRepository;
            this.eventModelBuilder = eventModelBuilder;
        }

        public IActionResult AddEvent() => View(new TimelineEventModel {Date = DateTime.Today});

        public IActionResult Voting()
        {
            var events = eventModelBuilder.DateSortedBuild(timelineEventsRepository.GetAll()).ToList();
            return View(new PresentationModel
                        {
                            Events = events
                        });
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