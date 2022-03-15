using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text textVariable; 

    private float countDown = 200.0f; //time in seconds

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
        textVariable.text = minute + ":" + seconds; //currTime.ToString();     
        if (countDown < 0)
        {
            Debug.Log("Completed");
            SceneManager.LoadScene("Credits");
        }
    }

}