using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    int status = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Btn_Pressed()
    {
         SceneManager.LoadSceneAsync("Lesson7");
    }
    public void Btn_Pause()
    {
        if (status == 0)
        {
            Time.timeScale = 1;
            status = 1;
        }
        else
        {
            Time.timeScale = 0;
            status = 0;
        }
    }
}
