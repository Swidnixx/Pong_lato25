using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float speed = 5;

    [Range(1,2)]
    [SerializeField] int playerNumber = 1;

    [SerializeField] float yMax = 4;

    float y;

    private void Update()
    {
        y += Input.GetAxis("Vertical" + playerNumber) * Time.deltaTime * speed;
        y = Mathf.Clamp(y, -yMax, yMax);
        transform.position = new Vector3(transform.position.x, y);
    }
}
