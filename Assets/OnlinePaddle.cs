using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlinePaddle : MonoBehaviourPunCallbacks
{
    [SerializeField] float speed = 5;
    [SerializeField] float yMax = 4;

    float y;

    private void Update()
    {
        if(photonView.IsMine)
        {
            y += Input.GetAxis("Vertical1") * Time.deltaTime * speed;
            y = Mathf.Clamp(y, -yMax, yMax);
            transform.position = new Vector3(transform.position.x, y);
        }
    }
}
