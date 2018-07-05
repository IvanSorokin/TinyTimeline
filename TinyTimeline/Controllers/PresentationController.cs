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
            var sessions = sessionsRepository.GetSessions().Select(sessionModelBuilder.Build);
            return View(new SessionsModel {Sessions = sessions});
        }

        public IActionResult FilteredSession(Guid sessionId, EventFilterType filterType)
        {
            IEnumerable<TimelineEventModel> events;
            switch (filterType)
            {
                case EventFilterType.Positive:
                    events = eventModelBuilder.DateSortedPositiveBuild(sessionsRepository.Get(sessionId).Events);
                    break;
                case EventFilterType.Debatable:
                    events = eventModelBuilder.DateSortedDebatableBuild(sessionsRepository.Get(sessionId).Events);
                    break;
                case EventFilterType.Negative:
                    events = eventModelBuilder.DateSortedNegativeBuild(sessionsRepository.Get(sessionId).Events);
                    break;
                default:
                    return NotFound();
            }
            return View(new FilteredSessionModel
                        {
                            Events = events,
                            SessionId = sessionId,
                            EventFilterType = filterType
                        });
        }
    }
}