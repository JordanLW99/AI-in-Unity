using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    public bool drawGUI;
    public bool drawGUIKey;
    public Transform other;
    public GameObject Key;

    void Update()
    {
        if (other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            if (dist < 2 && Key.activeInHierarchy == false)
            {
                drawGUI = true;
                if (Input.GetKeyDown(KeyCode.E) && Pause.ispaused == false)
                {
                    //Fence.transform.Rotate(0, 0, 40);
                    Destroy(GetComponent<Lock>());
                    gameObject.active = false;
                }
            }
            else
            {
                drawGUI = false;
            }

            if (dist < 2 && Key.activeInHierarchy == true)
            {
                drawGUIKey = true;
            }
            else
            {
                drawGUIKey = false;
            }
        }
    }

    void OnGUI()
    {
        if (drawGUI == true && Key.activeInHierarchy == false)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 51, 200, 120, 22), "Press E to unlock");
        }
        if (drawGUIKey == true && Key.activeInHierarchy == true)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 101, 200, 250, 22), "You need a key - hint: check warehouse");
        }
    }
}
