using System;
using Assets.Source;
using Assets.Source.Events;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel_ChangeMap : MonoBehaviour, IEventParameterPanel
{
    public InputField mapNameField;
    public InputField xField;
    public InputField yField;

    Action<GameEvent> action;
    ChangeMapEvent currentEvent;

    public void OK()
    {
        if (currentEvent == null)
        {
            currentEvent = new ChangeMapEvent();
        }

        currentEvent.mapName = mapNameField.text;

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
        currentEvent = ev as ChangeMapEvent;
        mapNameField.text = currentEvent.mapName;
        xField.text = currentEvent.x.ToString();
        yField.text = currentEvent.y.ToString();
    }
}
