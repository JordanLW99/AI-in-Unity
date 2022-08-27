using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public bool drawGUI;
    public bool drawGUIPadlock;
    public Transform other;
    public GameObject Fence;
    public GameObject Lock;
    public GameObject Chains;

    void Update()
    {
        if (other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            if (dist < 2 && Lock.activeInHierarchy == false)
            {
                drawGUI = true;
                if (Input.GetKeyDown(KeyCode.E) && Pause.ispaused == false)
                {
                    Fence.transform.Rotate(0, -40, 0);
                    Fence.transform.position = new Vector3(862, 0.005330682f, 851.46f);
                    Destroy(GetComponent<ExitButton>());
                    Destroy(Chains);
                }
            }
            else
            {
                drawGUI = false;
            }

            if (dist < 2 && Lock.activeInHierarchy == true)
            {
                drawGUIPadlock = true;
            }
            else
            {
                drawGUIPadlock = false;
            }
        }
    }

    void OnGUI()
    {
        if (drawGUI == true && Lock.activeInHierarchy == false)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 51, 200, 120, 22), "Press E to interact");
        }

        if (drawGUIPadlock == true && Lock.activeInHierarchy == true)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 101, 200, 190, 22), "Unlock the  gate padlock first");
        }
    }
}
