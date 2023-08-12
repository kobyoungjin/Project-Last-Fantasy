using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troll : Status
{

    private void Start()
    {
        level = 3;
        hp = 100;
        maxHp = 100;
        attack = 5;
        defense = 5;

        //Managers.UI.MakeWorldSpace<UI_HPBar>(transform);
    }
}