using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f; // speed of the player
    public Transform[] waypoints; // an array of waypoints to follow
    public float distanceThreshold = 0.1f; // how close the player needs to be to a waypoint before moving to the next one

    private int currentWaypoint = 0; // the current waypoint the player is heading towards

    // Start is called before the first frame update
    void Start()
    {
        // make sure there are at least 2 waypoints
        if (waypoints.Length < 2)
        {
            Debug.LogError("Not enough waypoints");
            enabled = false; // disable this script
        }
    }

    // Update is called once per frame
    void Update()
    {
        // calculate direction to the current waypoint
        Vector3 direction = waypoints[currentWaypoint].position - transform.position;
        direction.y = 0f; // ignore y direction

        // if the player is close enough to the current waypoint, move to the next one
        if (direction.magnitude < distanceThreshold)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Length)
            {
                currentWaypoint = 0; // loop back to the start
            }
        }

        // normalize the direction vector and move the player towards the current waypoint
        transform.position += direction.normalized * speed * Time.deltaTime;

        // rotate the player towards the current waypoint
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
