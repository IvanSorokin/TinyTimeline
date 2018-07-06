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
        private readonly ISessionsRepository sessionsRepository;

        public InteractionController(ITimelineEventModelBuilder eventModelBuilder,
                                     ISessionsRepository sessionsRepository)
        {
            this.eventModelBuilder = eventModelBuilder;
            this.sessionsRepository = sessionsRepository;
        }

        public IActionResult AddEvent(Guid sessionId) => View(new TimelineEventModel {Date = DateTime.Today.AddDays(-30), SessionId = sessionId});
        
        public IActionResult AddSession() => View(new SessionModel());

        public IActionResult Voting(Guid sessionId)
        {
            var events = eventModelBuilder.DateSortedBuild(sessionsRepository.Get(sessionId)).ToList();
            return View(new SessionModel
                        {
                            Events = events,
                            SessionId = sessionId
                        });
        }

        [HttpPost]
        public IActionResult Vote(VoteModel vote)
        {
            sessionsRepository.Vote(vote.SessionId, vote.EventId, vote.IsPositive);
            return Json("");
        }

        [HttpPost]
        public IActionResult SaveEvent(TimelineEventModel model)
        {
            sessionsRepository.AddEvent(model.SessionId,
                                        new TimelineEvent
                                          {
                                              Id = Guid.NewGuid(),
                                              Date = model.Date.Date,
                                              Text = model.Text
                                          });
            return RedirectToAction("AddEvent", new {sessionId = model.SessionId});
        }
        
        [HttpDelete]
        public IActionResult DeleteEvent(Guid sessionId, Guid eventId)
        {
            sessionsRepository.RemoveEvent(sessionId, eventId);
            return RedirectToAction("Session", "Presentation", new {sessionId = sessionId});
        }

        [HttpPost]
        public IActionResult SaveSession(SessionModel model)
        {
            sessionsRepository.Save(new Session
                                    {
                                        CreateDate = DateTime.Today,
                                        Id = Guid.NewGuid(),
                                        Name = model.Name,
                                        Events = new TimelineEvent[0]
                                    });
            return RedirectToAction("Sessions", "Presentation");
        }

        public IActionResult DeleteSession(Guid sessionid)
        {
            sessionsRepository.Delete(sessionid);
            return RedirectToAction("Sessions", "Presentation");
        }
    }
}