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

    void Update()
    {
        scoreValue = 0;
        GameObject[] blackSheepSpawned;
        blackSheepSpawned = GameObject.FindGameObjectsWithTag("BlackSquare");

        GameObject[] whiteSheepSpawned;
        whiteSheepSpawned = GameObject.FindGameObjectsWithTag("WhiteSquare");

        foreach (GameObject sheep in blackSheepSpawned)
        {
            float sheepX = sheep.transform.position.x;
            float sheepY = sheep.transform.position.y;
            if (sheepX > -16 && sheepX < -10 && sheepY < 9 && sheepY > 3)
            {
                scoreValue += 1;
            }
        }

        score.text = "Score: " + scoreValue.ToString();

    }
}
