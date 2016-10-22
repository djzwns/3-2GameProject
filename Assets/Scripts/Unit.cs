using UnityEngine;
using System.Collections;

public class Unit : ScriptableObject
{
    protected int iMaxLife = 3;
    protected int iCurrentLife = 3;

    public int MaxLife
    {
        get
        {
            return iMaxLife;
        }
    }

    public int CurrentLife
    {
        get
        {
            if(iCurrentLife > 0)
                return iCurrentLife;
            return 0;
        }
    }

    public void TakeDamage(int _damage)
    {
        iCurrentLife -= Mathf.Clamp(_damage, 0, iMaxLife);
    }
}