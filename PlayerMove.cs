using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 4.0f;
    public AudioClip walkSound;
    public AudioSource Audio;
    public bool triggerednoise;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-4 * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(0, 0, 4 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -4 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(4 * Time.deltaTime, 0, 0);
        }


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (!Audio.isPlaying)
            {
                Audio.loop = true;
                Audio.clip = walkSound;
                Audio.Play();
            }
                
            triggerednoise = true;
            //AudioSource.PlayClipAtPoint(walkSound, transform.position, 1);
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            triggerednoise = false;
            Audio.Stop();
        }
    }
}
