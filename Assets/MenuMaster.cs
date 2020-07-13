using System.Collections.Generic;
using System.Linq;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    public GamesPanel gamesPanel;
    public CreateGamePanel createGamePanel;

    void Start()
    {
        gamesPanel.gameObject.SetActive(false);
        createGamePanel.gameObject.SetActive(false);
    }

    public void NewProject()
    {
        createGamePanel.Open();
    }

    public void LoadProject()
    {
        gamesPanel.Open(file =>
        {
            Global.playGame = false;
            Global.loadGame = file;
            SceneManager.LoadScene("EditorScene");
        });
    }

    public void Play()
    {
        gamesPanel.Open(file =>
        {
            Global.playGame = true;
            Global.game = Persistor.instance.LoadFile<Game>(Persistor.DEFAULT_FOLDER + file).ToList()[0];
            SceneManager.LoadScene("PlayScene");
        });
    }

    public void Exit()
    {
        Application.Quit();
    }
}
