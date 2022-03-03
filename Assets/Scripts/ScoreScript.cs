using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript instance;

    public int player1Score = 0;
/*    public int player2Score = 0;
*/
    public Text score;
/*    public Text score2;
*/
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        score.text = "Player 1 Score: " + player1Score.ToString();
        score = GetComponent<Text>();

/*        score2.text = "Player 2 Score: " + player2Score.ToString();
        score2 = GetComponent<Text>();*/
    }

    void Update()
    {
        player1Score = 0;
/*        player2Score = 0;
*/        GameObject[] blackSheepSpawned;
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


/*
            //lower right square
            if (sheepX > 9 && sheepX < 15 && sheepY < -3 && sheepY > -9)
            {
                player2Score += 1;
            }*/


        }


        foreach (GameObject sheep in whiteSheepSpawned)
        {
            float sheepX = sheep.transform.position.x;
            float sheepY = sheep.transform.position.y;

 /*           //upper right square
            if (sheepX > 9 && sheepX < 15 && sheepY < 9 && sheepY > 3)
            {
                player2Score += 1;
            }*/

            //lower left square
            if (sheepX > -16 && sheepX < -10 && sheepY < -3 && sheepY > -9)
            {
                player1Score += 1;
            }

        }


        score.text = "Player 1 Score: " + player1Score.ToString();
    }
}
