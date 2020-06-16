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

    Action<GameEvent> action;

    public void OK()
    {
        var ev = new MoveEntityEvent
        {
            id = entityField.text
        };
        int.TryParse(xField.text, out ev.x);
        int.TryParse(yField.text, out ev.y);

        action?.Invoke(ev);
        Global.master.ClosePanel(gameObject, true);
    }

    public void OnCreateEvent(Action<GameEvent> action)
    {
        this.action = action;
    }
}
