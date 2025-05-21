using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OnlineGameManager : MonoBehaviourPunCallbacks
{
    public static OnlineGameManager Instance;
    private void Awake()
    {
        Instance = this;
    }

    bool gameRunning;
    bool gameOver;
    int score1, score2;

    [SerializeField] Ball ball;

    [SerializeField] TMP_Text textScore1;
    [SerializeField] TMP_Text textScore2;

    [SerializeField] GameObject gameOverText;

    [SerializeField] Transform p1, p2;

    private void Start()
    {
        CreatePlayer();
    }

    void CreatePlayer()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate("Paddle", p1.position, Quaternion.identity);
        }
        else
        {
            PhotonNetwork.Instantiate("Paddle", p2.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        if (gameRunning || gameOver) return;

        if(Input.GetKeyDown(KeyCode.Space) && PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
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

        if(score1 >= 2 || score2 >= 2)
        {
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator GameOverRoutine()
    {
        gameOver = true;
        gameOverText.SetActive(true);

        while(!Input.GetKeyDown(KeyCode.Return))
            yield return null;

        photonView.RPC(nameof(RestartGame), RpcTarget.All, null);
    }

    [PunRPC]
    void RestartGame()
    {
        gameOver = false;
        gameOverText.SetActive(false);
        score1 = 0;
        score2 = 0;

        gameRunning = false;
        DisplayScore();
    }

    void DisplayScore()
    {
        textScore1.text = score1.ToString();
        textScore2.text = score2.ToString();
    }
}
