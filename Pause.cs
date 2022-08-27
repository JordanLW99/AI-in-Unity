using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;

    public GameObject pausePanel1;

    public static bool ispaused;
    public WindZone windcontrol;
    public GameObject windzonecontroller;

    void Start()
    {
        pausePanel.SetActive(false);
        pausePanel1.SetActive(false);
        windcontrol = GetComponent<WindZone>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            pausePanel1.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ispaused = true;
            windzonecontroller.SetActive(false);
        }
    }

    public void Unpause()
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        pausePanel1.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        ispaused = false;
        windzonecontroller.SetActive(true);
    }
}