using System;
using UnityEngine;

public class PanelTile : MonoBehaviour
{
    public Action onClick;

    public void Click()
    {
        onClick?.Invoke();
    }
}
