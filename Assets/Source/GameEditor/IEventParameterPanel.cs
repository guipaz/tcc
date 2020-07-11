using System;
using Assets.Source.Model;

public interface IEventParameterPanel
{
    void OnSaveEvent(Action<GameEvent> action);
    void SetData(GameEvent ev);
}