using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLook : MonoBehaviour
{

    public float speedV = 2.0f;
    public float minimumY = 90f;
    public float maximumY = -90f;
    float rotationY;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.ispaused)
        {
            rotationY -= speedV * Input.GetAxis("Mouse Y");
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
            transform.localEulerAngles = new Vector3(rotationY, 0, 0);
        }
        else
        {

        }
    }
}
