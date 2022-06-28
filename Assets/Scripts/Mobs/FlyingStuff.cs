using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingStuff : MonoBehaviour
{
    public float speed = 5;
    public float waitTime = .3f;
    public float turnSpeed = 90;
    public Transform player;
    public float radius;
    private int state;//0 - летит, 1 - долетел
    private Vector3 randomPoint;
    public float viewDistance;
    float viewAngle;
    void Start()
    {
        //viewAngle = spotlight.spotAngle;
        //originalSpotlightColour = spotlight.color;
        Vector3[] waypoints = new Vector3[10];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = radius * new Vector3(Random.Range(-1, 1), Random.Range(-0.5f, 0.5f), Random.Range(-1, 1));
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
    }

    // Update is called once per frame
    void Update()
    {
        if (CanSeePlayer())
        {

        }
        else
        {

        }
    }


    bool CanSeePlayer()
    {
        if (Vector3.Distance(transform.position, player.position) < viewDistance)
        {
            Vector3 dirToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, dirToPlayer);
            if (angleBetweenGuardAndPlayer < viewAngle / 2f)
            {
                if (!Physics.Linecast(transform.position, player.position))
                {
                    return true;
                }
            }
        }
        return false;
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];

        int targetWaypointIndex = 1;
        Vector3 targetWaypoint = waypoints[targetWaypointIndex];
        transform.LookAt(targetWaypoint);

        while (true)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if (transform.position == targetWaypoint)
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
                yield return StartCoroutine(TurnToFace(targetWaypoint));
            }
            yield return null;
        }
    }

    IEnumerator TurnToFace(Vector3 lookTarget)
    {
        Vector3 dirToLookTarget = (lookTarget - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(dirToLookTarget.z, dirToLookTarget.x) * Mathf.Rad2Deg;

        while (Mathf.Abs(Mathf.DeltaAngle(transform.eulerAngles.y, targetAngle)) > 0.05f)
        {
            float angle = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle, turnSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * angle;
            yield return null;
        }
    }


}
