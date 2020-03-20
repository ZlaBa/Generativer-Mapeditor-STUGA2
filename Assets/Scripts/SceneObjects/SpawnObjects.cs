using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    GameData gameData;
    //Foodprefab in Tutorial - Zu spawnendes Objekt
    public GameObject TreePrefab;

    public Vector3 center;
    public Vector3 size;

    public Quaternion min;

    void Start()
    {
        gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();
    }

    void Update()
    {
        SpawnTree();
    }
    //SpawnFood in Tutorial
    public void SpawnTree()
    {//(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)
        Vector3 pos = center;
        Instantiate(TreePrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
