using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Codegym
{

    public class PlayerControler : MonoBehaviour
    {

        // Declare variables
        [SerializeField] private List<Transform> m_waypoints;
        [SerializeField] private float m_Speed;
        [SerializeField] private float m_RotationSpeed;
        [SerializeField] private DriveMode m_driveMode;
        


        private Rigidbody m_rb;
        private int currentIndexWaypoint;
        private void Awake()
        {
            m_rb = GetComponent<Rigidbody>();
        }
        private void Reset()
        {
            this.GetWaypoint();
        }
        private void Start()
        {
            this.GetWaypoint();
        }
        private void FixedUpdate()
        {
            this.SetDriveMode(m_driveMode);
        }
        public enum DriveMode
        {
            Manual,
            Automatic
        }
        public void SetDriveMode(DriveMode mode)
        {
            m_driveMode = mode;
            if (m_driveMode == DriveMode.Automatic)
            {
                this.AutoRunInWay();
            }
            else
            {
                this.Moving();
            }
        }
        public void GetWaypoint()
        {
            GameObject tempWaypoints = GameObject.Find("Waypoints");
            foreach(Transform waypoint in tempWaypoints.transform)
            {
                this.m_waypoints.Add(waypoint);
            }
        }
        
        public void AutoRunInWay()
        {
             
            if (currentIndexWaypoint >= m_waypoints.Count)
            {
                currentIndexWaypoint = 0;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_waypoints[currentIndexWaypoint].position, m_Speed);
            transform.LookAt(m_waypoints[currentIndexWaypoint].position);
            if (transform.position == m_waypoints[currentIndexWaypoint].position)
            {
                currentIndexWaypoint++;
            }
        }
        public void Moving()
        {

            float moveInput = Input.GetAxis("Vertical");
            float rotateInput = Input.GetAxis("Horizontal");

            // Di chuyển theo hướng trước và sau
            Vector3 movement = transform.forward * moveInput * m_Speed;
            m_rb.AddForce(movement);

            // Quay đầu
            Quaternion rotation = Quaternion.Euler(0f, rotateInput * m_RotationSpeed * Time.fixedDeltaTime, 0f);
            m_rb.MoveRotation(m_rb.rotation * rotation);
        }

    }
}