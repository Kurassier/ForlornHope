using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalBarWithEffectManager : MonoBehaviour {


    private List<GameObject> cardList = new List<GameObject>();
    public GameObject card;
    public bool mouseIsIn;
    public bool mouseIsDrag;
    public Vector2 dragPosition;

    public Vector2 targetPosition;

    private float transformLength = 960f;
    private float transformScale = 1.35f;


    private SoldierBarManager soldierBarManager;
    //test
    private Text log1;

    // Use this for initialization
    void Start () {

        soldierBarManager = GameObject.Find("SoldierManager").GetComponent<SoldierBarManager>();

        //test
        log1 = GameObject.Find("log1").GetComponent<Text>();

        mouseIsIn = false;
        //设置target的坐标
        targetPosition = transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        
        if (mouseIsIn)
        {
            //鼠标滚轮移动卡牌
            targetPosition += new Vector2(Input.GetAxis("Mouse ScrollWheel") * 3000, 0);
            //刷新卡牌特效
            if(mouseIsDrag == true)
            {
                EffectTransform(dragPosition.x);
            }
            else
            {
                EffectTransform(Input.mousePosition.x);
            }
        }

        if (cardList.Count > 12)
        {
            if (targetPosition.x > 0)
            {
                targetPosition = Vector2.Lerp(new Vector2(0, targetPosition.y), targetPosition, 0.1f);
            }
            else if (targetPosition.x < 1920 - cardList.Count * 160)
            {
                targetPosition = Vector2.Lerp(new Vector2(1920 - cardList.Count * 160, targetPosition.y), targetPosition, 0.1f);
            }
        }
        else
        {
            targetPosition = Vector2.Lerp(new Vector2(960 - 160 * cardList.Count / 2, targetPosition.y), targetPosition, 0.1f);
        }
        //控制管理器左右滑动
        MoveTo(Vector2.Lerp(transform.position, targetPosition, 0.1f));
        
    }

    public void RunAfterTime(string Fun,float time){
        Invoke(Fun, time);
    }
    public void Refresh()
    {
        int order = 0;
        foreach (GameObject troop in cardList)
        {
            Vector2 temp = new Vector2(order  * 160 + 80 + transform.position.x, 125);
            troop.GetComponent<Card>().SetTarget(temp);
            troop.GetComponent<Card>().SetIndex(order);
            troop.GetComponent<Card>().targetScale = new Vector3(1, 1, 1);
            order++;
        }
    }

    //移动
    public void MoveTo(Vector2 position)
    {
        Move(position - (Vector2)transform.position);
    }
    public void Move(Vector2 deltaPosition)
    {
        transform.position += (Vector3)deltaPosition;
        for (int i = 0;i < transform.childCount; i++){
            transform.GetChild(i).GetComponent<Card>().targetPosition += deltaPosition;
        }
    }

    //鼠标是否在卡牌栏内
    void PointerIn()
    {
        mouseIsIn = true;
    }
    void PointerOut()
    {
        if(mouseIsDrag == false)
        {
            mouseIsIn = false;
            Refresh();
        }
    }
    //卡牌特效
    void EffectTransform(float x)
    {
        foreach (GameObject card in cardList)
        {
            if(Mathf.Abs(card.transform.position.x - x) < transformLength / 2)
            {
                float scale = Mathf.Pow(2 * (transformLength / 2 - Mathf.Abs(card.transform.position.x - x)) / transformLength, 2) * (transformScale - 1) + 1;
                card.transform.localScale = new Vector3(scale, scale, 1);
            }
        }

        //卡牌渲染次序排序
        List<GameObject> tempList = new List<GameObject>(cardList);
        while(tempList.Count != 0)
        {
            GameObject tempCard = tempList[0];
            foreach (GameObject card in tempList)
            {
                if(Mathf.Abs(tempCard.transform.position.x - x)< Mathf.Abs(card.transform.position.x - x))
                {
                    tempCard = card;
                }
            }
            tempCard.transform.SetAsLastSibling();
            tempList.Remove(tempCard);
        }
    }

    //依据兵种列表生成卡牌列表
    public void CardListGenerate(List<Soldier> soldiers)
    {
        for (int i = cardList.Count; i > 0; i--) 
        {
            Delete(cardList[i - 1]);
        }
        foreach(var soldier in soldiers){

            bool skip = false;
            //如果是已有的士兵，直接跳过生成
            if (soldierBarManager.template == null) return;
            foreach (var group in soldierBarManager.template.groups)
            {
                if (group.soldier == soldier)
                {
                    skip = true;
                }
            }
            if (skip) continue;

            //生成士兵
            Add(soldier);
        }
        //自动列表显示部队
        Invoke("Refresh", 0.1f);
    }

    void Add(Soldier soldier)
    {
        GameObject card = Instantiate(this.card, transform);
        cardList.Add(card);
        ScriptReplace(card, soldier);
    }

    //生成具体类型的卡牌
    void ScriptReplace(GameObject card, Soldier soldier){
        Destroy(card.GetComponent<Card>());
        card.AddComponent<SoldierCard>();
        card.GetComponent<SoldierCard>().Initialize(soldier);
        card.GetComponent<SoldierCard>().SetManager(gameObject);
    }

    //删除选中的卡牌
    public void Delete(GameObject card)
    {
        cardList.Remove(card);
        Destroy(card);
        Refresh();
    }
}
