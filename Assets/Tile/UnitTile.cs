using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitTile : Tile
{
    public int Units;
    public Sprite[] Sprites = new Sprite[3];
    public int[] UnitMinimum = new int[3];

    public int UnitGenerationMinimum;
    public int UnitGenerationMaximum;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void GetTileData(Vector3Int location, ITilemap tilemap, ref TileData tileData)
    {
        for( int i=Sprites.Length - 1; i >= 0; i--)
        {
            if( Units >= UnitMinimum[i])
            {
                tileData.sprite = Sprites[i];
                return;
            }
        }
    }

#if UNITY_EDITOR
    // The following is a helper that adds a menu item to create a RoadTile Asset
    [MenuItem("Assets/Create/Tiles/Unit Tile")]
    public static void CreateUnitTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Unit Tile", "New Unit Tile", "Asset", "Save Unit Tile", "Assets");
        if (path == "")
            return;
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<UnitTile>(), path);
    }
#endif
}
