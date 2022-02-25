using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Map : MonoBehaviour
{
    public Vector2 Size = new Vector2(9, 9);
    IEnumerator IChangeColor;
    public enum MapType
    {
        Null, White, Black
    }
    public MapUnit[,] map;

    private void Awake()
    {
        map = new MapUnit[(int)Size.x, (int)Size.y];

        Transform blackGrids = transform.GetChild(0);
        Transform whiteGrids = transform.GetChild(1);

        Transform[] mapUnits = blackGrids.GetComponentsInChildren<Transform>();
        foreach (Transform t in mapUnits)
        {
            Vector2 p = t.position;
            //Debug.Log(new Vector2(Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y)));
            map[Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y)] = new MapUnit(MapType.Black, t, this);
        }
        mapUnits = whiteGrids.GetComponentsInChildren<Transform>();
        foreach (Transform t in mapUnits)
        {
            Vector2 p = t.position;
            map[Mathf.RoundToInt(p.x), Mathf.RoundToInt(p.y)] = new MapUnit(MapType.White, t, this);
        }
    }
    public MapType GetMapUnitType(int x, int y)
    {
        if (x < 0 || x >= Size.x || y < 0 || y >= Size.y)
            return MapType.Null;
        return map[x, y].type;
    }
    public bool ChangeMapUnitType(int x, int y, MapType t)
    {
        if (x < 0 || x >= Size.x || y < 0 || y >= Size.y)
            return false;
        Debug.Log("change map unit : " + x + " " + y + " to " + t);
        map[x, y].type = t;
        return true;
    }
    public bool ChangeMapUnitType(Vector2 pos, MapType t)
    {
        int x = Mathf.RoundToInt(pos.x);
        int y = Mathf.RoundToInt(pos.y);
        if (x < 0 || x >= Size.x || y < 0 || y >= Size.y)
            return false;
        
        if (t == MapType.Black)
        {
            map[x, y].WhiteToBlack();
        }
        else if (t == MapType.White)
        {
            map[x, y].BlackToWhite();
        }

        map[x, y].type = t;
        return true;
    }
    public void StartChangeColorCourtine(MapUnit unit, Color targetColor, float time)
    {
        if (IChangeColor != null)
            StopCoroutine(IChangeColor);
        IChangeColor = unit.IChangeColor(targetColor, time);
        StartCoroutine(IChangeColor);
    }
}
public class MapUnit
{
    public Map.MapType type;
    public Transform transform;
    private BoxCollider2D collider;
    private SpriteRenderer sp;
    private Map map;
    public bool isBlackBefore => collider != null;


    public void BlackToWhite()
    {
        if (!isBlackBefore)
            return;
        collider.enabled = false;
        Color spColor = sp.color;
        
        spColor.a = 0;
        map.StartChangeColorCourtine(this, spColor, 0.8f);
    }
    public void WhiteToBlack()
    {
        if (!isBlackBefore)
            return;
        collider.enabled = true;
        Color spColor = sp.color;
        spColor.a = 1;

        map.StartChangeColorCourtine(this, spColor, 0.8f);

    }
    public IEnumerator IChangeColor(Color targetColor, float time)
    {
        Color originColor = sp.color;
        float timer = 0;
        while (timer <= time)
        {
            timer += Time.deltaTime;
            sp.color = Color.Lerp(originColor, targetColor, timer / time);
            yield return null;
        }
    }
    public MapUnit(Map.MapType type, Transform transform, Map map)
    {
        this.map = map;
        this.type = type;
        this.transform = transform;

        if (type == Map.MapType.Black)
        {
            collider = transform.GetComponent<BoxCollider2D>();
            sp = transform.GetComponent<SpriteRenderer>();
        }
            
    }
}