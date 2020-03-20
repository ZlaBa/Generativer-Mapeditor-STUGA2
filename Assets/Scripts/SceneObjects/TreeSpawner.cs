using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using Assets;

public class TreeSpawner : MonoBehaviour
{

    GameData gameData;

    public Tilemap ObjectMap;

    public GameObject TreePrefab;

    public Vector3 GizmoPlace;
    public Vector3 GizmoSize;

    public Quaternion min;

    // Start is called before the first frame update
    void Start()
    {
         gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();
    }

    public void SpawnTree()
    {//(Random.Range(-size.x / 2, size.x / 2), Random.Range(-size.y / 2, size.y / 2), Random.Range(-size.z / 2, size.z / 2)
        Vector3 pos = GizmoPlace;
        Instantiate(TreePrefab, pos, Quaternion.identity);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(GizmoPlace, GizmoSize);
    }

    // Update is called once per frame
    void Update()
    {
        for (int x = 0; x < gameData.GetWidth(); x++)
        {
            for (int y = 0; y < gameData.GetHeight(); y++)
            {
                var feld = gameData.GetFeld(x, y);
                if (feld.Terrain == FeldTerrain.Wald)
                {
                    if (feld.Einheiten >= 0 && feld.Einheiten < 100)
                    {
                        SpawnTree();
                        return;
                    }
                }
            }
        }
    }
}
