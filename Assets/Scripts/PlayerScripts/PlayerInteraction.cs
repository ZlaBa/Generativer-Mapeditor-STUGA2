using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public GameObject _endGameObject = null;
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

        if (Input.GetButtonDown("Interact") && _endGameObject)
        {
            //Ist dies ein Inventory Objekt?
            if (currentObjectScript.inventory && inventory.InventoryFull() == true)
            {
                inventory.AddItem(_endGameObject);
                GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameStateManager>().YouWin();
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

        if (collision.CompareTag("EndGameObject"))
        {
            Debug.Log("End Game Objekt: " + collision.name);
            _endGameObject = collision.gameObject;
            currentObjectScript = _endGameObject.GetComponent<InteractionObject>();
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
            Debug.Log("Contact with currentObjet lost");
        }

        if (collision.CompareTag("EndGameObject"))
        {
            if (collision.gameObject == _endGameObject)
            {
                _endGameObject = null;
            }
            Debug.Log("Contact with _endGameObject lost");
        }
    }
}
