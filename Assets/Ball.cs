using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 5;

    private void Update()
    {
        Debug.Log(GetComponent<Rigidbody>().velocity.sqrMagnitude);
    }

    public void StartBall()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2 vel = new Vector2(x, y) * speed;
        GetComponent<Rigidbody>().velocity = vel;
    }

    void Stop()
    {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void OnTriggerEnter(Collider other)
    {
        bool player1Score = other.name == "WallE";
        GameManager.Instance?.Score(player1Score);
        OnlineGameManager.Instance?.Score(player1Score);
        Stop();
    }
}
