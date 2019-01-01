using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force{

    public List<Troop> troops;

    public Force(string data)
    {
        troops = new List<Troop>();
        if (data == "") return;
        foreach (string info in data.Split('@'))
        {
            if (info != "") troops.Add(Troop.ToTroop(info));
        }
    }

    //存储相关操作，以字符串存储
    public override string ToString()
    {
        string data = "";
        foreach(var troop in troops)
        {
            data +=troop.ToString();
        }
        return data;
    }

    public void ToForce(string data)
    {
        troops.RemoveAll(troop=>true);
        foreach(string info in data.Split('@'))
        {
            if (info != "") troops.Add(Troop.ToTroop(info));
        }
    }

}
