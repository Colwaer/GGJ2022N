using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//这是wtx写的用来自动填充白块的脚本
public class MapGenerator : MonoBehaviour
{
    public Map map;
    public GameObject whitePrefab;
    public Transform white;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int x = (int)map.Size.x, y = (int)map.Size.y;
        for(int i = 0; i < x; i++)
        {
            for(int j = 0; j < y; j++)
            {
                if(map.map[i, j] == null)
                {
                    GameObject t = Instantiate(whitePrefab, white);
                    t.transform.position = new Vector3(i, j, 0);
                    map.map[i, j] = new MapUnit(Map.MapType.White, t.transform, map);
                }
            }
        }
        gameObject.SetActive(false);
    }
}
