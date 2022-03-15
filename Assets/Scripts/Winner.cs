using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Winner : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text winner;

    // Start is called before the first frame update
    void Awake()
    {
        if (PlayersScore.p1Score > PlayersScore.p2Score)
        {
            winner.text = "Player 1 Wins!";
        }
        else if (PlayersScore.p2Score > PlayersScore.p1Score)
        {
            winner.text = "Player 2 Wins!";

        }
        else
        {
            winner.text = "It's a tie!";
        }

    }

}
