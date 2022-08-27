using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCounter : MonoBehaviour
{
    //public int counter;
    public bool drawGUI;
    public int counter;
    // Start is called before the first frame update
    void Start()
    {
        counter = Shooting.ammo;
    }

    // Update is called once per frame
    void Update()
    {
        drawGUI = true;
    }

    void OnGUI()
    {
        if (drawGUI == true)
        {
            //GUI.Box(new Rect(Screen.width * 0.5f - 51, 100, 0, 22), "Ammo: " + counter);
            GUIStyle textStyle = new GUIStyle();
            textStyle.fontSize = 30;
            textStyle.normal.textColor = Color.white;

            GUI.Label(new Rect(10, 710, 200, 200), "Ammo: " + Shooting.ammo.ToString(), textStyle);
        }
    }
}
