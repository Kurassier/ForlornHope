using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
   public float health = 10;
    public float attack = 5;
    public float maxMove = 5;
    public float range = 5;

    public float move = 0;
    public bool isAttack = false;
    public bool isSelect = false;
    public bool AIisOn = false;
    public Vector2 target;

	// Use this for initialization
	void Start () {
        if (transform.parent.name == "enemy"){
            AIisOn = true;
            target = transform.position;
        }

	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.localPosition.y);
        if (isSelect)
        {
            Vector2 delta = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) / 20;
            move += delta.magnitude;
            if (move < maxMove) transform.position += (Vector3)delta;
        }
        else
        {
        }
        if (AIisOn)
        {
            Vector2 delta = (target - (Vector2)transform.position) / (target - (Vector2)transform.position).magnitude / 10;
            move += delta.magnitude;
            if (move < maxMove) transform.position += (Vector3)delta;
        }
}
