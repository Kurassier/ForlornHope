using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopOption : Option{
    public Troop template;

    public void Initialize(Troop troop)
    {
       manager = GameObject.Find("TroopManager");
       template = troop;
       transform.Find("CardName").gameObject.GetComponent<Text>().text = template.name;
    }
    public void ChangeName(string name)
    {
       template.name = name;
       transform.Find("CardName").GetComponent<Text>().text = name;
    }
    public string GetName()
    {
       return template.name;
    }
}
