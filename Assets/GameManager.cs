using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    bool gameRunning;
    int score1, score2;

    [SerializeField] Ball ball;

    [SerializeField] TMP_Text textScore1;
    [SerializeField] TMP_Text textScore2;

    private void Update()
    {
        if (gameRunning) return;

        if(Input.GetKeyDown(KeyCode.Space))
        {
            gameRunning = true;
            ball.StartBall();
        }
    }

    public void Score(bool player1)
    {
        gameRunning = false;
        if (player1)
            score1++;
        else 
            score2++;

        DisplayScore();
    }

    void DisplayScore()
    {
        textScore1.text = score1.ToString();
        textScore2.text = score2.ToString();
    }
}
