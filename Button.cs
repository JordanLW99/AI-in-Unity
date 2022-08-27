using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool drawGUI;
    public Transform other;
    public GameObject Fence;

    void Update()
    {
        if (other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            if (dist < 2)
            {
                drawGUI = true;
                if (Input.GetKeyDown(KeyCode.E) && Pause.ispaused == false)
                {
                    Fence.transform.Rotate(0, 0, 40);
                    Destroy(GetComponent<Button>());
                }
            }
            else
            {
                drawGUI = false;
            }
        }
    }

    void OnGUI()
    {
        if (drawGUI == true)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 51, 200, 120, 22), "Press E to interact");
        }
    }
}
