using System;
using Assets.Source.Model;
using UnityEngine;

public class MapRecord : MonoBehaviour
{
    public GameMap gameMap;

    public Action<MapRecord> OnSelected;

    public void Set()
    {
        OnSelected?.Invoke(this);
    }
}
