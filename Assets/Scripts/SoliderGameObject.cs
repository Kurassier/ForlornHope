using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderGameObject : MonoBehaviour{
    public SpriteRenderer spriteRenderer;
    private Sprite sprite;    


	// Use this for initialization
	void Start () {
        sprite = Resources.Load<Sprite>("MusketeerR");
        this.transform.position = new Vector3(transform.position.x,transform.position.y,20);
        gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Initialize(string soldier)
    {

    }
}
