using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float speed;
    private Waypoints Wpoints;
    private int waypointIndex;

    public Transform target;

    public float awarenessRange;
    public float distanceToTarget;
    private float temprange;

    public float pSpeed;

    public bool spotted = false;

    public float distance;

    public GameObject Player;

    // Use this for initialization
    void Start()
    {

        Wpoints = GameObject.FindGameObjectWithTag("Waypoints").GetComponent<Waypoints>();

    }

    // Update is called once per frame
    void Update()
    {

        if (PlayerController.tempSpeed < PlayerController.pspeed)
        {
            temprange = awarenessRange / 2;
        }
        else
        {
            temprange = awarenessRange;
        }

        Player = GameObject.FindGameObjectWithTag("Player");

        //check the distance to the player
        distanceToTarget = Vector3.Distance(transform.position, target.position);
        //check if the enemy is aware of player, if not then patrol

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, (Player.transform.position - transform.position), distance);

        if (distanceToTarget < temprange && hitInfo.collider != null && hitInfo.collider.CompareTag("Player"))
        {

            Debug.DrawLine(transform.position, hitInfo.point, Color.red);

            spotted = true;

            Chase();
        }

        else
        {
            Debug.DrawLine(transform.position, hitInfo.point, Color.green);

            spotted = false;

            Patrol();

        }
    }

    void Chase()
    {
        //chase 
        float distanceToTarget2 = Vector3.Distance(transform.position, target.position);
        if (distanceToTarget2 < temprange)
        {
            Vector3 targetDir = target.position - transform.position;
            float angle2 = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.AngleAxis(angle2, Vector3.forward);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, q, 180);

            transform.Translate(Vector3.up * Time.deltaTime * speed);
        }
    }

    void Patrol()
    {
        //patrol
        transform.position = Vector2.MoveTowards(transform.position, Wpoints.waypoints[waypointIndex].position, speed * Time.deltaTime);

        //change direction when moving between waypoints
        Vector3 dir = Wpoints.waypoints[waypointIndex].position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;
        Quaternion p = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, p, 180f);

        if (Vector2.Distance(transform.position, Wpoints.waypoints[waypointIndex].position) < 0.1f)
        {
            waypointIndex++;
        }

        if (waypointIndex == Wpoints.waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
