using System.Collections;
using System.Collections.Generic;
using Assets.Source;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Layers : MonoBehaviour
{
    public void ChangeLayer(int layerId)
    {
        var layer = Global.currentLayer;

        switch (layerId)
        {
            case 0:
                layer = Layers.Terrain;
                break;
            case 1:
                layer = Layers.Construction;
                break;
            case 2:
                layer = Layers.Above;
                break;
            case 3:
                layer = Layers.Entities;
                break;
        }

        Global.currentLayer = layer;

        if (Global.currentLayer == Layers.Entities)
        {
            Global.cursorObject.GetComponent<SpriteRenderer>().sprite = null;
        }
    }
}
