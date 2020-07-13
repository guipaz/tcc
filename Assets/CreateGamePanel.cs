using System.Collections;
using System.Collections.Generic;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateGamePanel : MonoBehaviour
{
    public InputField nameField;
    public InputField fileField;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Create()
    {
        var name = nameField.text;
        var file = fileField.text;

        if (name.Length == 0 || file.Length == 0)
            return;

        gameObject.SetActive(false);

        Global.game = new Game
        {
            name = name,
            fileName = file
        };

        Global.playGame = false;
        SceneManager.LoadScene("EditorScene");
    }
}
