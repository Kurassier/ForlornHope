using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GroupOption : Option{
    public Group template;

    public void Initialize(Group group)
    {
        manager = GameObject.Find("SoldierManager");
        template = group;
        transform.Find("CardName").GetComponent<Text>().text = template.soldier.name + "(" + template.amount + ")";
    }
    public void ChangeAmount(int amount)
    {
        template.amount = amount;
        transform.Find("CardName").GetComponent<Text>().text = template.soldier.name + "(" + template.amount + ")";
    }
}
