using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Player playerCharacter;
    /* //Can be used without instance with this code
    private void Awake()
    {
        {
            playerCharacter = GetComponent<Player>();
        }
    }
    */
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            Player.instance.Attack();
        }
    }
}
