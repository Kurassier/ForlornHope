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

	}
	
	// Update is called once per frame
	void Update () {

    }


    //拖拽事件
    void DragBegin(){

    }
    void DragEnd()
    {


    }
    
    void Drag(){
    }

    //设置管理器
    public void SetManager(GameObject gameObject)
    {

    }
    //鼠标是否在图标上
    void PointerIn()
    {

    }
    void PointerOut()
    {

    }
    //点击事件
    void Click()
    {

    }
    //设置目标
    public void SetTarget(Vector2 position)
    {

    }

    //设置编号
    public void SetIndex(int index)
    {

    }

    virtual public void Remove()
    {

    }
    //消逝
    protected IEnumerator Fade(float time)
    {

    }
}
