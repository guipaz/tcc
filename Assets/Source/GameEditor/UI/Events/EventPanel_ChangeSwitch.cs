using System;
using Assets.Source;
using Assets.Source.Events;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel_ChangeSwitch : MonoBehaviour, IEventParameterPanel
{
    public InputField switchNameField;
    public Dropdown valueField;

    Action<GameEvent> action;
    ChangeSwitchEvent currentEvent;

    public void OK()
    {
        if (currentEvent == null)
        {
            currentEvent = new ChangeSwitchEvent();
        }

        currentEvent.id = switchNameField.text;
        currentEvent.value = valueField.value == 0;

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
        currentEvent = ev as ChangeSwitchEvent;
        switchNameField.text = currentEvent.id;
        valueField.value = currentEvent.value ? 0 : 1;
    }
}
