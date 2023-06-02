using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DriveMode2
{
    Manual,
    Automatic
}
public class DriveMode : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float manualRotationSpeed = 100f;
    [SerializeField] private DriveMode2 driveMode = DriveMode2.Automatic;
    public Transform[] waypoints;
    public float distanceThreshold = 0.1f; // how close the player needs to be to a waypoint before moving to the next one

    private int currentWaypoint = 0;

    // Define the input axes for manual control
    private string horizontalAxis = "Horizontal";
    private string verticalAxis = "Vertical";
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (driveMode)
        {
            case DriveMode2.Automatic:
                Automatic();
                break;
            case DriveMode2.Manual:
                Manual();
                break;
        }
    }

    private void Manual()
    {
        float horizontalInput = Input.GetAxis(horizontalAxis); // get input for horizontal movement
        float verticalInput = Input.GetAxis(verticalAxis); // get input for vertical movement

        transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime); // move car forward/backward
        transform.Rotate(Vector3.up, horizontalInput * manualRotationSpeed * Time.deltaTime); // rotate car left/right
    }

    private void Automatic()
    {
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

