using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieBehavior : MonoBehaviour
{
    public float speed = 5.0f;
    public Waypoint[] waypoints;
    private int currentWaypoint = 0;
    private float chooseCooldown = 0.0f;
    public float chooseDelay = 1.0f;

    void Update()
    {
        if (waypoints == null || currentWaypoint >= waypoints.Length || waypoints[currentWaypoint] == null)
        {
            return;
        }

        if (currentWaypoint < waypoints.Length)
        {
            Vector3 direction = new Vector3(waypoints[currentWaypoint].transform.position.x - transform.position.x, 0, waypoints[currentWaypoint].transform.position.z - transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }

        if (chooseCooldown > 0.0f)
        {
            chooseCooldown -= Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (currentWaypoint < waypoints.Length && waypoints[currentWaypoint] != null &&
        other.gameObject == waypoints[currentWaypoint].gameObject && chooseCooldown <= 0.0f)
        {
            ChooseNextWaypoint();
            chooseCooldown = chooseDelay;
        }
    }

    void ChooseNextWaypoint()
    {
        Waypoint current = waypoints[currentWaypoint];
        if (current.connectedWaypoints.Length > 0)
        {
            int nextIndex = Random.Range(0, current.connectedWaypoints.Length);
            currentWaypoint = System.Array.IndexOf(waypoints, current.connectedWaypoints[nextIndex]);
        }
        else
        {
            currentWaypoint++;
        }
    }
}
