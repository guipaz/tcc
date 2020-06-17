using System;
using Assets;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.UI;

public class Panel_States : MonoBehaviour, IEditorPanel
{
    public InputField nameField;

    public Toggle checkSwitchField;
    public InputField switchNameField;
    public Dropdown switchValueField;

    public Toggle checkVariableField;
    public InputField variableNameField;
    public InputField variableValueField;

    public GameObject stateContainer;
    public GameObject textItemPrefab;

    public Action<GameEntityState> OnSelectState;

    GameEntityState selectedState;
    GameEntity entity;

    public void DialogOpened()
    {
        ClearFields();
    }

    public void SetData(GameEntity entity)
    {
        this.entity = entity;

        ReloadStateTree();

        selectedState = entity.states[GameEntityState.DEFAULT_STATE_NAME];

        FillFields();
    }

    void ReloadStateTree()
    {
        // erase old states
        foreach (Transform child in stateContainer.transform)
            Destroy(child.gameObject);

        foreach (var key in entity.states.Keys)
        {
            var state = entity.states[key];
            var obj = Instantiate(textItemPrefab, stateContainer.transform);
            obj.GetComponent<Text>().text = key;
            obj.GetComponent<MapRecord>().OnSelected = record =>
            {
                selectedState = state;
                FillFields();
            };
        }
    }

    void FillFields()
    {
        nameField.text = selectedState.name;
        checkSwitchField.isOn = selectedState.switchCheck;
        switchNameField.text = selectedState.switchCheckName;
        switchValueField.value = selectedState.switchCheckValue ? 0 : 1;

        checkVariableField.isOn = selectedState.variableCheck;
        variableNameField.text = selectedState.variableCheckName;
        variableValueField.text = selectedState.variableCheckValue.ToString();
    }

    void ClearFields()
    {
        nameField.text = "";
        checkSwitchField.isOn = false;
        switchNameField.text = "";
        switchValueField.value = 0;

        checkVariableField.isOn = false;
        variableNameField.text = "";
        variableValueField.text = "";
    }

    public void Save()
    {
        // this shouldn't be changed ever
        //TODO show this to the user
        if (selectedState.name == GameEntityState.DEFAULT_STATE_NAME || string.IsNullOrEmpty(nameField.text))
            return;

        var oldName = selectedState.name; // to replace in the dictionary

        selectedState.name = nameField.text;
        selectedState.switchCheck = checkSwitchField.isOn;
        selectedState.switchCheckName = switchNameField.text;
        selectedState.switchCheckValue = switchValueField.value == 0;

        selectedState.variableCheck = checkVariableField.isOn;
        selectedState.variableCheckName = variableNameField.text;
        int.TryParse(variableValueField.text, out selectedState.variableCheckValue);

        if (oldName != null)
            entity.states.Remove(oldName);

        entity.states[selectedState.name] = selectedState;

        ReloadStateTree();
    }

    public void Close()
    {
        Global.master.ClosePanel(gameObject);
    }

    public void Select()
    {
        OnSelectState?.Invoke(selectedState);
        Close();
    }

    public void Delete()
    {
        if (selectedState.name != GameEntityState.DEFAULT_STATE_NAME)
        {
            entity.states.Remove(selectedState.name);
            selectedState = entity.states[GameEntityState.DEFAULT_STATE_NAME];
            FillFields();
            ReloadStateTree();
        }
    }

    public void NewState()
    {
        ClearFields();

        selectedState = new GameEntityState();
    }
}
