using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuttleScript : MonoBehaviour
{
    public bool inventory; //Kann im Unity mit Checkbox aktiviert werden
    public string itemType; //Definiert den Typ des Items
    public GameObject YouWinScreen;

    public void DoInteraction()
    {
        //Aufnehmen ins Inventar (Unsichtbar machen)
        YouWinScreen.SetActive(true);
    }
}
