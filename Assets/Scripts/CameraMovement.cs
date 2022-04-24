using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    public string movUp = "w", movDown = "s", movRight = "d", movLeft = "a";


    public float scrollSpeed = 20f;
    public Vector2 scrollLimits = new Vector2(20, 120);


    // The point (0,0) is the limit on the left corner
    public Vector2 mapSize = new Vector2(30, 30);



    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if(Input.GetKey(movUp) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(movDown) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(movRight) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }
        if (Input.GetKey(movLeft) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * Time.deltaTime *100f;
        pos.y = Mathf.Clamp(pos.y, scrollLimits.x, scrollLimits.y);


        pos.x = Mathf.Clamp(pos.x, 0, mapSize.x);
        pos.z = Mathf.Clamp(pos.z, 0, mapSize.y);

        transform.position = pos;
    }
}
