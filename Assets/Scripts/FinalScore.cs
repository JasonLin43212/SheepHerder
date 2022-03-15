using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FinalScore : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text finalScore;

    // Start is called before the first frame update
    void Awake()
    {
        finalScore = GetComponent<TMP_Text>();

        finalScore.text = "P1: " + PlayersScore.p1Score + "    v.s.    P2: " + PlayersScore.p2Score;
    }

}
