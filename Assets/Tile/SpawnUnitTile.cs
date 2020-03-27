using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnUnitTile : Tile
{
    public int Units;
    public Sprite[] Sprites = new Sprite[3];
    public int[] UnitMinimum = new int[3];
    public int UnitGenerationMinimum;
    public int UnitGenerationMaximum;

    //public GameObject SpawningObject;
    public GameObject[] SpawningObject = new GameObject[1];
    public int[] SpawnUnitMinimum = new int[1];
    public int UnitSpawnMinimum;
    public int UnitSpawnMaximum;

    /*void SpawnObject()
    {
        Instantiate(SpawningObject);
    }*/

    void Start()
    {

    }

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        for (int i = Sprites.Length - 1; i >= 0; i--)
        {
            if (Units >= UnitMinimum[i])
            {
                tileData.sprite = Sprites[i];
                return;
            }

            /*if (Units >= SpawnUnitMinimum[i])
            {
                Instantiate(SpawningObject[i]);
                //SpawnObject();
                Debug.Log("Player Spawned");
            }*/
        }

        /*for (int i = Sprites.Length - 1; i >= 0; i--)
        {
            if (Units >= SpawnUnitMinimum[i])
            {
                Instantiate(SpawningObject[i]);
                //SpawnObject();
                Debug.Log("Player Spawned");
                return;
            }
        }*/
    }
    /*
    public bool StartUp(Vector3Int position, Tilemaps.ITilemap tilemap, GameObject go); 
    {
        for (int i = Sprites.Length - 1; i >= 0; i--)
        {
            if (Units >= SpawnUnitMinimum[i]])
            {
                Instantiate(SpawningObject[i]);
            }
        }
    }*/


#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/Tiles/Spawn Unit Tile")]
    public static void CreateSpawnUnitTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Spawn Unit Tile", "New Spawn Unit Tile", "Asset", "Save Spawn Unit Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<SpawnUnitTile>(), path);
    }
#endif
}
