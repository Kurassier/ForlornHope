using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleFieldManager : MonoBehaviour {

 public List<GameObject> mapBlockArray = new List<GameObject>();
    public int col = 10;
    public int row = 10;
    public const float MapMovingSpeed = 5;
    public float scale = 1;


    private List<Vector2> positionList = new List<Vector2>();

    // Use this for initialization
    void Start()
    {
        //初始化地图坐标表
        //右下为x轴正方向，左下为y轴正方向
        Vector3 position;
        GameObject mapBlock;
        for (int x = 0; x < col; x++)
        {
            for (int y = 0; y < row; y++)
            {
                position = Position.MapToAbsolute(new Vector3(x, y, 0) * 5);
                mapBlock = Instantiate(mapBlockArray[0], position, Quaternion.identity, transform);
                mapBlock.transform.position = position;
                
            }
        }
        
        //获取部队列表并且生成
        Soldier.SoldierListCreator(TxtReader.TxtCuter(Resources.Load<TextAsset>("File/Soldier")));
    }

    // Update is called once per frame
    void Update () {
        //挪动与缩放地图
        if (Input.mousePosition.y > 1030)
            transform.position -= new Vector3(0, MapMovingSpeed * Time.deltaTime, 0);
        if (Input.mousePosition.y < 50)
            transform.position += new Vector3(0, MapMovingSpeed * Time.deltaTime, 0);
        if (Input.mousePosition.x > 1870)
            transform.position -= new Vector3(MapMovingSpeed * Time.deltaTime, 0, 0);
        if (Input.mousePosition.x < 50)
            transform.position += new Vector3(MapMovingSpeed * Time.deltaTime, 0, 0);

        scale += Input.GetAxis("Mouse ScrollWheel") / 2;
        if (scale > 2) scale = 2;
        else if (scale < 0.3) scale = 0.3f;
        transform.localScale = new Vector3(scale, scale, 1);
    }
    
}
