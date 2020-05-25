using System;
using Assets.Source.Model;

public interface IEventParameterPanel
{
    void OnCreateEvent(Action<GameEvent> action);
}