using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            lastPosition = Input.mousePosition;
        }

        if (Input.GetMouseButton(1))
        {
            var delta = Input.mousePosition - lastPosition;
            transform.Translate(delta.x * -1 * Time.deltaTime, delta.y * -1 * Time.deltaTime, 0);
            lastPosition = Input.mousePosition;
        }
    }
}
