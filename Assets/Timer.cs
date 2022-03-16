using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    public float timerRemaining = 3;
    public bool timerIsRunning = false;
    public Text timeText;
    public string GameName;


    private void Start()
    {
        timerIsRunning = true;

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(timerRemaining);
        if(timerRemaining > 0)
        {
            timerRemaining -= Time.deltaTime;
            DisplayTime(timerRemaining);
        }
        else 
        {
            timerRemaining = 0;
            timerIsRunning = false;
            Time.timeScale = 0;
        }

        if(timerIsRunning == false && Input.GetKeyDown("return"))
        {
            SceneManager.LoadScene(GameName, LoadSceneMode.Single);
            timerIsRunning = true;
            Time.timeScale = 1;
        }

    }

    void DisplayTime(float timeToDisplay)
    {
        if(timerIsRunning == true)
        {
            float minutes = Mathf.FloorToInt(timerRemaining/60);
            float seconds = Mathf.FloorToInt(timerRemaining%60);
            float milliSeconds = (timeToDisplay % 1 * 1000);
            timeText.text = string.Format("{0:00}:{1:00}:{2:000}",minutes,seconds,milliSeconds);
        }
    }

    void OnGUI()
    {
        if(timerIsRunning == false)
        {
            GUI.Box(new Rect(0,50,250,25), "Fuck you, press enter du restart");
        }
    }

}
