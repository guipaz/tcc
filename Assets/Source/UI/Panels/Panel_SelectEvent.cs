using System;
using Assets;
using Assets.Source;
using Assets.Source.Model;
using UnityEngine;

public class Panel_SelectEvent : MonoBehaviour, IEditorPanel
{
    public GameObject messagePrefab;

    public Action<GameEvent> OnSelected;

    public void OpenParameterScreen(string eventType)
    {
        GameObject prefab = null;

        if (eventType == "message")
        {
            prefab = messagePrefab;
        }

        if (prefab != null)
        {
            var panel = Instantiate(messagePrefab, Global.canvasObject.transform);
            panel.GetComponent<IEventParameterPanel>().OnCreateEvent(ev =>
            {
                OnSelected?.Invoke(ev);
                Global.master.ClosePanel(gameObject);
            });
        }
    }

    public void DialogOpened()
    {
    }
}
