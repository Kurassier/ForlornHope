using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour {
    protected Vector2 startPosition;
    protected Vector2 startMousePosition;

    protected GameObject manager;
    protected bool mouseIsIn;

    //test
    protected Text log1;

    // Use this for initialization
    void Start ()
    {
        mouseIsIn = false;
    }
	
	// Update is called once per frame
	void Update () {
        if(mouseIsIn == true)
        {
            manager.GetComponent<VerticalBarManager>().MoveTarget(Input.GetAxis("Mouse ScrollWheel") * -1000);
        }
    }

    //拖拽事件
    void DragBegin()
    {
        startPosition = manager.transform.position;
        startMousePosition = Input.mousePosition;

    }
    void Drag()
    {
        Vector2 movePosition = (Vector2)Input.mousePosition - startMousePosition;
        startMousePosition = Input.mousePosition;

        manager.GetComponent<VerticalBarManager>().MoveTarget(movePosition.y);
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
    }
    void PointerOut()
    {
        mouseIsIn = false;
    }
    void Select()
    {
        manager.GetComponent<VerticalBarManager>().Select(gameObject);
    }
}
