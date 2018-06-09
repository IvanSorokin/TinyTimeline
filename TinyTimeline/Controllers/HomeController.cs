using System;
using DataAccess.Interfaces.Repositories;
using Domain.Objects;
using Microsoft.AspNetCore.Mvc;

namespace TinyTimeline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITimelineEventsRepository timelineEventsRepository;

        public HomeController(ITimelineEventsRepository timelineEventsRepository)
        {
            this.timelineEventsRepository = timelineEventsRepository;
        }

        public IActionResult Index()
        {
            timelineEventsRepository.Save(new TimelineEvent {Id = Guid.NewGuid(), DateTime = DateTimeOffset.Now, Text = "test"});
            return View();
        }
    }
}