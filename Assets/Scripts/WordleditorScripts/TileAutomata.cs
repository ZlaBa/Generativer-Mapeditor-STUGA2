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
    //private int count = 0; //für gespeicherte Files

    public Vector3Int kartenMasse; //Kartengrösse

    public Tilemap PlayerMap;
    public Tilemap ObjectMap;
    public Tilemap UnwalkableMap;
    public Tilemap GroundMap;
    public Tilemap RuleTileMap;
    //public Tilemap BackgroundMap;

    public UnitTile waldTrees;
    public RuleTile RuledWaldTile;
    public UnitTile wasserTiefe;
    public RuleTile RuledWasserTile;
    public SpawnUnitTile wieseHight;
    public RuleTile RuledWieseTile;
    public RuleTile RuledSandTile;
    public RuleTile RuledGesteinTile;

    private GameData _gameData;

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
        ClearMap(false);
        width = kartenMasse.x;
        height = kartenMasse.y;

        _gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();

        if (!_gameData.IsGenerated())
        {
            _gameData.Init(width, height);
            initPos();
        }

        for (int i = 0; i < rechenRunden; i++)
        {
            var newFelder = genTilePos(_gameData.GetFelder());
            _gameData.ReplaceFelder(newFelder);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var feld = _gameData.GetFeld(x, y);
                var neighborCount = CountNeighbors(_gameData.GetFelder(), x, y);

                if (feld.Terrain == FeldTerrain.Wald)
                {
                    // Konfiguration des hinzuzufügenden Tiles
                    waldTrees.Units = CalculateUnits(neighborCount[FeldTerrain.Wald], waldTrees.UnitGenerationMinimum, waldTrees.UnitGenerationMaximum); //waldTrees.Sprites.Length
                    ObjectMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waldTrees);

                    UnwalkableMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), RuledWaldTile);
                }
                if (feld.Terrain == FeldTerrain.Sand)
                {
                    RuleTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), RuledSandTile);
                }
                if (feld.Terrain == FeldTerrain.Gestein)
                {
                    RuleTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), RuledGesteinTile);
                }
                if (feld.Terrain == FeldTerrain.Wasser)
                {
                    wasserTiefe.Units = CalculateUnits(neighborCount[FeldTerrain.Wasser], waldTrees.UnitGenerationMinimum, waldTrees.UnitGenerationMaximum);
                    UnwalkableMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), wasserTiefe);

                    UnwalkableMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), RuledWasserTile);
                }
                if (feld.Terrain == FeldTerrain.Wiese)
                {
                    wieseHight.Units = CalculateUnits(neighborCount[FeldTerrain.Wiese], wieseHight.UnitGenerationMinimum, wieseHight.UnitGenerationMaximum);
                    GroundMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), wieseHight);

                    RuleTileMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), RuledWieseTile);
                }
            }
        }
    }
    
    /*private int CalculateUnits(int neighborCount, int min, int max, int anzahlSprites)
    {
        var step = 8 / anzahlSprites;
        var currentStep = step;
        var stepCount = 0;
        var unitStep = (max - min) / anzahlSprites;

        while(currentStep <= 8)
        {
            if( neighborCount <= currentStep)
            {
                var value = Random.Range(stepCount * unitStep, (stepCount + 1) * unitStep);
                Debug.Log("neighborCount: " + neighborCount + ", value: " + value);
                return value;
            }

            stepCount++;
            currentStep += step;
        }

        throw new System.InvalidOperationException("Nicht gut.");
    }*/


    private int CalculateUnits(int neighborCount, int min, int max)
    {
        switch (neighborCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return Random.Range(min, max / 4);//250
            case 4:
            case 5:
                return Random.Range(max / 4, max / 4 * 2);//500
            case 6:
            case 7:
                return Random.Range(max / 4 * 2, max / 4 * 3);//750
            case 8:
                return Random.Range(max / 4 * 3, max);//1000
        }

        throw new System.InvalidOperationException("Nicht gut.");
    }

    /*private int CalculateUnits(int neighborCount, int min, int max)
    {
        switch (neighborCount)
        {
            case 0:
            case 1:
            case 2:
            case 3:
                return Random.Range(min, max / 3);
            case 4:
            case 5:
            case 6:
                return Random.Range(max / 3, max / 3 * 2);
            case 7:
            case 8:
                return Random.Range(max / 3 * 2, max);
        }

        throw new System.InvalidOperationException("Nicht gut.");
    }*/

    public Feld[,] genTilePos(Feld[,] oldMap)
    {
        Feld[,] newMap = new Feld[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Dictionary<FeldTerrain, int> neighbors = CountNeighbors(oldMap, x, y);

                var oldTerrain = oldMap[x, y].Terrain;
                var nemTerrain = GetNem(oldTerrain);
                if (neighbors[oldTerrain] < nemTerrain)
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

    private Dictionary<FeldTerrain, int> CountNeighbors(Feld[,] oldMap, int x, int y)
    {
        BoundsInt myB = new BoundsInt(-1, -1, 0, 3, 3, 1);

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

        return neighbors;
    }

    private FeldTerrain getNewTerrain(Dictionary<FeldTerrain, int> neighbors)
    {
        var terrain = FeldTerrain.Wiese;
        var terrainCount = 0;

        foreach (var key in neighbors.Keys)
        {
            var neighborCount = neighbors[key];
            if (neighborCount > terrainCount)
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
                    _gameData.SetFeld(feld, x, y);
                }
                else if (randomValue <= (iniWald + iniWasser))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Wasser };
                    _gameData.SetFeld(feld, x, y);
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Gestein };
                    _gameData.SetFeld(feld, x, y);
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein + iniSand))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Sand };
                    _gameData.SetFeld(feld, x, y);
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein + iniSand + iniWiese))
                {
                    var feld = new Feld { Terrain = FeldTerrain.Wiese };
                    _gameData.SetFeld(feld, x, y);
                }
            }
        }
    }
    private void Awake()
    {
        doSim(rechenRunden);
    }
    // Update is called once per frame
    void Update()
    {   // Befehl zum Start
        if (Input.GetKeyUp(KeyCode.Backspace))
        {
            doSim(rechenRunden);
        }
        // Befehl zur Löschung
        if (Input.GetKeyUp(KeyCode.Delete))
        {
            ClearMap(true);
        }
    }
    // Befehl zum Speichern
    /*if (Input.GetMouseButton(2))
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
}*/

    public void ClearMap(bool complete)
    {
        _gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();
        ObjectMap.ClearAllTiles();
        UnwalkableMap.ClearAllTiles();
        GroundMap.ClearAllTiles();
        RuleTileMap.ClearAllTiles();

        if (complete)
        {
            _gameData.ResetGame();
        }
    }
}

