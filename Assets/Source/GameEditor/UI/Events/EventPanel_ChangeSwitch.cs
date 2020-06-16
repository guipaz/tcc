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

    public void OK()
    {
        var ev = new ChangeSwitchEvent()
        {
            id = switchNameField.text,
            value = valueField.value == 0
        };

        action?.Invoke(ev);
        Global.master.ClosePanel(gameObject, true);
    }

    public void OnCreateEvent(Action<GameEvent> action)
    {
        this.action = action;
    }
}
