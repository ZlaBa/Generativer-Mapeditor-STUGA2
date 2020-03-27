using Assets;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private Feld[,] _data;
    private bool _isGenerated;
    private int _width;
    private int _height;
    private object CMDebug;

    public void Init(int width, int height)
    {
        _data = new Feld[width, height];
        _width = width;
        _height = height;
        _isGenerated = true;
    }

    public bool IsGenerated()
    {
        return _isGenerated;
    }

    public void ResetGame()
    {
        _isGenerated = false;
    }

    public void SetFeld(Feld feld, int x, int y)
    {
        _data[x, y] = feld;
    }

    public Feld GetFeld(int x, int y)
    {
        return _data[x, y];
    }

    public void ReplaceFelder(Feld[,] felder)
    {
        _data = felder;
    }

    public Feld[,] GetFelder()
    {
        return _data;
    }

    public int GetHeight()
    {
        return _height;
    }

    public int GetWidth()
    {
        return _width;
    }
}
