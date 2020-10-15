using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerhp : MonoBehaviour
{
    private int Hp;

    public playerhp(int _HP)
    {
        Hp = _HP;
    }

    public void decreaseHp(int damage)
    {
        Hp -= damage;
    }

    public int getHp()
    {
        return Hp;
    }
}
