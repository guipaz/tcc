using System;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class EventInfo : MonoBehaviour
{
    public Text eventText;
    public Text detailText;

    public GameEvent gameEvent;
    public Action OnDelete;
    public Action OnEdit;

    public void Set(GameEvent ev)
    {
        gameEvent = ev;
        eventText.text = ev.GetNameText();
        detailText.text = ev.GetDescriptionText();
    }

    public void Delete()
    {
        OnDelete?.Invoke();
    }

    public void Edit()
    {
        OnEdit?.Invoke();
    }
}
