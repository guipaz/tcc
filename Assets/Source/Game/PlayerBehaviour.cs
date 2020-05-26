using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    Vector2 mov;

    //TODO interaction

    void Update()
    {
        mov.x = 0;
        mov.y = 0;

        if (Input.GetKeyDown(KeyCode.RightArrow))
            mov.x = 1;
        if (Input.GetKeyDown(KeyCode.LeftArrow))
            mov.x = -1;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            mov.y = 1;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            mov.y = -1;

        //TODO check collision

        if (mov != Vector2.zero)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + mov.x, transform.localPosition.y + mov.y, transform.localPosition.z);
            CenterCamera();
        }
    }

    public void CenterCamera()
    {
        Camera.main.transform.localPosition = new Vector3(transform.position.x, transform.position.y, Camera.main.transform.localPosition.z);
    }
}
