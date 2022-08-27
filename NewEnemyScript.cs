using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTAI;
using UnityEngine.AI;
using System.Linq.Expressions;
using System;

public class NewEnemyScript : MonoBehaviour
{

    public Animator AnimEnemy;
    public Transform Enemy;
    public GameObject Player;
    Root aiRoot = BT.Root();
    public NavMeshAgent agent;
    public Transform enemygoal;
    public Transform firstpoint;

    private GameObject[] points;
    public float timer;
    public int i = 0;

    public bool canshoot;
    public GameObject aim;
    public GameObject enemybullet;
    public GameObject clonedbullet;

    public AudioClip shootSound;

     void Fire()
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position, 1);
        aim.transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y - 1, Player.transform.position.z));
        clonedbullet = Instantiate(enemybullet, new Vector3(aim.transform.position.x, aim.transform.position.y + 0.05f, aim.transform.position.z), aim.transform.rotation) as GameObject;
        clonedbullet.GetComponent<Rigidbody>().AddForce(transform.forward * 750);
    }

    IEnumerator Shooting()
    {
        canshoot = true;
        yield return new WaitForSeconds(1);
        Fire();
        yield return new WaitForSeconds(1);
        canshoot = false;

    }

    private void OnEnable() 
    {

        points = GameObject.FindGameObjectsWithTag("Patrol2");
        //agent.destination = points[i].transform.position;
        aiRoot.OpenBranch(BT.If(VisibleTarget).OpenBranch(BT.Call(Chase)), BT.If(ShootCheck).OpenBranch(BT.Call(Shoot)), BT.If(NotVisibleTarget).OpenBranch(BT.Call(Walk)/*, BT.Wait(2.0f), BT.Call(Turn), BT.Wait(2.0f), BT.Call(Turn)*/));
    }

    private void Turn()
    { 
        Enemy.transform.Rotate(0, 180, 0);
        //Debug.Log("Turn"); 
    }

    private void Walk() 
    { 
        //Debug.Log("Walk");
        AnimEnemy.SetTrigger("BackToIdle");
        AnimEnemy.ResetTrigger("NotIdleAttack");
        AnimEnemy.ResetTrigger("Idle");
        //agent.SetDestination(firstpoint.position);

        float dist = Vector3.Distance(points[i].transform.position, transform.position);

        if (dist < 0.5f)
        {
            timer += Time.deltaTime;
            if (timer > 3)
            {
                timer = 0;
                i++;
                if (timer <= 0)
                {
                   AnimEnemy.ResetTrigger("Idle");
                   AnimEnemy.SetTrigger("BackToIdle");
                }
            }
            if (i < points.Length)
            {
                agent.destination = points[i].transform.position;
            }
        }
        else
        {
            agent.destination = points[i].transform.position;
        }
       
        if (i == points.Length)
        {
            i = 0;
            agent.destination = points[i].transform.position;
        }
    }

    private bool ShootCheck()
    {
        float enemytoplayer = Vector3.Distance(Player.transform.position, transform.position);

        if (enemytoplayer < 10.0f)
        {
            canShoot = true;
            //Enemy.transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y - 1, Player.transform.position.z));
            Debug.Log("CanPlayerBeShot:" + canShoot);
        }
        if (enemytoplayer > 10.0f)
        {
            canShoot = false;
            Debug.Log("CanPlayerBeShot:" + canShoot);
        }
        return canShoot;
    }

    private void Shoot() 
    {
        if(canShoot == true)
        {
            Enemy.transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y - 1, Player.transform.position.z));
        }
        isVisibleSuccess = false;
        isNotVisibleSuccess = false;
        agent.GetComponent<NavMeshAgent>().isStopped = true;
        AnimEnemy.ResetTrigger("BackToIdle");
        AnimEnemy.ResetTrigger("NotIdleAttack");
        AnimEnemy.SetTrigger("Crouch");
        if (!canshoot)
        {
            StartCoroutine(Shooting());
        }
        //Debug.Log("Shoot"); 
    }


    private void Chase() 
    {
        agent.GetComponent<NavMeshAgent>().isStopped = false;
        Enemy.transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y - 1, Player.transform.position.z));
        AnimEnemy.SetTrigger("NotIdleAttack");
        AnimEnemy.ResetTrigger("Idle");
        AnimEnemy.ResetTrigger("BackToIdle");
        agent.SetDestination(enemygoal.position);
        //return true;
        //Debug.Log("Chase"); 
    }

    public bool isVisibleSuccess;
    public bool isNotVisibleSuccess;
    public bool canShoot;

    private bool VisibleTarget() 
    {
        float enemytoplayer = Vector3.Distance(Player.transform.position, transform.position);

        if (enemytoplayer < 20.0f && enemytoplayer > 10.0f)
        {
            isVisibleSuccess = true;
            Debug.Log("TargetIsVisible Result:" + isVisibleSuccess);
            isNotVisibleSuccess = false;
        }
        return isVisibleSuccess;
    }

    private bool NotVisibleTarget()
    {
        float enemytoplayer = Vector3.Distance(Player.transform.position, transform.position);

        if (enemytoplayer > 20.0f)
        {
            isNotVisibleSuccess = true;
            Debug.Log("TargetIsNOTVisible Result:" + isNotVisibleSuccess);
            isVisibleSuccess = false;
        }
        return isNotVisibleSuccess;
    }

    private void Update() 
    { 
        aiRoot.Tick(); 
    }
    public Root GetAIRoot() 
    { 
        return aiRoot; 
    }
}
