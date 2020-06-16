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
    }

    public void Exit()
    {
        Application.Quit();
    }
}
