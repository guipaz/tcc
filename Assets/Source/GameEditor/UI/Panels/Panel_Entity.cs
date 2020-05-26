using System;
using Assets;
using Assets.Source;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Entity : MonoBehaviour, IEditorPanel
{
    EditorEntity editorEntity;

    public Image image;
    public InputField nameField;
    public Dropdown executionDropdown;
    public Toggle passableToggle;
    public GameObject eventContent;
    public GameObject eventInfoPrefab;

    Sprite selectedSprite;

    public void SelectImage()
    {
        var panel = Global.master.OpenPanel("selectImage");
        panel.GetComponent<Panel_SelectImage>().OnSelected = sprite =>
        {
            image.sprite = sprite;
            selectedSprite = sprite;
        };
    }

    public void SelectEvent()
    {
        var panel = Global.master.OpenPanel("selectEvent");
        panel.GetComponent<Panel_SelectEvent>().OnSelected = ev =>
        { 
            editorEntity.gameEntity.events.Add(ev);
            AddEventToScreen(ev);
        };
    }

    public void AddEventToScreen(GameEvent ev)
    {
        var eventInfo = Instantiate(eventInfoPrefab, eventContent.transform);
        eventInfo.GetComponent<EventInfo>().Set(ev);
        eventInfo.GetComponent<EventInfo>().OnDelete = () =>
        {
            Destroy(eventInfo);
            editorEntity.gameEntity.events.Remove(ev);
        };
    }

    public void SetData(EditorEntity editorEntity)
    {
        this.editorEntity = editorEntity;

        nameField.text = editorEntity?.gameEntity?.name ?? "";
        executionDropdown.value = (int?)editorEntity?.gameEntity?.execution ?? 0;

        selectedSprite = editorEntity?.gameEntity?.image;

        image.sprite = selectedSprite ?? image.sprite;
        passableToggle.isOn = editorEntity?.gameEntity?.passable ?? false;

        if (editorEntity?.gameEntity?.events != null)
            foreach (var ev in editorEntity.gameEntity.events)
                AddEventToScreen(ev);
    }

    public void Save()
    {
        editorEntity.gameEntity.name = nameField.text;
        Enum.TryParse(executionDropdown.value.ToString(), out editorEntity.gameEntity.execution);
        editorEntity.gameEntity.image = selectedSprite;
        editorEntity.gameEntity.passable = passableToggle.isOn;

        // events are already inside it

        Close();
    }

    public void DialogOpened()
    {
        nameField.text = "";
        image.sprite = Resources.Load<Sprite>("noimage");
        selectedSprite = null;
        passableToggle.isOn = false;

        foreach (Transform child in eventContent.transform)
            Destroy(child.gameObject);
    }

    public void Close()
    {
        Global.master.ClosePanel("entity");

        editorEntity.GetComponent<SpriteRenderer>().sprite = editorEntity?.gameEntity?.image ?? Resources.Load<Sprite>("ICO_Feint");
    }

    public void Delete()
    {
        Global.currentMap.entityLayer.entities.Remove(editorEntity.gameEntity);
        Destroy(editorEntity.gameObject);
        Close();
    }
}
