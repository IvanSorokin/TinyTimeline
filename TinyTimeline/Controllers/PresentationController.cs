using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces.Repositories;
using Microsoft.AspNetCore.Mvc;
using TinyTimeline.ModelBuilding;
using TinyTimeline.Models;

namespace TinyTimeline.Controllers
{
    public class PresentationController : Controller
    {
        private readonly ITimelineEventModelBuilder timelineEventModelBuilder;
        private readonly ITimelineEventsRepository timelineEventsRepository;

        public PresentationController(ITimelineEventsRepository timelineEventsRepository,
                                      ITimelineEventModelBuilder timelineEventModelBuilder)
        {
            this.timelineEventsRepository = timelineEventsRepository;
            this.timelineEventModelBuilder = timelineEventModelBuilder;
        }

        public IActionResult Index()
        {
            var events = GetAllEvents();
            return View(new MainModel {Events = events.ToList()});
        }

        private IEnumerable<TimelineEventModel> GetAllEvents()
            => timelineEventModelBuilder.DateSortedBuild(timelineEventsRepository.GetAll());
    }
}