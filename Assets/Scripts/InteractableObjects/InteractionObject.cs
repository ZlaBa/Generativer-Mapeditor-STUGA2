using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    public bool inventory; //Kann im Unity mit Checkbox aktiviert werden
    public string itemType; //Definiert den Typ des Items

    public void DoInteraction()
    {
        //Aufnehmen ins Inventar (Unsichtbar machen)
        gameObject.SetActive(false);
    }
}
