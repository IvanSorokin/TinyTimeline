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
        private readonly ITimelineEventModelBuilder eventModelBuilder;
        private readonly ITimelineEventsRepository eventsRepository;

        public PresentationController(ITimelineEventsRepository eventsRepository,
                                      ITimelineEventModelBuilder eventModelBuilder)
        {
            this.eventsRepository = eventsRepository;
            this.eventModelBuilder = eventModelBuilder;
        }

        public IActionResult Index()
        {
            var events = eventModelBuilder.DateSortedBuild(eventsRepository.GetAll()).ToList();
            return View(new PresentationModel{ Events = events });
        }

        public IActionResult PositiveEvents()
        {
            var events = eventModelBuilder.DateSortedPositiveBuild(eventsRepository.GetAll()).ToList();
            return View(new PresentationModel {Events = events});
        }
        
        public IActionResult NegativeEvents()
        {
            var events = eventModelBuilder.DateSortedNegativeBuild(eventsRepository.GetAll()).ToList();
            return View(new PresentationModel {Events = events});
        }
        
        public IActionResult DebatableEvents()
        {
            var events = eventModelBuilder.DateSortedDebatableBuild(eventsRepository.GetAll()).ToList();
            return View(new PresentationModel {Events = events});
        }

        private IEnumerable<TimelineEventModel> GetAllEvents()
            => eventModelBuilder.DateSortedBuild(eventsRepository.GetAll());
    }
}