using System.Linq;
using Assets.Source.Model;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMaster : MonoBehaviour
{
    public void NewProject()
    {
        SceneManager.LoadScene("EditorScene");
    }

    public void LoadProject()
    {
        //TODO call windows file opener
        Global.loadGame = "save1.json";
        SceneManager.LoadScene("EditorScene");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
