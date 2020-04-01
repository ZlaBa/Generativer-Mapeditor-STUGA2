using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryScript : MonoBehaviour
{
    public GameObject[] inventory = new GameObject[7];

    public void AddItem(GameObject item)
    {
        bool itemAdded = false;
        //Finde einen Slot
        for (int i = 0; i < inventory.Length; i++)
        {
            if(inventory [i] == null)
            {
                inventory[i] = item;
                Debug.Log(item.name + " was added");
                itemAdded = true;
                //Interaktion mit dem Objekt
                item.SendMessage("DoInteraction");
                break;
            }
        }
        //Alle Slots belegt
        if (!itemAdded)
        {
            Debug.Log("Inventory Full!");
        }
    }

    public bool InventoryFull()
    {
        bool _inventoryFull = false;

        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                _inventoryFull = false;
                return _inventoryFull;
            }
            else
            {
                _inventoryFull = true;
            }
        }
        return _inventoryFull;
    }


    // FUNKTIONIERT NICHT!!!
    public bool FindItem(GameObject item)
    {
        for(int i = 0; i < inventory.Length; i++)
        {
            if (inventory [i] == item)
            {
                return true;
            }
        }
        return false;
    }


    public GameObject FindItemByType(string itemType)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null)
            {
                if(inventory[i].GetComponent<InteractionObject>().itemType == itemType)
                {
                    //Item gefunden
                    return inventory[i];
                }
            }
        }
        //nicht gefunden
        return null;
    }

    public void RemoveItem(GameObject item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
                if (inventory[i] == item)
                {
                    //Item remove
                    inventory[i] = null;
                    Debug.Log(item.name + " was removed from inventory");
                    break;
                }
            
        }
    }
}
