using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour {

    public GameObject[] mapBlockArray;
    public int col = 10;
    public int row = 10;

    private Transform mapHolder;
    private List<Vector2> positionList = new List<Vector2>();

	// Use this for initialization
	void Start () {
        //初始化地图块集合
        mapHolder = new GameObject("Map").transform;
        //初始化地图坐标表
        positionList.Clear();
        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                Vector2 position = new Vector2(x, y) - (new Vector2(col - 1, row - 1) / 2);
                position.x *= 2;
                positionList.Add(position);
            }
        }

        //test
        positionList.Clear();

        foreach (Vector2 position in positionList)
        {
            GameObject mapBlock = Instantiate(mapBlockArray[0], position, Quaternion.identity, mapHolder);
            mapBlock.transform.position = new Vector3(mapBlock.transform.position.x, mapBlock.transform.position.y, 500);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}
