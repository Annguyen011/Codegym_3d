using System;
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
            if (m_waypoints.Count == 0)
            {
                this.GetWaypoint();
            }

        }
        private void Start()
        {
            m_rb.centerOfMass = _certerOffMass;
            if (m_waypoints.Count == 0)
            {
                this.GetWaypoint();
            }
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
            foreach (Transform waypoint in tempWaypoints.transform)
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
            this.GetInput();
        }
        private void LateUpdate()
        {
            Steer();
            Move();
        }

        // ================================= Car moving =================================
        public enum Axel
        {
            Front,
            Rear
        }
        [Serializable]
        public struct Wheel
        {
            public GameObject wheelModel;
            public WheelCollider wheelCollider;
            public Axel axel;
        }
        public float maxAcceleration = 30f;
        public float breakeAcceleration = 50f;
        public float turnSensivity = 1f;
        public float maxSteerAngle = 30f;
        public Vector3 _certerOffMass;  
        public List<Wheel> wheels;
        float moveInput;
        float steerInput;
        void GetInput()
        {
            steerInput   = Input.GetAxis("Horizontal");
            moveInput = Input.GetAxis("Vertical");
        }
        void Move()
        {
            foreach (var wheel in wheels)
            {
                wheel.wheelCollider.motorTorque = moveInput * m_Speed* maxAcceleration * Time.deltaTime;
            }
        }
        void Steer()
        {
            foreach(var wheel in wheels)
            {
                if (wheel.axel == Axel.Front)
                {
                    var _steerAngle = steerInput * turnSensivity * maxSteerAngle;
                    wheel.wheelCollider.steerAngle = Mathf.Lerp(wheel.wheelCollider.steerAngle, _steerAngle, 0.06f);
                }
            }
        }
    }

}