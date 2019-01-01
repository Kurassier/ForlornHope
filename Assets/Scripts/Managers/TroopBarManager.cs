using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TroopBarManager : VerticalBarManager{
     private InputField namer;

    private SaveLoadManager save;
    private Force force;
    private SoldierBarManager soldierBarManager;

    private Text log1;

    private void Start()
    {
         namer = GameObject.Find("Name").GetComponent<InputField>();
        soldierBarManager = GameObject.Find("SoldierManager").GetComponent<SoldierBarManager>();

        //test
        //只用第一个存档
        save = new SaveLoadManager("Save0");
        

        force = new Force(save.LaodForce());

        //test
        log1 = GameObject.Find("log1").GetComponent<Text>();
        log1.text = save.LaodForce();

        foreach (var troop in force.troops)
        {
            Add(troop);
        }
        //自动列表显示部队
        Refresh();
        //设置target的坐标
        targetPosition = transform.position;
    }

    //生成具体类型的卡牌
    void Add(Troop troop)
    {
        if (troop == null) return;
        GameObject tempOpt = GameObject.Instantiate(option, transform);
        ScriptReplace(tempOpt, troop);
        optionList.Add(tempOpt);
        Refresh();
    }
    void ScriptReplace(GameObject option, Troop troop)
    {
        Destroy(option.GetComponent<Option>());
        option.AddComponent<TroopOption>();
        option.GetComponent<TroopOption>().Initialize(troop);
        option.GetComponent<TroopOption>().SetManager(gameObject);
    }
    void ChangeName()
    {
        if (selected != null) selected.SendMessage("ChangeName", namer.text);
    }
    override public void Select(GameObject selected)
    {
          if (this.selected != selected)
        {
            if (this.selected != null) this.selected.GetComponent<Image>().color *= 2f;
            this.selected = selected;
            selected.GetComponent<Image>().color *= 0.5f;
            namer.text = selected.GetComponent<TroopOption>().GetName();
            //设置兵种栏的模板
            soldierBarManager.template = selected.GetComponent<TroopOption>().template;
            soldierBarManager.ReRenerate();
        }
        else
        {
            this.selected.GetComponent<Image>().color *= 2f;
            this.selected = null;
            namer.text = "";
            //设置兵种栏的模板
            soldierBarManager.template = null;
            soldierBarManager.ReRenerate();
        }
    }

    void New()
    {
         if(selected == null)
        {
            Troop troop = new Troop("新建兵团");
            Add(troop);
            force.troops.Add(troop);
        }
        else
        {
            Troop troop = selected.GetComponent<TroopOption>().template.Copy();
            Add(troop);
            force.troops.Add(troop);
        }
    }
    void Delete()
    {
        if (selected == null) return;
        force.troops.Remove(selected.GetComponent<TroopOption>().template);
        optionList.Remove(selected);
        Destroy(selected);
        Refresh();
    }
    void Save()
    {
         save.SaveForce(force.ToString());

        log1.text = save.LaodForce();
    }
    override public void Up()
    {
         if (selected == null || optionList.IndexOf(selected) == 0) return;
        int index = optionList.IndexOf(selected);
        optionList.Remove(selected);
        optionList.Insert(index - 1, selected);
        force.troops.Remove(selected.GetComponent<TroopOption>().template);
        force.troops.Insert(index - 1, selected.GetComponent<TroopOption>().template);
        Refresh();
    }
    override public void Down()
    {
          if (selected == null || optionList.IndexOf(selected) == optionList.Count - 1) return;
        int index = optionList.IndexOf(selected);
        optionList.Remove(selected);
        optionList.Insert(index + 1, selected);
        force.troops.Remove(selected.GetComponent<TroopOption>().template);
        force.troops.Insert(index + 1, selected.GetComponent<TroopOption>().template);
        Refresh();
    }
}
