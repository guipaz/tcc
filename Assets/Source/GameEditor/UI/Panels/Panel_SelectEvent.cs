﻿using System;
using Assets;
using Assets.Source;
using Assets.Source.Events;
using Assets.Source.Model;
using UnityEngine;

public class Panel_SelectEvent : MonoBehaviour, IEditorPanel
{
    public GameObject messagePrefab;
    public GameObject changeMapPrefab;
    public GameObject changeSpritePrefab;
    public GameObject moveEntityPrefab;
    public GameObject changeSwitchPrefab;
    public GameObject changeVariablePrefabb;

    public Action<GameEvent> OnSelected;

    public void OpenParameterScreen(string eventType)
    {
        OpenParameterScreen(eventType, null);
    }

    public void OpenParameterScreen(string eventType, GameEvent currentEvent)
    {
        GameObject prefab = null;

        if (eventType == "message")
            prefab = messagePrefab;
        else if (eventType == "changeMap")
            prefab = changeMapPrefab;
        else if (eventType == "changeSprite")
            prefab = changeSpritePrefab;
        else if (eventType == "moveEntity")
            prefab = moveEntityPrefab;
        else if (eventType == "changeSwitch")
            prefab = changeSwitchPrefab;
        else if (eventType == "changeVariable")
            prefab = changeVariablePrefabb;

        if (prefab != null)
        {
            var panel = Instantiate(prefab, Global.canvasObject.transform);
            panel.GetComponent<IEventParameterPanel>().OnSaveEvent(ev =>
            {
                OnSelected?.Invoke(ev);
                Global.master.ClosePanel(gameObject);
            });

            if (currentEvent != null)
            {
                panel.GetComponent<IEventParameterPanel>().SetData(currentEvent);
            }
        }
        else if (eventType == "endGame")
        {
            OnSelected?.Invoke(new EndGameEvent());
            Global.master.ClosePanel(gameObject);
        }
    }

    public void DialogOpened()
    {
    }

    public void Close()
    {
        Global.master.ClosePanel(gameObject);
    }
}
