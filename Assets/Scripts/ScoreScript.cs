using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;

    public int scoreValue = 0;
    public Text score;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Score: " + scoreValue.ToString();
        score = GetComponent<Text>();
    }

    public void setScore(int s)
    {
        scoreValue = s;
        score.text = "Score: " + scoreValue.ToString();

        GameObject[] blackSheepSpawned;
        blackSheepSpawned = GameObject.FindGameObjectsWithTag("BlackSquare");

        GameObject[] whiteSheepSpawned;
        whiteSheepSpawned = GameObject.FindGameObjectsWithTag("WhiteSquare");
    }
}
