using System;
using Assets.Source;
using Assets.Source.Events;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel_Message : MonoBehaviour, IEventParameterPanel
{
    public InputField textField;

    Action<GameEvent> action;

    public void OK()
    {
        action?.Invoke(new MessageEvent
        {
            message = textField.text
        });
        Global.master.ClosePanel(gameObject);
    }

    public void OnCreateEvent(Action<GameEvent> action)
    {
        this.action = action;
    }
}
