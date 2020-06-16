using System;
using Assets;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Entity : MonoBehaviour, IEditorPanel
{
    EditorEntity editorEntity;

    public GameEntityState currentState;

    public Image image;
    public InputField nameField;
    public Dropdown executionDropdown;
    public Toggle passableToggle;
    public GameObject eventContent;
    public GameObject eventInfoPrefab;
    public Sprite noimageSprite;
    public Text stateName;

    Sprite selectedSprite;

    public void OpenStates()
    {
        var statesPanel = Global.master.OpenPanel("states");
        statesPanel.GetComponent<Panel_States>().OnSelectState = state =>
        {
            ClearFields();
            SetState(state);
        };
        statesPanel.GetComponent<Panel_States>().SetData(editorEntity.gameEntity);
    }

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
            currentState.events.Add(ev);
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
            currentState.events.Remove(ev);
        };
    }

    public void SetData(EditorEntity editorEntity)
    {
        this.editorEntity = editorEntity;
        if (editorEntity == null)
        {
            return;
        }

        SetState(editorEntity.gameEntity.states[GameEntityState.DEFAULT_STATE_NAME]);
    }

    void SetState(GameEntityState state)
    {
        currentState = state;

        stateName.text = state.name;

        nameField.text = editorEntity.gameEntity.name ?? "";
        executionDropdown.value = (int?)currentState.execution ?? 0;

        selectedSprite = currentState.image;

        image.sprite = selectedSprite ?? image.sprite;
        passableToggle.isOn = currentState.passable;

        if (currentState.events != null)
            foreach (var ev in currentState.events)
                AddEventToScreen(ev);
    }

    public void Save()
    {
        editorEntity.gameEntity.name = nameField.text;
        Enum.TryParse(executionDropdown.value.ToString(), out currentState.execution);
        currentState.image = selectedSprite;
        currentState.passable = passableToggle.isOn;
    }

    public void DialogOpened()
    {
        ClearFields();
    }

    void ClearFields()
    {
        nameField.text = "";
        image.sprite = noimageSprite;
        selectedSprite = null;
        passableToggle.isOn = false;

        foreach (Transform child in eventContent.transform)
            Destroy(child.gameObject);
    }

    public void Close()
    {
        Global.master.ClosePanel(gameObject);

        editorEntity.GetComponent<SpriteRenderer>().sprite = currentState.image ?? Resources.Load<Sprite>("ICO_Feint");
    }

    public void Delete()
    {
        Global.currentMap.entities.Remove(editorEntity.gameEntity);
        Destroy(editorEntity.gameObject);
        Close();
    }
}
