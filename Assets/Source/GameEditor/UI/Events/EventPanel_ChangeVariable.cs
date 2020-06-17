using System;
using Assets.Source;
using Assets.Source.Events;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel_ChangeVariable : MonoBehaviour, IEventParameterPanel
{
    public InputField switchNameField;
    public InputField valueField;
    public Dropdown operationField;

    Action<GameEvent> action;

    public void OK()
    {
        var ev = new ChangeVariableEvent()
        {
            id = switchNameField.text,
            operation = operationField.value
        };

        int.TryParse(valueField.text, out ev.value);

        action?.Invoke(ev);
        Global.master.ClosePanel(gameObject, true);
    }

    public void OnCreateEvent(Action<GameEvent> action)
    {
        this.action = action;
    }
}
