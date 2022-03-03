using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer2 : MonoBehaviour
{

    float countDown = 300.0f; //time in seconds
    public TMPro.TMP_Text textVariable; //[SerializeField] private TMPro.TMP_Text textVariable;

    void Score()
    {
        textVariable.text = "Start time";
    }
    void Update()
    {
        if (countDown > 0)
        {
            countDown -= Time.deltaTime;
        }
        string minute = Mathf.Floor(countDown / 60).ToString("00");
        string seconds = (countDown % 60).ToString("00");
        //double currTime = System.Math.Round (countDown, 2);     
        textVariable.text = minute + ":" + seconds; //currTime.ToString();     
        if (countDown < 0)
        {
            Debug.Log("Completed");
            SceneManager.LoadScene("Credits");
        }
    }

}