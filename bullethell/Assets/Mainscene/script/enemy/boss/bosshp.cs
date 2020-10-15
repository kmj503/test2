using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bosshp : MonoBehaviour
{
    private int Hp = 500;

    public bosshp(int _HP)
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
