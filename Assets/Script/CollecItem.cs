using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollecItem : MonoBehaviour
{
    [SerializeField] private float m_Hp;
    [SerializeField] private float m_Damaged;
    [SerializeField] private float m_Fuel;
    [SerializeField] private float m_Capacity;
    [SerializeField] private float m_Laps;
    // Start is called before the first frame update
    void Start()
    {
        m_Hp = 100;
        m_Damaged = 0;
        m_Fuel = 0;
        m_Capacity = 0;
        m_Laps = 0;


    }

    // Update is called once per frame
    void Update()
    {
        if (m_Damaged == 100)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }   
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            m_Laps++;
        }
        if (other.CompareTag("Fuel"))
        {

            m_Damaged += 20;
        }
        if (other.CompareTag("Capacity"))
        {

            m_Capacity += 10;
        }
        if (other.CompareTag("Blackbox"))
        {

            m_Damaged = m_Damaged * 100 / 30f;
        }
        if (other.CompareTag("Box"))
        {

            m_Damaged = 0;
        }
    }
}
