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

        public IActionResult Reviews(Guid sessionId)
        {
            var session = sessionsRepository.Get(sessionId);
            return View(new ReviewsModel
                        {
                            Reviews = session.Reviews
                                     .OrderByDescending(x => x.Rating)
                                     .Select(x => new ReviewModel
                                                  {
                                                      Content = x.Content,
                                                      Rating = x.Rating,
                                                      Id = x.Id,
                                                      SessionId = sessionId
                                                  }),
                            SessionInfo = new SessionInfoModel
                                          {
                                              SessionId = session.Id, 
                                              SessionCreateDate = session.CreateDate, 
                                              SessionName = session.Name
                                          }
                        });
        }

        public IActionResult Session(Guid sessionId, EventFilterType filterType = EventFilterType.All)
        {
            var session = sessionsRepository.Get(sessionId);
            IEnumerable<TimelineEventModel> events;
            switch (filterType)
            {
                case EventFilterType.All:
                    events = eventModelBuilder.DateSortedBuild(session.Events, sessionId);
                    break;
                case EventFilterType.Positive:
                    events = eventModelBuilder.DateSortedPositiveBuild(session);
                    break;
                case EventFilterType.Debatable:
                    events = eventModelBuilder.DateSortedDebatableBuild(session);
                    break;
                case EventFilterType.Negative:
                    events = eventModelBuilder.DateSortedNegativeBuild(session);
                    break;
                case EventFilterType.Discussable:
                    events = eventModelBuilder.ToBeDiscussedBuild(session);
                    break;
                default:
                    return NotFound();
            }

            return View(new SessionModel
                        {
                            Events = events,
                            EventFilterType = filterType,
                            SessionInfo = new SessionInfoModel
                                          {
                                              SessionId = session.Id, 
                                              SessionCreateDate = session.CreateDate, 
                                              SessionName = session.Name
                                          }
                        });
        }
    }
}