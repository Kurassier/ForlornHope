using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Soldier{
    public string info;
    public string name;
    public string type;
    private string path;

    //基础数据
    private float health;
    private float armorProtection;
    private float armorCover;
    private float morale;
    private float extraActivity;

    private bool hasRange { get { return hasRange;} set { } }

    //攻击数据
    private int[] Attack = new int[2];
    private int[] AttackAP = new int[2];

    private float[] damage = new float[2];
    private float[] frequency = new float[2];
    private float[] armorPierce = new float[2];
    private float[] hitProbability = new float[2];


    public static List<Soldier> totalList = new List<Soldier>();

    public Soldier()
    {

    }
    public Soldier(string data)
    {
        info = data;
        string[] datas = data.Split('\n');
        SetSoldierAbility(datas[0]);
        SetMeleeAbility(datas[1]);
        if (datas[2] != "null")
        {
            SetRangeAbility(datas[2]);
            hasRange = true;
        }
        else hasRange = false;
    }
    public Soldier(string soldierData, string MeleeData)
    {
        info = soldierData + "\n" + MeleeData;
        SetSoldierAbility(soldierData);
        SetMeleeAbility(MeleeData);
        hasRange = false;
    }
    public Soldier(string soldierData, string MeleeData, string RangeData)
    {
        info = soldierData + "\n" + MeleeData + "\n" + RangeData;
        SetSoldierAbility(soldierData);
        SetMeleeAbility(MeleeData);
        SetRangeAbility(RangeData);
        hasRange = true;
    }

    string GetType(string type)
    {
        switch (type)
        {
            case "asltFt":
                return "冲击步兵";
            case "lnFt":
                return "战线步兵";
            case "rgFt":
                return "远程步兵";
            case "mlHrs":
                return "近战骑兵";
            case "rgHrs":
                return "远程骑兵";
            default:
                return null;
        }
    }

    void SetSoldierAbility(string data)
    {
        string[] datas = data.Split(',');
        int i = 0;
        name = datas[i];        i++;
        type = GetType(datas[i]);        i++;
        path = datas[i];        i++;
        health = float.Parse(datas[i]);        i++;
        armorProtection = float.Parse(datas[i]);        i++;
        armorCover = float.Parse(datas[i]); i++;
        morale = float.Parse(datas[i]);        i++;
        extraActivity = float.Parse(datas[i]);
    }
    void SetMeleeAbility(string data)
    {
        string[] datas = data.Split(',');
        int i = 0;
        damage[0] = float.Parse(datas[i]);        i++;
        armorPierce[0] = float.Parse(datas[i]);        i++;
        frequency[0] = float.Parse(datas[i]);        i++;
        hitProbability[0] = float.Parse(datas[i]);
        CalculateAttack(0);
    }
    void SetRangeAbility(string data)
    {
        hasRange = true;
        string[] datas = data.Split(',');
        int i = 0;
        damage[1] = float.Parse(datas[i]);        i++;
        armorPierce[1] = float.Parse(datas[i]);        i++;
        frequency[1] = float.Parse(datas[i]);        i++;
        hitProbability[1] = float.Parse(datas[i]);
        CalculateAttack(1);
    }
    void CalculateAttack(int i){
        Attack[i] = (int)(damage[i] * frequency[i] * hitProbability[i]);
        Attack[i] = (int)(damage[i] * frequency[i] * hitProbability[i] * (1 - armorPierce[i] / 100));
    }
    float CalculateDamage(float damage,float armorPierce){
        int random = Random.Range(1, 100);
        if (random <= armorCover) return damage - (armorProtection * (1 - armorPierce / 100));
        else return damage;
    }

    public List<string> GetTag()
    {
        List<string> tags = new List<string>();

        //士气
        if (morale<=10) tags.Add("一触即溃");
        else if(morale <= 20) tags.Add("尚可一战");
        else if (morale <= 30) tags.Add("令行禁止");
        else tags.Add("不败铁军");

        //破甲
        if (armorPierce[0] >= 70 || armorPierce[1] >= 70 || damage[0] > 200 || damage[1] > 200) tags.Add("重甲克星");
        else if (armorPierce[0] >= 40 || armorPierce[1] >= 40 || damage[0] > 120 || damage[1] > 120) tags.Add("反制重甲");

        //

        return tags;
    }

    public static void SoldierListCreator(List<string> datas)
    {
        string[] temp = new string[3];
        while(datas.Count != 0){
            temp[0] = datas[0]; datas.RemoveAt(0);
            temp[1] = datas[0]; datas.RemoveAt(0);
            if(datas[0][0]>='0'&& datas[0][0] <= '9'){
                temp[2] = datas[0]; datas.RemoveAt(0);
                //拥有远程攻击能力
                totalList.Add(new Soldier(temp[0], temp[1], temp[2]));
            }
            else{
                //不拥有远程攻击能力
                totalList.Add(new Soldier(temp[0], temp[1]));
            }
        }
    }

    public static Soldier GetSoldier(string name){
        foreach(Soldier soldier in totalList)
            if (soldier.name == name) return soldier;
        return null;
    }
}
