using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class P1ScoreTMP : MonoBehaviour
{
    public static P1ScoreTMP instance;

    public int player1Score = 0;

    public TMPro.TMP_Text score;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        score.text = "P1 Score: " + player1Score.ToString();
    }

    void Update()
    {
        player1Score = 0;
        GameObject[] blackSheepSpawned;
        blackSheepSpawned = GameObject.FindGameObjectsWithTag("BlackSquare");

        GameObject[] whiteSheepSpawned;
        whiteSheepSpawned = GameObject.FindGameObjectsWithTag("WhiteSquare");

        foreach (GameObject sheep in blackSheepSpawned)
        {
            float sheepX = sheep.transform.position.x;
            float sheepY = sheep.transform.position.y;

            //upper left square
            if (sheepX > -16 && sheepX < -10 && sheepY < 9 && sheepY > 3)
            {
                player1Score += 1;
            }

            //lower left square, incorrectly sorted
            if (sheepX > -16 && sheepX < -10 && sheepY < -3 && sheepY > -9)
            {
                player1Score -= 2;
            }


        }


        foreach (GameObject sheep in whiteSheepSpawned)
        {
            float sheepX = sheep.transform.position.x;
            float sheepY = sheep.transform.position.y;

            //lower left square
            if (sheepX > -16 && sheepX < -10 && sheepY < -3 && sheepY > -9)
            {
                player1Score += 1;
            }

            //upper left square, incorrectly sorted
            if (sheepX > -16 && sheepX < -10 && sheepY < 9 && sheepY > 3)
            {
                player1Score -= 2;
            }

        }


        score.text = "P1 Score: " + player1Score.ToString();
    }
}
