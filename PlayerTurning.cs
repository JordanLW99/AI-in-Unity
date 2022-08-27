using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurning : MonoBehaviour
{

    float rotationX;
    public float speedH = 2.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Pause.ispaused)
        {
            rotationX += speedH * Input.GetAxis("Mouse X");
            transform.localEulerAngles = new Vector3(0, rotationX, 0);
        }
        else
        {

        }
    }
}
