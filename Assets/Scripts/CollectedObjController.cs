using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedObjController : MonoBehaviour
{
    private PlayerManager playerManager;

    [SerializeField] private Transform sphere;

    void Start()
    {
        Debug.Log(gameObject.tag);
        playerManager = GameObject.FindGameObjectWithTag("PlayerManager").GetComponent<PlayerManager>();
        sphere = transform.GetChild(0);
        sphere.tag = "SphereObj";
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("CollectibleObj"))
        {
            if (!playerManager.collidedList.Contains(other.gameObject))
            {
                other.gameObject.tag = gameObject.tag;
                other.transform.parent = playerManager.collectedPool;
                other.gameObject.GetComponent<Renderer>().material = playerManager.playerColor;
                playerManager.collidedList.Add(other.gameObject);
                
                other.gameObject.AddComponent<CollectedObjController>();
                Rigidbody rb = other.gameObject.AddComponent<Rigidbody>();
                rb.useGravity = false;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            playerManager.collidedList.Remove(gameObject);

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CollectibleList"))
        {
            other.transform.GetComponent<BoxCollider>().enabled = false;
            other.transform.parent = playerManager.collectedPool;
            foreach (Transform child in other.transform)
            {
                if (!playerManager.collidedList.Contains(child.gameObject))
                {
                    child.gameObject.GetComponent<Renderer>().material = playerManager.playerColor;

                    playerManager.collidedList.Add(child.gameObject);
                    child.gameObject.tag = gameObject.tag;
                    child.gameObject.AddComponent<CollectedObjController>();
                    Rigidbody rb = child.gameObject.AddComponent<Rigidbody>();
                    rb.useGravity = false;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                }
            }
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
            playerManager.collidedList.Remove(gameObject);
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            playerManager.levelState = PlayerManager.LevelState.Finshished;
            MakeSphere();
        }
    }

    private void MakeSphere()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = false;
    }

    public void DropObj()
    {
        sphere.gameObject.layer = 8;
        sphere.gameObject.GetComponent<SphereCollider>().isTrigger = false;
        sphere.gameObject.AddComponent<Rigidbody>();
        sphere.GetComponent<Rigidbody>().useGravity = true;
    }
}