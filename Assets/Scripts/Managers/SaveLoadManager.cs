using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager{
    public string save;

    public SaveLoadManager(string name)
    {
        save = name;
    }
    public string LaodForce()
    {
        string data = PlayerPrefs.GetString(save + "Force");
        return data;
    }
    public void SaveForce(string data)
    {
        PlayerPrefs.SetString(save + "Force", data);
    }
}
