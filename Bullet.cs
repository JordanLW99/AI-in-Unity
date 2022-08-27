using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Animator anim;
    public float reactiontimer;
    public GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        /*if (collision.gameObject.tag == "Enemy")
        {
            //animIdle.SetTrigger("Hit");
            Destroy(bullet);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
