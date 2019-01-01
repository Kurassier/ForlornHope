using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Group{
    public int amount;
    public Soldier soldier;

    public Group(Soldier soldier ,int amount)
    {
        this.soldier = soldier;
        this.amount = amount;
    }
}