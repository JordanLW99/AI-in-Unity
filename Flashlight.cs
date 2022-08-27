using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public bool on = false;

    void Update()
    {
        Light light = GetComponent<Light>();

        if (Input.GetKeyDown(KeyCode.F))
            on = !on;
        if (on)
            light.enabled = true;
        else if (!on)
            light.enabled = false;
    }
}
