using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;

public class TileAutomata : MonoBehaviour
{
    public class Feld
    {
        public Terrain Terrain { get; set; }
        public int Einheiten { get; set; }
        public double Wachstumsfaktor { get; set; }
    }

    public enum Terrain
    {
        Wiese,
        Wasser,
        Wald,
        Gestein,
        Sand,
    }
    
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

    private Feld[,] terrainMap; //Store Terrain Tiles, 0 für tot, 1 für lebendig
    public Vector3Int kartenMasse; //Kartengrösse

    public Tilemap topMap;
    public Tilemap botMap;
    public Tile waldTile;
    public Tile wasserTile;
    public Tile wieseTile;
    public Tile sandTile;
    public Tile gesteinTile;

    int width;
    int height;

    public int GetNem(Terrain terrain)
    {
        switch (terrain)
        {
            case Terrain.Gestein:
                return nemGestein;
            case Terrain.Sand:
                return nemSand;
            case Terrain.Wald:
                return nemWald;
            case Terrain.Wasser:
                return nemWasser;
            case Terrain.Wiese:
                return nemWiese;
        }

        throw new System.InvalidOperationException();
    }

    public int GetGeb(Terrain terrain)
    {
        switch (terrain)
        {
            case Terrain.Gestein:
                return gebGestein;
            case Terrain.Sand:
                return gebSand;
            case Terrain.Wald:
                return gebWald;
            case Terrain.Wasser:
                return gebWasser;
            case Terrain.Wiese:
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

        if (terrainMap == null)
        {
            terrainMap = new Feld[width, height];
            initPos();
        }

        for (int i = 0; i < rechenRunden; i++)
        {
            terrainMap = genTilePos(terrainMap);
        }

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (terrainMap[x, y].Terrain == Terrain.Wald)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), waldTile);
                }
                if (terrainMap[x, y].Terrain == Terrain.Sand)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), sandTile);
                }
                if (terrainMap[x, y].Terrain == Terrain.Gestein)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), gesteinTile);
                }
                if (terrainMap[x, y].Terrain == Terrain.Wasser)
                {
                    topMap.SetTile(new Vector3Int(-x + width / 2, -y + height / 2, 0), wasserTile);
                }
                if (terrainMap[x, y].Terrain == Terrain.Wiese)
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
                Dictionary<Terrain, int> neighbors = new Dictionary<Terrain, int>();
                neighbors.Add(Terrain.Gestein, 0);
                neighbors.Add(Terrain.Sand, 0);
                neighbors.Add(Terrain.Wald, 0);
                neighbors.Add(Terrain.Wasser, 0);
                neighbors.Add(Terrain.Wiese, 0);

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

    private Terrain getNewTerrain(Dictionary<Terrain, int> neighbors)
    {
        var terrain = Terrain.Wiese;
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
                    terrainMap[x, y] = new Feld { Terrain = Terrain.Wald };
                }
                else if( randomValue <= (iniWald + iniWasser))
                {
                    terrainMap[x, y] = new Feld { Terrain = Terrain.Wasser };
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein))
                {
                    terrainMap[x, y] = new Feld { Terrain = Terrain.Gestein };
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein + iniSand))
                {
                    terrainMap[x, y] = new Feld { Terrain = Terrain.Sand };
                }
                else if (randomValue <= (iniWald + iniWasser + iniGestein + iniSand + iniWiese))
                {
                    terrainMap[x, y] = new Feld { Terrain = Terrain.Wiese };
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
        topMap.ClearAllTiles();
        botMap.ClearAllTiles();

        if (complete)
        {
            terrainMap = null;
        }
    }
}

