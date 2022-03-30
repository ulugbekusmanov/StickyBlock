using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;

    private Rigidbody rb;

    [SerializeField] private bool isGrounded;

    void Start()
    {
        Debug.Log("OnStart");

        rb = GetComponent<Rigidbody>();
        GetComponent<Renderer>().material = playerManager.playerColor;
        playerManager.collidedList = new List<GameObject> {gameObject};
        Debug.Log(playerManager.collidedList.Count);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Grounded();
        }
    }

    void Grounded()
    {
        isGrounded = true;
        playerManager.playerState = PlayerManager.PlayerState.Move;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        Destroy(this, 1f);
    }
    
}