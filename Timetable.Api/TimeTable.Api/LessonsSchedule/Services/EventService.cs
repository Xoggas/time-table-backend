﻿using Microsoft.AspNetCore.SignalR;
using TimeTable.Api.LessonsSchedule.Hubs;

namespace TimeTable.Api.LessonsSchedule.Services;

public interface IEventService
{
    Task NotifyAllClientsAboutUpdate();
}

public sealed class EventService : IEventService
{
    private readonly IHubContext<EventHub, IEventHub> _eventHub;

    public EventService(IHubContext<EventHub, IEventHub> eventHub)
    {
        _eventHub = eventHub;
    }

    public async Task NotifyAllClientsAboutUpdate()
    {
        await _eventHub.Clients.All.Update();
    }
}