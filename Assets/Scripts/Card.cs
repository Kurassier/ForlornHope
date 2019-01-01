using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;


public class Card : MonoBehaviour {

    protected Vector2 startPosition;
    protected Vector2 startMousePosition;
    public Vector3 targetScale;
    public Vector2 targetPosition;
    

    protected GameObject manager;
    protected bool mouseIsIn;
    protected bool mouseIsDrag;
    protected int index;


    //test
    protected Text log1;
    

    // Use this for initialization
void Start () {
        mouseIsIn = false;
        manager = GameObject.Find("CardManager");
        //test
        log1 = GameObject.Find("log1").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = Vector2.Lerp(transform.position,targetPosition,0.1f);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 0.1f);

        if (mouseIsDrag)
        {
            Vector2 moveToPosition = (Vector2)Input.mousePosition - startMousePosition + startPosition;
            targetPosition.y = moveToPosition.y;
            manager.GetComponent<HorizontalBarWithEffectManager>().targetPosition.x = moveToPosition.x - 160 * index;

            //向管理器发送信息，表明正在被拖动
            manager.GetComponent<HorizontalBarWithEffectManager>().mouseIsDrag = true;
            manager.GetComponent<HorizontalBarWithEffectManager>().dragPosition = transform.position;
        }
    }


    //拖拽事件
    void DragBegin(){
        startPosition = transform.position;
        startMousePosition = Input.mousePosition;
        mouseIsDrag = true;
    }
    void DragEnd()
    {
        //向管理器发送信息，表明停止被拖动
        manager.GetComponent<HorizontalBarWithEffectManager>().mouseIsDrag = false;

        //如果拖拽高度足够，则移出卡牌
        if (targetPosition.y > 500)
        {
            Remove();
        }
        mouseIsDrag = false;
        manager.SendMessage("PointerOut");

    }
    
    void Drag(){
    }

    //设置管理器
    public void SetManager(GameObject gameObject)
    {
        manager = gameObject;
    }
    //鼠标是否在图标上
    void PointerIn()
    {
        mouseIsIn = true;
        manager.SendMessage("PointerIn");
    }
    void PointerOut()
    {
        mouseIsIn = false;
        manager.SendMessage("PointerOut");
    }
    //点击事件
    void Click()
    {
        if (mouseIsDrag == false)
        {
            Remove();
        }
    }
    //设置目标
    public void SetTarget(Vector2 position)
    {
        targetPosition = position;
    }

    //设置编号
    public void SetIndex(int index)
    {
        this.index = index;
    }

    virtual public void Remove()
    {
        gameObject.GetComponent<EventTrigger>().enabled = false;
        manager.GetComponent<HorizontalBarWithEffectManager>().Delete(gameObject);
        StartCoroutine(Fade(0.2f));
    }
    //消逝
    protected IEnumerator Fade(float time)
    {
        for (float i = time; i > 0; i -= Time.deltaTime)
        {
            targetPosition += new Vector2(0, 40);
            targetScale = transform.localScale * (1 + Time.deltaTime);
            gameObject.GetComponent<Image>().color *= 1 - Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
