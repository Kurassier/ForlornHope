using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierBarManager : VerticalBarManager {
    public Troop template;
    private InputField counter;
    private HorizontalBarWithEffectManager horizontalBar;
    private void Start()
    {
        counter = GameObject.Find("Amount").GetComponent<InputField>();
        horizontalBar = GameObject.Find("CardManager").GetComponent<HorizontalBarWithEffectManager>();
        //自动列表显示部队
        Refresh();
        //设置target的坐标
        targetPosition = transform.position;
    }

    public void ReRenerate()
    {
         Clear();
        if(template != null)
            foreach (Group group in template.groups)
                Add(group);
        horizontalBar.CardListGenerate(Soldier.totalList);
        Refresh();
    }
    public void Clear()
    {
        //设置计数器
        counter.text = "";
        counter.enabled = false;
        selected = null;
        for (int i = optionList.Count; i > 0; i--) 
        {
            GameObject tempOpt;
            tempOpt = optionList[i - 1];
            optionList.RemoveAt(i - 1);
            Destroy(tempOpt);
        }
    }

    //生成具体类型的卡牌
    public void Add(Group group)
    {
        GameObject tempOpt = GameObject.Instantiate(option, transform);
        ScriptReplace(tempOpt, group);
        optionList.Add(tempOpt);
        Refresh();
    }
    public void Add(Soldier soldier)
    {
        Group group = new Group(soldier, 100);
        template.Add(soldier.name, group.amount);
        GameObject tempOpt = GameObject.Instantiate(option, transform);
        ScriptReplace(tempOpt, group);
        optionList.Add(tempOpt);
        Refresh();
    }
    void ScriptReplace(GameObject option, Group group)
    {
        Destroy(option.GetComponent<Option>());
        option.AddComponent<GroupOption>();
        option.GetComponent<GroupOption>().Initialize(group);
        option.GetComponent<GroupOption>().SetManager(gameObject);
    }

    override public void Select(GameObject selected)
    {
         if (this.selected != selected)
        {
            if (this.selected != null) this.selected.GetComponent<Image>().color *= 2f;
            this.selected = selected;
            selected.GetComponent<Image>().color *= 0.5f;
            //设置计数器
            counter.enabled = true;
            counter.text = selected.GetComponent<GroupOption>().template.amount.ToString();
        }
        else
        {
            this.selected.GetComponent<Image>().color *= 2f;
            this.selected = null;
            //设置计数器
            counter.text = "";
            counter.enabled = false;
        }
    }
    void ChangeAmount()
    {
        if (selected != null) selected.SendMessage("ChangeAmount", int.Parse(counter.text));
    }
    void Delete()
    {
       if (selected == null) return;
        template.groups.Remove(selected.GetComponent<GroupOption>().template);
        optionList.Remove(selected);
        Destroy(selected);
        Refresh(); 
    }
    override public void Up()
    {
        if (selected == null || optionList.IndexOf(selected) == 0) return;
        int index = optionList.IndexOf(selected);
        optionList.Remove(selected);
        optionList.Insert(index - 1, selected);
        template.groups.Remove(selected.GetComponent<GroupOption>().template);
        template.groups.Insert(index - 1, selected.GetComponent<GroupOption>().template);
        Refresh();
    }
    override public void Down()
    {
        if (selected == null || optionList.IndexOf(selected) == optionList.Count - 1) return;
        int index = optionList.IndexOf(selected);
        optionList.Remove(selected);
        optionList.Insert(index + 1, selected);
        template.groups.Remove(selected.GetComponent<GroupOption>().template);
        template.groups.Insert(index + 1, selected.GetComponent<GroupOption>().template);
        Refresh();
    }
}
