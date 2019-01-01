using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GeneralManager : MonoBehaviour {

    private GameObject cardManager;
    private GameObject troopManager;
    private GameObject soldierManager;
    private HorizontalBarWithEffectManager cardManagerScript;
    private VerticalBarManager troopManagerScript;
    private VerticalBarManager soldierManagerScript;

    public TextAsset soldierData;
    public string[] soldier;

    //test
    private static Text log1;

    // Use this for initialization
    void Start () {
        cardManager = GameObject.Find("CardManager");
        troopManager = GameObject.Find("TroopManager");
        soldierManager = GameObject.Find("SoldierManager");
        cardManagerScript = cardManager.GetComponent<HorizontalBarWithEffectManager>();
        troopManagerScript = troopManager.GetComponent<TroopBarManager>();
        soldierManagerScript = soldierManager.GetComponent<SoldierBarManager>();

        CardBarInitialize();

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    List<string> Filter(string[] data)
    {
        List<string> filteredData = new List<string>();



        return filteredData;
    }

    void CardBarInitialize()
    {

        //获取部队列表并且生成牌表
        Soldier.SoldierListCreator(TxtReader.TxtCuter(Resources.Load<TextAsset>("File/Soldier")));


    }

    void Skip()
    {
        troopManager.SendMessage("Save");
        SceneManager.LoadScene("BattleField", LoadSceneMode.Single);
    }
}
