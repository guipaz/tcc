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

    ChangeVariableEvent currentEvent;

    public void OK()
    {
        if (currentEvent == null)
        {
            currentEvent = new ChangeVariableEvent();
        }

        currentEvent.id = switchNameField.text;
        currentEvent.operation = operationField.value;

        int.TryParse(valueField.text, out currentEvent.value);

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
        currentEvent = ev as ChangeVariableEvent;
        switchNameField.text = currentEvent.id;
        operationField.value = currentEvent.operation;
        valueField.text = currentEvent.value.ToString();
    }
}
