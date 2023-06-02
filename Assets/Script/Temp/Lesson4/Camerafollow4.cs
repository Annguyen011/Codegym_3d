using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camerafollow4 : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform target; // reference to the car object
    public float sensitivity = 5f; // mouse sensitivity for camera rotation
    public float distance = 5f; // distance from the camera to the target object

    private float currentRotationAngle = 0f;
    private float currentHeight = 0f;
    private float mouseX; // stores the mouse X input for rotation
    private float mouseY; // stores the mouse Y input for rotation

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // lock the mouse cursor to the center of the screen
    }

    void Update()
    {
        // Update camera rotation based on mouse input
        mouseX += Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -80f, 80f); // clamp the rotation angle to prevent flipping

        // Calculate the current rotation and position of the camera
        currentRotationAngle = mouseX;
        currentHeight = target.position.y + distance * Mathf.Sin(Mathf.Deg2Rad * mouseY);
        Vector3 currentPos = new Vector3(
            target.position.x + distance * Mathf.Cos(Mathf.Deg2Rad * mouseY) * Mathf.Sin(Mathf.Deg2Rad * currentRotationAngle),
            currentHeight,
            target.position.z + distance * Mathf.Cos(Mathf.Deg2Rad * mouseY) * Mathf.Cos(Mathf.Deg2Rad * currentRotationAngle));

        // Set the camera's position and look at the target object
        transform.position = currentPos;
        transform.LookAt(target.position);
    }
}
