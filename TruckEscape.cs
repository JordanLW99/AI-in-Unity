using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TruckEscape : MonoBehaviour
{
    public bool drawGUI;
    public Transform other;

    void Update()
    {
        if (other)
        {
            float dist = Vector3.Distance(other.position, transform.position);
            if (dist < 5)
            {
                drawGUI = true;
                if (Input.GetKeyDown(KeyCode.E) && Pause.ispaused == false)
                {
                    SceneManager.LoadScene(sceneBuildIndex: 0);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
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
            GUI.Box(new Rect(Screen.width * 0.5f - 51, 200, 120, 22), "Press E to escape");
        }
    }
}
