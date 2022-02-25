using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteCat : Cat
{
    Vector2 dir;

    bool moving = false;
    bool arrival = true;

    public Vector2 currentGridCell;
    public Vector2 nextGridCell;
    Vector2 targetPoint;

    protected override void Awake()
    {
        base.Awake();
        currentGridCell = transform.position;
        nextGridCell = transform.position;

        
    }

    protected override void HandleInput()
    {
        
        _Move();
        
    }
    /// <summary>
    /// 从MG Copy来的代码
    /// </summary>
    private void _Move()
    {
        if (dir == Vector2.zero)
        {
            if (Input.GetKey(KeyCode.A))
            {
                //sprite.flipX = true;
                dir = Vector2.left;
                moving = true;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                //sprite.flipX = false;
                dir = Vector2.right;
                moving = true;
            }
            else if (Input.GetKey(KeyCode.W))
            {
                dir = Vector2.up;
                moving = true;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                dir = Vector2.down;
                moving = true;
            }
            else
            {
                moving = false;
            }
            //anim.SetBool("moving", moving);
        }
        else
        {
            //检测边界
            Vector2 targetPos = currentGridCell + dir;
            //Debug.Log(map.GetMapUnitType(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y)));
            if (map.GetMapUnitType(Mathf.RoundToInt(targetPos.x), Mathf.RoundToInt(targetPos.y)) == Map.MapType.Black)
            {
                nextGridCell = targetPos;
            }
            else
            {
                dir = Vector2.zero;
                return;
            }


            //到达时
            if (arrival)
                GetTargetPoint();
            else
            {
                //未到达时
                if (new Vector2(transform.position.x, transform.position.y) == targetPoint)
                {
                    currentGridCell = nextGridCell;
                    arrival = true;
                    dir = Vector2.zero;
                }
                else
                {                
                    transform.position = Vector2.MoveTowards(transform.position, targetPoint, speed * Time.deltaTime);
                }
            }
        }
        animator.SetBool("Moving", moving);
    }

    public override bool EnableSwitchCat()
    {
        if (moving)
            return false;
        return true;
    }
    public override void ToGrid()
    {
        if (moving)
            return;

        base.ToGrid();

        animator.SetTrigger("ToGrid");
        map.ChangeMapUnitType(transform.position, Map.MapType.White);


    }
    public override void ToCat()
    {
        base.ToCat();
        animator.SetTrigger("ToCat");
        map.ChangeMapUnitType(transform.position, Map.MapType.Black);
    }
    public override void Die()
    {
        map.ChangeMapUnitType(transform.position, Map.MapType.Black);

        base.Die();

        currentGridCell = transform.position;
    }

    public void GetTargetPoint()
    {
        targetPoint = nextGridCell;
        arrival = false;
    }

    /// <summary>
    /// 已弃用
    /// </summary>
    protected override void Move()
    {
    }

}
