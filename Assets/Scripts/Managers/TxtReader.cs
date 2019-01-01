using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class TxtReader{

    //test
    private static Text log1;

    static TxtReader()
    {
        //test
        log1 = GameObject.Find("log1").GetComponent<Text>();
    }
    public static List<string> TxtCuter(TextAsset resource)
    {
        string data = resource.text;
        List<string> datas = new List<string>();
        foreach(var var in data.Split('\n'))
        {
            if (!var.Contains("//") && var.Length > 1)   datas.Add(var);
        }
        //test
        //foreach (var var in datas)
        //{
        //    log1.text += var;
        //    log1.text += "\n";
        //}
        return datas;
    }
}
