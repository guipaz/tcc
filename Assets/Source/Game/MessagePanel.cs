using UnityEngine;
using UnityEngine.UI;

public class MessagePanel : MonoBehaviour
{
    public static MessagePanel main;

    public Text text;

    public void Awake()
    {
        main = this;
    }

    public void Toggle(bool on)
    {
        gameObject.SetActive(on);
    }

    public void SetMessage(string message)
    {
        text.text = message;
    }
}
