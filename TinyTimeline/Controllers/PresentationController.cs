using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Interfaces.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyTimeline.Helpers;
using TinyTimeline.ModelBuilding;
using TinyTimeline.Models;
using TinyTimeline.Policies;

namespace TinyTimeline.Controllers
{
    [Authorize(Policy = PolicyNames.OnlyAuthUser)]
    public class PresentationController : Controller
    {
        private readonly ITimelineEventModelBuilder eventModelBuilder;
        private readonly ISessionModelBuilder sessionModelBuilder;
        private readonly ISessionsRepository sessionsRepository;
        private readonly IAuthTokenHelper authTokenHelper;

        public PresentationController(ITimelineEventModelBuilder eventModelBuilder,
                                      ISessionsRepository sessionsRepository,
                                      ISessionModelBuilder sessionModelBuilder,
                                      IAuthTokenHelper authTokenHelper)
        {
            this.eventModelBuilder = eventModelBuilder;
            this.sessionsRepository = sessionsRepository;
            this.sessionModelBuilder = sessionModelBuilder;
            this.authTokenHelper = authTokenHelper;
        }

        public IActionResult Sessions()
        {
            var sessions = sessionsRepository.GetSessions().Select(x => sessionModelBuilder.Build(x, authTokenHelper.IsAdmin()));
            return View(new SessionsModel
                        {
                            Sessions = sessions, 
                            AllowModify = authTokenHelper.IsAdmin()
                        });
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
                                                      SessionId = sessionId,
                                                      AllowModify = authTokenHelper.IsAdmin()
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
            var allowModify = authTokenHelper.IsAdmin();
            IEnumerable<TimelineEventModel> events;
            
            switch (filterType)
            {
                case EventFilterType.All:
                    events = eventModelBuilder.DateSortedBuild(session.Events, sessionId, allowModify);
                    break;
                case EventFilterType.Positive:
                    events = eventModelBuilder.DateSortedPositiveBuild(session, allowModify);
                    break;
                case EventFilterType.Debatable:
                    events = eventModelBuilder.DateSortedDebatableBuild(session, allowModify);
                    break;
                case EventFilterType.Negative:
                    events = eventModelBuilder.DateSortedNegativeBuild(session, allowModify);
                    break;
                case EventFilterType.Discussable:
                    events = eventModelBuilder.ToBeDiscussedBuild(session, allowModify);
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