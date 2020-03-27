using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject currentObject = null;
    public InteractionObject currentObjectScript = null;
    public PlayerInventoryScript inventory;

    private void Update()
    {
        if(Input.GetButtonDown("Interact") && currentObject)
        {
            //Ist dies ein Inventory Objekt?
            if (currentObjectScript.inventory)
            {
                inventory.AddItem(currentObject);
            }
        }

        //Benutze die gesammelten Bruchstücke
        if (Input.GetButtonDown("Use") && currentObject)
        {
            Debug.Log("Q was pressed!");
            //Inventar prüfen (ShuttlePiece)
            GameObject ShuttlePiece = inventory.FindItemByType("Nektar");
            if(ShuttlePiece != null)
            {
                //Bruchstücke verwenden - Shuttle Reparieren
                //Von Inventar entfernen
                if (currentObjectScript.inventory)
                {
                    inventory.RemoveItem(inventory.FindItemByType("Nektar"));
                }
            }
            if(ShuttlePiece = null)
            {
                Debug.Log("Can't find a ShuttlePiece!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            Debug.Log("Interaktives Objekt: " + collision.name);
            currentObject = collision.gameObject;
            currentObjectScript = currentObject.GetComponent<InteractionObject>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("InteractableObject"))
        {
            if(collision.gameObject == currentObject)
            {
                currentObject = null;
            }
            Debug.Log("Contact lost");
        }
    }
}
