using Assets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Baum : MonoBehaviour
{
    GameData gameData;

    // Start is called before the first frame update
    void Start()
    {
         gameData = GameObject.FindWithTag("GameData").GetComponent<GameData>();
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
                    if (feld.Einheiten > 20 && feld.Einheiten < 50)
                    {
                        //Baum anzeigen
                        return;
                    }
                }
            }
        }
    }
}
