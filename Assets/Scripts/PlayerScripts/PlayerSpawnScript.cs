using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnScript : MonoBehaviour
{
    Rigidbody2D rb2D;

    public Transform spawnPos;
    public GameObject Player;

    void SpawnPlayer()
    {
        Instantiate(Player, spawnPos.position, spawnPos.rotation);
    }

    private void Start()
    {
        SpawnPlayer();
    }

    void Update()
    {/*
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 0 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.green);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.red);
            Debug.Log("Did not Hit");
        }*/
    }
}

