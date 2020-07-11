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

    MessageEvent currentEvent;

    public void OK()
    {
        if (currentEvent == null)
        {
            currentEvent = new MessageEvent();
        }

        currentEvent.message = textField.text;

        action?.Invoke(currentEvent);
        Global.master.ClosePanel(gameObject, true);

        currentEvent = null;
    }

    public void OnSaveEvent(Action<GameEvent> action)
    {
        this.action = action;
    }

    public void SetData(GameEvent ev)
    {
        currentEvent = ev as MessageEvent;
        textField.text = currentEvent.message;
    }
}
