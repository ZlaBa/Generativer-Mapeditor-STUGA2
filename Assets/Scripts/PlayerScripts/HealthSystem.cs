using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public int Air;
    public int maxAir;
    public int damageAmount = 10;
    public int healAmount = 5;

    public HealthSystem(int maxAir)
    {
        this.maxAir = maxAir;
        Air = maxAir;
    }

    public int GetAir()
    {
        return Air;
    }

    //public void Damage(int damageAmount)
    public void Damage()
    {
        Air -= damageAmount;
        if (Air < 0) Air = 0;
    }

    //public void Heal(int healAmount)
    public void Heal()
    {
        Air += healAmount;
        if (Air > maxAir) Air = maxAir;
    }
}

