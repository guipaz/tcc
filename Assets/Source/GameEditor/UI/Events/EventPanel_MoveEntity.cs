using System;
using Assets.Source.Events;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel_MoveEntity : MonoBehaviour, IEventParameterPanel
{
    public InputField entityField;
    public InputField xField;
    public InputField yField;
    public Dropdown relativeField;

    Action<GameEvent> action;

    MoveEntityEvent currentEvent;

    public void OK()
    {
        if (currentEvent == null)
        {
            currentEvent = new MoveEntityEvent();
        }

        currentEvent.id = entityField.text;
        currentEvent.relative = relativeField.value == 0;

        int.TryParse(xField.text, out currentEvent.x);
        int.TryParse(yField.text, out currentEvent.y);

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
        currentEvent = ev as MoveEntityEvent;
        entityField.text = currentEvent.id;
        relativeField.value = currentEvent.relative ? 0 : 1;
        xField.text = currentEvent.x.ToString();
        yField.text = currentEvent.y.ToString();
    }
}
