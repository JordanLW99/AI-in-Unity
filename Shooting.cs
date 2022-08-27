using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletclone;
    public float bulletSpeed = 20.0f;
    public float timer;

    public bool drawGUI;
    public bool drawGUI2;
    public bool drawGUI3;
    public static int ammo = 0;
    public GameObject thePlayer;
    public GameObject bulletrefill;
    public GameObject bulletrefill2;
    public GameObject bulletrefill3;

    public AudioClip shootSound;
    // Start is called before the first frame update
    void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag("Player");
        //bulletrefill = Instantiate(bulletrefill, new Vector3(225.89f + 2.0f, 1.83f, 162.4733f), Quaternion.identity) as GameObject;
    }

    void Shoot()
    {
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (!Pause.ispaused)
        {
            if (ammo > 0)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    AudioSource.PlayClipAtPoint(shootSound, transform.position, 1);
                    bulletclone = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
                    bulletclone.GetComponent<Rigidbody>().AddForce(transform.forward * 2000);
                    ammo = ammo - 1;
                }
            }


            if (bulletrefill)
            {
                float dist = Vector3.Distance(thePlayer.transform.position, bulletrefill.transform.position);
                if (dist < 3)
                {
                    drawGUI = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ammo = ammo + 5;
                        Destroy(bulletrefill);
                        drawGUI = false;
                    }
                }
                else
                {
                    drawGUI = false;
                }
            }

            if (bulletrefill2)
            {
                float dist2 = Vector3.Distance(thePlayer.transform.position, bulletrefill2.transform.position);
                if (dist2 < 3)
                {
                    drawGUI2 = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ammo = ammo + 7;
                        Destroy(bulletrefill2);
                        drawGUI2 = false;
                    }
                }
                else
                {
                    drawGUI2 = false;
                }
            }

            if (bulletrefill3)
            {
                float dist3 = Vector3.Distance(thePlayer.transform.position, bulletrefill3.transform.position);
                if (dist3 < 3)
                {
                    drawGUI3 = true;
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        ammo = ammo + 7;
                        Destroy(bulletrefill3);
                        drawGUI3 = false;
                    }
                }
                else
                {
                    drawGUI3 = false;
                }
            }
        }
        else
        {

        }
    }

    void OnGUI()
    {
        if (drawGUI == true)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 51, 200, 150, 22), "Press E to get ammo");
        }
        if (drawGUI2 == true)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 51, 200, 150, 22), "Press E to get ammo");
        }
        if (drawGUI3 == true)
        {
            GUI.Box(new Rect(Screen.width * 0.5f - 51, 200, 150, 22), "Press E to get ammo");
        }
    }

}
