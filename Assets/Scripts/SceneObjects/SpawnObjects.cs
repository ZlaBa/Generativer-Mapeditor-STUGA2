﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObjects : MonoBehaviour
{
    //Foodprefab in Tutorial
    public GameObject TreePrefab;

    public Vector3 center;
    public Vector3 size;

    public Quaternion min;

    void Start()
    {
        SpawnTree();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.T))
            SpawnTree();
    }
    //SpawnFood in Tutorial
    public void SpawnTree()
    {
        Vector3 pos = center + new Vector3(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2));
        Instantiate(TreePrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(center, size);
    }
}
