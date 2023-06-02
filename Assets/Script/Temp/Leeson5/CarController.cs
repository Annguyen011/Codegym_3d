using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController
    : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 10f;
    public float turnSpeed = 10f;

    private Rigidbody rb;
    private BoxCollider boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Move the car forward/backward
        Vector3 movement = transform.forward * moveInput * speed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);

        // Rotate the car left/right
        Quaternion turnRotation = Quaternion.Euler(0f, turnInput * turnSpeed * Time.fixedDeltaTime, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);

        // Clamp the car within the bounds of the racetrack
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, boxCollider.bounds.min.x, boxCollider.bounds.max.x);
        clampedPosition.z = Mathf.Clamp(clampedPosition.z, boxCollider.bounds.min.z, boxCollider.bounds.max.z);
        transform.position = clampedPosition;
    }

    // Draw the bounds of the racetrack in the scene view
    private void OnDrawGizmos()
    {
        if (boxCollider != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(boxCollider.bounds.center, boxCollider.bounds.size);
        }
    }
}
