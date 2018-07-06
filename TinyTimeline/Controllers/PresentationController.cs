using System;
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

        public IActionResult Sessions()
        {
            var sessions = sessionsRepository.GetSessions().Select(sessionModelBuilder.Build);
            return View(new SessionsModel {Sessions = sessions});
        }

        public IActionResult Session(Guid sessionId, EventFilterType filterType = EventFilterType.All)
        {
            var session = sessionsRepository.Get(sessionId);
            IEnumerable<TimelineEventModel> events;
            switch (filterType)
            {
                case EventFilterType.Positive:
                    events = eventModelBuilder.DateSortedPositiveBuild(session.Events);
                    break;
                case EventFilterType.Debatable:
                    events = eventModelBuilder.DateSortedDebatableBuild(session.Events);
                    break;
                case EventFilterType.Negative:
                    events = eventModelBuilder.DateSortedNegativeBuild(session.Events);
                    break;
                case EventFilterType.All:
                    events = eventModelBuilder.DateSortedBuild(session.Events);
                    break;
                default:
                    return NotFound();
            }
            return View(new SessionModel
                        {
                            Events = events,
                            SessionId = sessionId,
                            EventFilterType = filterType,
                            Name = session.Name,
                            CreateDate = session.CreateDate,
                        });
        }
    }
}