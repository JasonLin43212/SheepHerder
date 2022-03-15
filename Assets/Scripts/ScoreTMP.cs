using UnityEngine;
using TMPro;

public class ScoreTMP : MonoBehaviour
{
    [SerializeField] private GameObject blackPen;
    [SerializeField] private GameObject whitePen;
    [SerializeField] private TMPro.TMP_Text score;
    [SerializeField] private int playerNum;

    private static ScoreTMP instance;

    private int playerScore = 0;

    private void updateScoreText() {

        if (playerNum == 1)
        {
            PlayersScore.p1Score = playerScore;
        }
        else
        {
            PlayersScore.p2Score = playerScore;
        }

        score.text = "P" + playerNum + " Score: " + playerScore.ToString();
    }

    void Awake()
    {
        instance = this;
   
    }

    void Start()
    {
        updateScoreText();
    }

    void Update()
    {
        Collider2D blackPenCollider = blackPen.GetComponent<Collider2D>();
        Collider2D whitePenCollider = whitePen.GetComponent<Collider2D>();

        Collider2D[] inBlackPen = Physics2D.OverlapBoxAll(blackPenCollider.bounds.center, blackPenCollider.bounds.size, 0);
        Collider2D[] inWhitePen = Physics2D.OverlapBoxAll(whitePenCollider.bounds.center, whitePenCollider.bounds.size, 0);

        foreach (Collider2D entity in inBlackPen)
        {
            GameObject entityObject = entity.gameObject;
            // It is a sheep
            if (entityObject.layer == 7) {
                bool readyToDisappear = entityObject.GetComponent<SheepMovement>().readyToDisappear();
                if (readyToDisappear) {
                    if (entityObject.tag == "BlackSquare") {
                        playerScore += 1;
                    } else if (entityObject.tag == "WhiteSquare") {
                        playerScore -= 2;
                    }
                    Destroy(entityObject);
                }
                
            }
        }

        foreach (Collider2D entity in inWhitePen)
        {
            GameObject entityObject = entity.gameObject;
            // It is a sheep
            if (entityObject.layer == 7) {
                bool readyToDisappear = entityObject.GetComponent<SheepMovement>().readyToDisappear();
                if (readyToDisappear) {
                    if (entityObject.tag == "BlackSquare") {
                        playerScore -= 2;
                    } else if (entityObject.tag == "WhiteSquare") {
                        playerScore += 1;
                    }
                    Destroy(entityObject);
                }
            }
        }

        updateScoreText();
    }
}
