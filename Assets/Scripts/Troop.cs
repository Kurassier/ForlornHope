using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Troop{
    public string name;
    public string type;
    public List<Group> groups;
    public Troop(string name){
        this.name = name;
        groups = new List<Group>();
    }
    public void Add(string soldierName){
        //如果已有该单位，直接退出
        foreach (Group group in groups)
            if (group.soldier.name == soldierName) return;
        Group newGroup = new Group(Soldier.GetSoldier(soldierName), 100);
        groups.Add(newGroup);
    }
    public void Add(string soldierName,int amuont)
    {
        //如果已有该单位，直接退出
        foreach (Group group in groups)
            if (group.soldier.name == soldierName) return;
        Group newGroup = new Group(Soldier.GetSoldier(soldierName), amuont);
        groups.Add(newGroup);
    }
    public void Add(Soldier soldier, int amuont)
    {
        //如果已有该单位，直接退出
        foreach (Group group in groups)
            if (group.soldier.name == soldier.name) return;
        Group newGroup = new Group(soldier, amuont);
        groups.Add(newGroup);
    }

    //存储相关操作，以字符串存储
    public override string ToString(){
        string info = "";
        info += "@" + name + "\n";
        foreach(Group group in groups)
        {
            info += group.soldier.name + "," + group.amount + "\n";
        }
        return info;
    }
    public static Troop ToTroop(string info)
    {
        string[] datas = info.Split('\n');
        Troop troop = new Troop(datas[0]);
        for (int i = 1; i < datas.Length; i++)
        {
            if (datas[i] == "") break;
            troop.Add(datas[i].Split(',')[0], int.Parse(datas[i].Split(',')[1]));
        }
        return troop;
    }
    public Troop Copy()
    {
        Troop troop = new Troop(this.name);
        foreach(Group group in this.groups)
        {
            troop.Add(group.soldier, group.amount);
        }
        return troop;
    }
}
