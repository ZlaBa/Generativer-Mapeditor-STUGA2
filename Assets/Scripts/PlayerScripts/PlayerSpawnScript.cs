using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerSpawnScript : MonoBehaviour
{
    Rigidbody2D rb2D;

    public Grid grid;
    public Tilemap IsoGroundMap;
    //public Transform spawnPos;
    public GameObject Player;
    private GameData _gameData;

    private void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            Debug.Log("Gstartet");
            _gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();
            Debug.Log(_gameData.GetWidth());

            IsoGroundMap.CompressBounds();

            for (int x = 0; x < _gameData.GetWidth(); x++)
            {

                for (int y = 0; y < _gameData.GetHeight(); y++)
                {
                    var feld = _gameData.GetFeld(x, y);
                    Debug.Log(_gameData.GetFeld(x, y));
                    if (feld.Terrain == FeldTerrain.Wiese)
                    {
                        Player.layer = 5;
                        /*
                        GridLayout gridLayout = transform.parent.GetComponentInParent<GridLayout>();
                        Vector3Int cellPosition = gridLayout.WorldToCell(transform.position);
                        transform.position = gridLayout.CellToWorld(cellPosition);
                        */
                        //int feldKoordinate = int.Parse(feld.ToString());
                        //Instantiate(Player, new Vector3(IsoGroundMap.origin.x, IsoGroundMap.origin.y), spawnPos.rotation);
                        Instantiate(Player, IsoGroundMap.GetCellCenterWorld(new Vector3Int(x - 7, y - 7, 0)), Quaternion.identity);
                        //Instantiate(Player, new Vector3(IsoGroundMap.origin.x + IsoGroundMap.cellSize.x * x, IsoGroundMap.origin.y + IsoGroundMap.cellSize.y * y), spawnPos.rotation);
                        //Instantiate(Player, new Vector3Int(cellPosition.x, cellPosition.y, z: 0), spawnPos.rotation);
                        //Instantiate(Player, new Vector3Int((IsoGroundMap.origin.x, IsoGroundMap.origin.y), (IsoGroundMap.origin.x, IsoGroundMap.origin.y));
                        //Debug.Log("X: " + (IsoGroundMap.origin.x) + " Y: " + (IsoGroundMap.origin.y));
                        //Debug.Log("Player was spawned at" + (IsoGroundMap.origin.x + IsoGroundMap.cellSize.x * x, IsoGroundMap.origin.y + IsoGroundMap.cellSize.y * y) + "!");
                        Debug.Log("Player was spawned at X: " + (IsoGroundMap.GetCellCenterWorld(new Vector3Int(x, y, 0)).x) + " Y: " + (IsoGroundMap.GetCellCenterWorld(new Vector3Int(x, y, 0)).y) + "Q: " + (Quaternion.identity) + "!");
                        //Debug.Log("Player was spawned at cellPosition.x: " + (cellPosition.x) + " cellPosition.y: " + (cellPosition.y) + " spawnPos.rotation: " + (spawnPos.rotation) + "!");
                        //Debug.Log("Mission Complete!");
                        return;
                    }
                }
            }
        }
        /*
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

