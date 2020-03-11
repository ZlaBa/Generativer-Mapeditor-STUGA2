using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    public Enemy enemyCharacter;

    void Awake()
    {
        instance = this;
        enemyCharacter = GameObject.FindWithTag("BadGuys").GetComponent<Enemy>();
    }

    public void Attack()
    {
        enemyCharacter.TakeDamage();
        Debug.Log("Attacking!");
    }
}
