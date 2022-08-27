using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sensor : MonoBehaviour
{
    public LayerMask hitMask;
    public enum Type
    {
        Line,
        Raybundle,
        Spherecast,
        Boxcast
    }

    public Animator animIdle;

    public GameObject enemy;
    public Transform goal;

    private GameObject[] points;
    public NavMeshAgent agent;
    private int i = 0;
    public float timer = 0;
    public GameObject Bullet;

    public int hitpoints = 2;
    public GameObject gun;
    public SphereCollider collider;
    public GameObject player;
    public float distance = 7.0f;

    public GameObject enemygun;



    public Type sensorType = Type.Line;
    public float raycastLength = 1.0f;

    [Header("BoxExtent Settings")]
    public Vector2 boxExtents = new Vector2(1.0f, 1.0f);

    [Header("Sphere Settings")]
    public float sphereRadius = 1.0f;

    [Header("RayBundle Settings")]
    [Range(2.0f, 10.0f)]
    public float RayAmount;
    [Range(0.0f, 360.0f)]
    public float SpotAngle;

    Transform CachedTransform;
    // Start is called before the first frame update
    void Start()
    {

        collider.radius = 10.0f;

        //gun.SetActive(false);

        points = GameObject.FindGameObjectsWithTag("Patrol4");

        player = GameObject.FindGameObjectWithTag("Player");

        agent.destination = points[i].transform.position;

        animIdle = GetComponentInParent<Animator>();

        CachedTransform = GetComponent<Transform>();
    }

    public bool Hit { get; private set; }
    public RaycastHit info = new RaycastHit();

    public bool Scan()
    {
        Hit = false;
        Vector3 Direction = CachedTransform.forward;
        Collider[] Collisions;

        switch (sensorType)
        {
            case Type.Line:
                if (Physics.Linecast(CachedTransform.position, CachedTransform.position + Direction * raycastLength, out info, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Hit = true;
                    //Debug.Log("Hit");
                    if (info.collider.gameObject.CompareTag("Enemy"))
                    {
                        agent.SetDestination(goal.position);
                    }
                    return true;
                }
                if (!Physics.Linecast(CachedTransform.position, CachedTransform.position + Direction * raycastLength, out info, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Hit = false;
                    //Debug.Log("Not Hit");
                    return true;
                }
                break;
            case Type.Raybundle:
                if (Physics.Linecast(CachedTransform.position, CachedTransform.position + Direction * raycastLength, out info, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Hit = true;
                    //Debug.Log("Hit");
                    return true;
                }
                break;
            case Type.Spherecast:
                //(Physics.SphereCast(new Ray(CachedTransform.position, Direction), sphereRadius, out info, raycastLength, hitMask, QueryTriggerInteraction.Ignore))
                Collisions = Physics.OverlapSphere(this.transform.position, sphereRadius, hitMask, QueryTriggerInteraction.Ignore);
                for (int i = 0; i < Collisions.Length; i++)
                    if (Collisions[i].tag == "Player" /*&& flashlight.enabled*/)
                    {
                        Hit = true;
                        //Debug.Log("Hit");
                        agent.SetDestination(goal.position);
                        // gun.SetActive(true);
                        return true;
                    }
                if (Collisions[i].tag != "Player" /*&& flashlight.enabled*/)
                {
                    Hit = false;
                    //Debug.Log("Not Hit");
                    agent.destination = points[i].transform.position;
                    //gun.SetActive(false);
                    return true;
                }
                /*Hit = true;
                Debug.Log("Hit");
                if (info.collider.gameObject.CompareTag("Enemy"))
                {
                    agent.SetDestination(goal.position);
                }
                return true;*/
                break;
            case Type.Boxcast:
                if (Physics.CheckBox(this.transform.position, new Vector3(boxExtents.x, boxExtents.y, raycastLength) / 2.0f, this.transform.rotation, hitMask, QueryTriggerInteraction.Ignore))
                {
                    Hit = true;
                    if (Hit == true)
                    {
                        agent.SetDestination(goal.position);
                    }
                    else
                    {
                    }
                    return true;
                }
                break;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        if (CachedTransform == null)
        {
            CachedTransform = GetComponent<Transform>();
        }
        Scan();
        if (Hit) Gizmos.color = Color.red;
        Gizmos.matrix *= Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        float length = raycastLength;
        switch (sensorType)
        {
            case Type.Line:
                if (Hit) length = Vector3.Distance(this.transform.position, info.point);
                Gizmos.DrawLine(Vector3.zero, Vector3.forward * length);
                Gizmos.color = Color.green;
                Gizmos.DrawCube(Vector3.forward * length, new Vector3(0.02f, 0.02f, 0.02f));
                break;
            case Type.Raybundle:
                if (Hit) length = Vector3.Distance(this.transform.position, info.point);

                Quaternion leftRayRotation = Quaternion.AngleAxis(-SpotAngle / 2, Vector3.up);
                Quaternion rightRayRotation = Quaternion.AngleAxis(SpotAngle / 2, Vector3.up);
                Vector3 leftRayDirection = leftRayRotation * Vector3.forward;
                Vector3 rightRayDirection = rightRayRotation * Vector3.forward;
                Gizmos.DrawLine(Vector3.zero, leftRayDirection * length);
                Gizmos.DrawLine(Vector3.zero, rightRayDirection * length);
                Gizmos.DrawLine(Vector3.zero, Vector3.forward * length);
                break;
            case Type.Spherecast:
                Gizmos.DrawWireSphere(Vector3.zero, sphereRadius);
                /*if (Hit)
                {
                    Vector3 ballCenter = info.point + info.normal * sphereRadius;
                    length = Vector3.Distance(CachedTransform.position, ballCenter);
                }*/

                //float halfExtents = sphereRadius;
                //Gizmos.DrawLine(Vector3.up * halfExtents, Vector3.up * halfExtents + Vector3.forward * length);
                // Gizmos.DrawLine(-Vector3.up * halfExtents, -Vector3.up * halfExtents + Vector3.forward * length);
                // Gizmos.DrawLine(Vector3.right * halfExtents, Vector3.right * halfExtents + Vector3.forward * length);
                // Gizmos.DrawLine(-Vector3.right * halfExtents, -Vector3.right * halfExtents + Vector3.forward * length);

                //Gizmos.DrawWireSphere(Vector3.zero + Vector3.forward * length, sphereRadius);
                break;
            case Type.Boxcast:
                Gizmos.DrawWireCube(Vector3.zero, new Vector3(boxExtents.x, boxExtents.y, raycastLength));
                break;
        }
    }

    IEnumerator OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            hitpoints = hitpoints - 1;
            animIdle.SetBool("IsHit", true);
            animIdle.SetBool("CrouchHit", true);
            //collision.gameObject.SetActive(false);
            Destroy(collision.gameObject, 0.35f);
        }
        yield return new WaitForSeconds(0.7f);
        animIdle.SetBool("IsHit", false);
        animIdle.SetBool("CrouchHit", false);

        if (hitpoints <= 0)
        {
            Destroy(enemygun);
            agent.GetComponent<NavMeshAgent>().isStopped = true;
            animIdle.SetBool("Dead", true);
            yield return new WaitForSeconds(5.0f);
            Destroy(enemy);
            //gun.SetActive(false);
        }
        yield return null;
    }

    /*void OnCollisionExit(Collision collision)
    {
        animIdle.SetBool("IsHit", false);
    }*/
    public GameObject enemybullet;
    public GameObject clonedbullet;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animIdle.SetTrigger("NotIdleAttack");
            animIdle.ResetTrigger("Idle");
            animIdle.ResetTrigger("BackToIdle");
            //gun.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            animIdle.SetTrigger("BackToIdle");
            animIdle.ResetTrigger("Idle");
            //animIdle.ResetTrigger("NotIdleAttack");
            //gun.SetActive(false);
        }
    }
    public bool canshoot;
    public GameObject aim;
    public AudioClip shootSound;

    void Fire()
    {
        AudioSource.PlayClipAtPoint(shootSound, transform.position, 1);
        aim.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z));
        clonedbullet = Instantiate(enemybullet, new Vector3(aim.transform.position.x, aim.transform.position.y + 0.05f, aim.transform.position.z), aim.transform.rotation) as GameObject;
        clonedbullet.GetComponent<Rigidbody>().AddForce(transform.forward * 750);
    }

    IEnumerator Shoot()
    {
        canshoot = true;
        yield return new WaitForSeconds(1);
        Fire();
        yield return new WaitForSeconds(1);
        canshoot = false;

    }

    void Update()
    {

        if (MovingLight.flockenemies == true)
        {
            //soldier1.transform.position = player.transform.position;
            agent.SetDestination(goal.position);
            points = null;
        }


        if (!Pause.ispaused)
        {
            if (Input.GetMouseButtonDown(0) && Shooting.ammo > 0)
            {
                sphereRadius = sphereRadius + 5;
                collider.radius = collider.radius + 3.0f;
                distance = distance + 3;
            }
        }

        if (sphereRadius > 16)
        {
            sphereRadius -= Time.deltaTime;

        }
        if (collider.radius > 10)
        {
            collider.radius -= Time.deltaTime;
        }

        //agent.SetDestination(startpoint.position);
        /*if (Vector3.Distance(agent.transform.position, startpoint.position) < 0.3f)
        {
            animIdle.SetTrigger("Idle");
        }
        if (Vector3.Distance(agent.transform.position, startpoint.position) > 0.3f)
        {
            animIdle.ResetTrigger("Idle");
        }*/


        float disttoplayer = Vector3.Distance(player.transform.position, transform.position);

        if (disttoplayer < distance)
        {
            enemy.transform.LookAt(new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z));
            agent.GetComponent<NavMeshAgent>().isStopped = true;
            animIdle.ResetTrigger("BackToIdle");
            animIdle.ResetTrigger("NotIdleAttack");
            animIdle.SetTrigger("Crouch");
            if (!canshoot)
            {
                StartCoroutine(Shoot());
            }
        }

        if (disttoplayer > distance)
        {
            agent.GetComponent<NavMeshAgent>().isStopped = false;
            animIdle.ResetTrigger("BackToIdle");
            animIdle.ResetTrigger("Idle");
            animIdle.SetTrigger("NotIdleAttack");
        }

        float dist = Vector3.Distance(points[i].transform.position, transform.position);

        if (dist < 0.5f)
        {
            animIdle.ResetTrigger("NotIdleAttack");
            animIdle.SetTrigger("Idle");
            timer += Time.deltaTime;
            if (timer > 3)
            {

                timer = 0;
                i++;
                if (timer <= 0)
                {
                    animIdle.ResetTrigger("Idle");
                    animIdle.SetTrigger("BackToIdle");
                }
            }
            if (i < points.Length)
            {

                agent.destination = points[i].transform.position;
            }
        }


        if (i == points.Length)
        {
            i = 0;
            agent.destination = points[i].transform.position;
        }

        OnDrawGizmos();
    }
}
