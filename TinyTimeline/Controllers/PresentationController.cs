using System;
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
        private readonly ISessionModelBuilder sessionModelBuilder;
        private readonly ISessionsRepository sessionsRepository;

        public PresentationController(ITimelineEventModelBuilder eventModelBuilder,
                                      ISessionsRepository sessionsRepository,
                                      ISessionModelBuilder sessionModelBuilder)
        {
            this.eventModelBuilder = eventModelBuilder;
            this.sessionsRepository = sessionsRepository;
            this.sessionModelBuilder = sessionModelBuilder;
        }

        public IActionResult Session(Guid sessionId)
        {
            var session = sessionsRepository.Get(sessionId);
            var events = eventModelBuilder.DateSortedBuild(session.Events);
            return View(new SessionModel
                        {
                            Events = events.ToArray(),
                            Name = session.Name,
                            CreateDate = session.CreateDate,
                            SessionId = sessionId
                        });
        }

        public IActionResult Sessions()
        {
            var sessions = sessionsRepository.GetAll().Select(sessionModelBuilder.Build);
            return View(new SessionsModel {Sessions = sessions});
        }

        public IActionResult PositiveEvents(Guid sessionId)
        {
            var events = eventModelBuilder.DateSortedPositiveBuild(sessionsRepository.Get(sessionId).Events).ToList();
            return View(new SessionModel {Events = events, SessionId = sessionId});
        }

        public IActionResult NegativeEvents(Guid sessionId)
        {
            var events = eventModelBuilder.DateSortedNegativeBuild(sessionsRepository.Get(sessionId).Events).ToList();
            return View(new SessionModel {Events = events, SessionId = sessionId});
        }

        public IActionResult DebatableEvents(Guid sessionId)
        {
            var events = eventModelBuilder.DateSortedDebatableBuild(sessionsRepository.Get(sessionId).Events).ToList();
            return View(new SessionModel {Events = events, SessionId = sessionId});
        }
    }
}