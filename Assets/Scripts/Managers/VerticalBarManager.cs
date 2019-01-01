using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VerticalBarManager : MonoBehaviour
{
    protected GameObject selected;
    protected List<GameObject> optionList = new List<GameObject>();
    public GameObject option;

    protected Vector2 targetPosition;
    protected int maxAmount = 14;
    // Use this for initialization
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //控制管理器上下滑动
        if (targetPosition.y < 1080 - 50|| optionList.Count <= maxAmount)
        {
            targetPosition = Vector2.Lerp(new Vector2(targetPosition.x, 1080 - 50), targetPosition, 0.1f);
        }
        else if (targetPosition.y > 1080 +  50 + (optionList.Count - maxAmount) * 50 && optionList.Count > maxAmount)
        {
            targetPosition = Vector2.Lerp(new Vector2(targetPosition.x, 1080 + 50 + (optionList.Count - maxAmount) * 50), targetPosition, 0.1f);
        }
        transform.position = Vector2.Lerp(transform.position, targetPosition, 0.1f);
    }

    public bool HasSelected()
    {
        if (selected == null) return false;
        else return true;
    }
    public void RunAfterTime(string Fun, float time)
    {
        Invoke(Fun, time);
    }
    public void Refresh()
    {
        int order = 0;
        foreach (GameObject troop in optionList)
        {
            order++;
            Vector2 temp = new Vector2(transform.position.x, transform.position.y - order * 50);
            troop.transform.position = temp;
        }
    }

    //设置移动目标
    public void MoveTarget(float y)
    {
        targetPosition.y += y;
    }

    virtual public void Select(GameObject selected)
    {
        if (this.selected != selected)
        {
            if (this.selected != null) this.selected.GetComponent<Image>().color *= 2f;
            this.selected = selected;
            selected.GetComponent<Image>().color *= 0.5f;
        }
        else
        {
            this.selected.GetComponent<Image>().color *= 2f;
            this.selected = null;
        }
    }

    virtual public void Up(){
        if (selected == null || optionList.IndexOf(selected) == 0) return;
        int index = optionList.IndexOf(selected);
        optionList.Remove(selected);
        optionList.Insert(index - 1, selected);
        Refresh();
    }
    virtual public void Down(){
        if (selected == null || optionList.IndexOf(selected) == optionList.Count - 1) return;
        int index = optionList.IndexOf(selected);
        optionList.Remove(selected);
        optionList.Insert(index + 1, selected);
        Refresh();
    }
}

