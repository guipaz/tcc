using System;
using Assets.Source.Events;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class EventPanel_ChangeImage : MonoBehaviour, IEventParameterPanel
{
    public InputField entityNameField;
    public Image spriteImage;

    Action<GameEvent> action;
    ChangeImageEvent currentEvent;

    public void OK()
    {
        if (currentEvent == null)
        {
            currentEvent = new ChangeImageEvent();
        }

        currentEvent.id = entityNameField.text;
        currentEvent.sprite = spriteImage.sprite;


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
        currentEvent = ev as ChangeImageEvent;
        entityNameField.text = currentEvent.id;
        spriteImage.sprite = currentEvent.sprite;
    }

    public void SelectImage()
    {
        var panel = Global.master.OpenPanel("selectImage");
        panel.GetComponent<Panel_SelectImage>().OnSelected = sprite =>
        {
            spriteImage.sprite = sprite;
        };
    }
}
