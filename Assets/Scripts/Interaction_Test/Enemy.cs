using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int Luft;
    public int MaxLuft = 360;

    public void Start()
    {
        Luft = MaxLuft;
    }

    public void TakeDamage()
    {
        MaxLuft -= 2;
    }


}
