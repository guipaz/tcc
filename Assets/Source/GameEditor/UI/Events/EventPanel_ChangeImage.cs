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

    public void OK()
    {
        var ev = new ChangeImageEvent
        {
            id = entityNameField.text,
            sprite = spriteImage.sprite
        };

        action?.Invoke(ev);
        Global.master.ClosePanel(gameObject, true);
    }

    public void OnCreateEvent(Action<GameEvent> action)
    {
        this.action = action;
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
