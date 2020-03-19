using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
using Assets;

public class TileAutomata : MonoBehaviour
{
    [Range(0, 100)]
    public int iniWald;

    [Range(0, 8)]
    public int gebWald;

    [Range(0, 8)]
    public int nemWald;

    [Range(0, 100)]
    public int iniWasser;

    [Range(0, 8)]
    public int gebWasser;

    [Range(0, 8)]
    public int nemWasser;

    [Range(0, 100)]
    public int iniGestein;

    [Range(0, 8)]
    public int gebGestein;

    [Range(0, 8)]
    public int nemGestein;

    [Range(0, 100)]
    public int iniSand;

    [Range(0, 8)]
    public int gebSand;

    [Range(0, 8)]
    public int nemSand;

    [Range(0, 100)]
    public int iniWiese;

    [Range(0, 8)]
    public int gebWiese;

    [Range(0, 8)]
    public int nemWiese;

    //Anzahl Runden die durchgerechnet werden
    [Range(1, 10)]
    public int rechenRunden;
    private int count = 0; //für gespeicherte Files

    public Vector3Int kartenMasse; //Kartengrösse

    public Tilemap ObjectMap;
    public Tilemap UnwalkableMap;
    public Tilemap topMap;
    public Tilemap botMap;
    public Tile waldTile;
    public Tile treeTile;
    public Tile wasserTile;
    public Tile wieseTile;
    public Tile sandTile;
    public Tile gesteinTile;

    private GameData gameData;

    int width;
    int height;
    
    public int GetNem(FeldTerrain terrain)
    {
        switch (terrain)
        {
            case FeldTerrain.Gestein:
                return nemGestein;
            case FeldTerrain.Sand:
                return nemSand;
            case FeldTerrain.Wald:
                return nemWald;
            case FeldTerrain.Wasser:
                return nemWasser;
            case FeldTerrain.Wiese:
                return nemWiese;
        }

        throw new System.InvalidOperationException();
    }

    public int GetGeb(FeldTerrain terrain)
    {
        switch (terrain)
        {
            case FeldTerrain.Gestein:
                return gebGestein;
            case FeldTerrain.Sand:
                return gebSand;
            case FeldTerrain.Wald:
                return gebWald;
            case FeldTerrain.Wasser:
                return gebWasser;
            case FeldTerrain.Wiese:
                return gebWiese;
        }

        throw new System.InvalidOperationException();
    }

    //Hauptfunktion
    public void doSim(int rechenRunden)
    {
        clearMap(false);
        width = kartenMasse.x;
        height = kartenMasse.y;

        gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();

        if (!gameData.IsGenerated())
        {
            gameData.Init(width, height);
            initPos();
        }

        for (int i = 0; i < rechenRunden; i++)
        {
            var newFelder = genTilePos(gameData.GetFelder());
            gameData.ReplaceFelder(newFelder);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var feld = gameData.GetFeld(x, y);

                if (feld.Terrain == FeldTerrain.Wald)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waldTile);
                    //ObjectMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), treeTile);
                }
                if (feld.Terrain == FeldTerrain.Sand)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), sandTile);
                }
                if (feld.Terrain == FeldTerrain.Gestein)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), gesteinTile);
                }
                if (feld.Terrain == FeldTerrain.Wasser)
                {
                    UnwalkableMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), wasserTile);
                }
                if (feld.Terrain == FeldTerrain.Wiese)
                {
                    botMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), wieseTile);
                }
            }
        }
    }

    public Feld[,] genTilePos(Feld[,] oldMap)
    {
        Feld[,] newMap = new Feld[width, height];
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Dictionary<FeldTerrain, int> neighbors = new Dictionary<FeldTerrain, int>();
                neighbors.Add(FeldTerrain.Gestein, 0);
                neighbors.Add(FeldTerrain.Sand, 0);
                neighbors.Add(FeldTerrain.Wald, 0);
                neighbors.Add(FeldTerrain.Wasser, 0);
                neighbors.Add(FeldTerrain.Wiese, 0);

                foreach (var b in myB.allPositionsWithin)
                {
                    if (b.x == 0 && b.y == 0)
                        continue;
                    if (x + b.x >= 0 && x + b.x < width && y + b.y >= 0 && y + b.y < height)
                    {
                        var terrainAtField = oldMap[x + b.x, y + b.y].Terrain;
                        neighbors[terrainAtField] = neighbors[terrainAtField] + 1;
                    }
                }

                var oldTerrain = oldMap[x, y].Terrain; //Wiese
                var nemTerrain = GetNem(oldTerrain); //4
                if (neighbors[oldTerrain] < nemTerrain) // 5 
                {
                    newMap[x, y] = new Feld { Terrain = getNewTerrain(neighbors) };
                }
                else
                {
                    newMap[x, y] = new Feld { Terrain = oldTerrain };
                }
            }
        }
        return newMap;
    }

    private FeldTerrain getNewTerrain(Dictionary<FeldTerrain, int> neighbors)
    {
        var terrain = FeldTerrain.Wiese;
        var terrainCount = 0;

        foreach( var key in neighbors.Keys )
        {
            var neighborCount = neighbors[key];
            if( neighborCount > terrainCount)
            {
                terrain = key;
                terrainCount = neighborCount;
            }
        }

        return terrain;
    }

    public void initPos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var randomValue = Random.Range(1, (iniWald + iniWasser + iniGestein + iniSand + iniWiese));

                if (randomValue <= (iniWald))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Wald };
                    gameData.SetFeld(feld, x, y);
                }
                else if( randomValue <= (iniWald + iniWasser))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Wasser };
                    gameData.SetFeld(feld, x, y);
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Gestein };
                    gameData.SetFeld(feld, x, y);
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein + iniSand))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Sand };
                    gameData.SetFeld(feld, x, y);
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein + iniSand + iniWiese))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Wiese };
                    gameData.SetFeld(feld, x, y);
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {   // Befehl zum Start
        if (Input.GetMouseButtonDown(0))
        {
            doSim(rechenRunden);
        }
        // Befehl zur Löschung
        if (Input.GetMouseButtonDown(1))
        {
            clearMap(true);
        }
        // Befehl zum Speichern
        if (Input.GetMouseButton(2))
        {
            SaveAssetMap();
        }
    }
    // Karte als Asset speichern
    public void SaveAssetMap()
    {
        string saveName = "tmapXY_" + count;
        var mf = GameObject.Find("Grid");

        if (mf)
        {
            var savePath = "assets/" + saveName + ".prefab";
            if (PrefabUtility.CreatePrefab(savePath, mf))
            {
                EditorUtility.DisplayDialog("Tilemap saved", "Your Tilemap was saved under " + savePath, "Continue");
            }
            else
            {
                EditorUtility.DisplayDialog("Tilemap NOT saved", "An Error occured while trying to save the Tilemap under " + savePath, "Continue");
            }
        }
    }

    public void clearMap(bool complete)
    {
        UnwalkableMap.ClearAllTiles();
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();

        if (complete)
        {
            gameData.ResetGame();
        }
    }
}

