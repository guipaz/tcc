using Assets;
using UnityEngine;
using UnityEngine.UI;

public class Panel_PlayerConfig : MonoBehaviour, IEditorPanel
{
    public Image characterSpriteImage;
    public InputField startingMapField;
    public InputField startingXField;
    public InputField startingYField;

    Sprite selectedSprite;

    public void DialogOpened()
    {
        Clear();

        characterSpriteImage.sprite = Global.game.player.sprite ?? characterSpriteImage.sprite;
        startingMapField.text = Global.game.player.startingMap ?? "";
        startingXField.text = Global.game.player.startingX.ToString();
        startingYField.text = Global.game.player.startingY.ToString();
    }

    void Clear()
    {
        startingMapField.text = "";
        startingXField.text = "";
        startingYField.text = "";
        characterSpriteImage.sprite = Resources.Load<Sprite>("noimage");
        selectedSprite = null;
    }

    public void Save()
    {
        Global.game.player.sprite = selectedSprite ?? Global.game.player.sprite;
        Global.game.player.startingMap = startingMapField.text;
        Global.game.player.startingX = int.Parse(startingXField.text);
        Global.game.player.startingY = int.Parse(startingYField.text);

        Close();
    }

    public void Close()
    {
        Global.master.ClosePanel(gameObject);
    }

    public void SelectImage()
    {
        var panel = Global.master.OpenPanel("selectImage");
        panel.GetComponent<Panel_SelectImage>().OnSelected = sprite =>
        {
            characterSpriteImage.sprite = sprite;
            selectedSprite = sprite;
        };
    }
}
