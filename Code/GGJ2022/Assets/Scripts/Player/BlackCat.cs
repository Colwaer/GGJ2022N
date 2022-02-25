using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackCat : Cat
{
    Vector2 direction = Vector2.zero;
    float jumpHeight = 2.3f;

    [HideInInspector]
    public bool haveWeapon = false;

    [SerializeField]
    bool onGround = true;
    public LayerMask GroundLayer;

    private float airSpeed;

    private float groundCastLength = 0.49f;
    private float groundCastXoffset = 0.2f;

    private bool attacking = false;
    private Sword weapon;

    protected override void Awake()
    {
        base.Awake();
        airSpeed = speed / 6 * 5;
    }
    protected override void HandleInput()
    {
        direction.x = Input.GetAxisRaw("Horizontal");

        if(attacking)
        {
            direction.x = 0;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    
    protected override void Move()
    {
        if (onGround)
        {
            rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(direction.x * airSpeed, rb.velocity.y);
        }

    }

    private void Jump()
    {
        if (onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Sqrt(-Physics2D.gravity.y * 2 * jumpHeight));
            Debug.Log("Jump speed" + new Vector2(rb.velocity.x, Mathf.Sqrt(-Physics2D.gravity.y * 2 * jumpHeight)));
        }

    }
    public override bool EnableSwitchCat()
    {
        if (attacking)
            return false;
        if (onGround && direction.x == 0)
            return true;
        return false;
    }
    /// <summary>
    /// 先移动到整数坐标，再变成格子
    /// </summary>
    public override void ToGrid()
    {

        Debug.Log("BlackCat To Grid");
        base.ToGrid();
        Vector2 pos = transform.position;
        StartCoroutine(IToGrid(pos, new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.y)), speed / 2));
    }
    

    public override void ToCat()
    {
        Debug.Log("BlackCat To Cat");
        map.ChangeMapUnitType(transform.position, Map.MapType.White);
        animator.SetTrigger("ToCat");
        isGrid = false;      
    }
    public override void Die()
    {
        map.ChangeMapUnitType(transform.position, Map.MapType.White);



        base.Die();
    }
    IEnumerator IToGrid(Vector2 originPos, Vector2 targetPos, float speed)
    {
        float distance = targetPos.x - originPos.x;
        float direction = targetPos.x - originPos.x > 0 ? 1 : -1;
        Vector3 originScale = transform.localScale;
        originScale.x = Mathf.Abs(originScale.x) * direction;
        transform.localScale = originScale;
        animator.SetBool("Moving", true);
        
        while (Mathf.Abs(transform.position.x - targetPos.x) > 0.03f)
        {
            transform.position = (Vector2)transform.position + Time.fixedDeltaTime * speed * direction * Vector2.right;
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Moving", false);
        yield return new WaitForFixedUpdate();
        animator.SetTrigger("ToGrid");
        Vector2 pos = transform.position;
        map.ChangeMapUnitType(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Map.MapType.Black);



    }

    public void GiveWeapon(Sword sword)
    {
        weapon = sword;
        haveWeapon = true;
    }
    public void BeginAttack(Transform enemyTrans)
    {
        StartCoroutine(AttackCoroutine(enemyTrans));
    }

    IEnumerator AttackCoroutine(Transform enemyTrans)
    {
        attacking = true;
        //播放动画

        animator.Play("Attack");
        weapon.Attack(enemyTrans);
        yield return new WaitForSeconds(1.8f);//这个时间可能调整
        attacking = false;
    }


    public override void Enter()
    {

    }
    public override void Exit()
    {
        direction = Vector2.zero;

        
    }
    

    protected override void PhysicsDetect()
    {
        Vector2 pos = transform.position;

        RaycastHit2D hit1 = Physics2D.Raycast(new Vector2(pos.x - groundCastXoffset, pos.y), Vector2.down,
            groundCastLength, GroundLayer);
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(pos.x + groundCastXoffset, pos.y), Vector2.down,
            groundCastLength, GroundLayer);

        if (hit1 || hit2)
            onGround = true;
        else
            onGround = false;
        //Debug.Log(onGround);
        animator.SetBool("OnGround", onGround);
        if (direction.x > 0.05f)
        {
            Vector3 originScale = transform.localScale;
            originScale.x = Mathf.Abs(originScale.x);
            transform.localScale = originScale;
            animator.SetBool("Moving", true);
        }
        else if (direction.x < -0.05f)
        {
            Vector3 originScale = transform.localScale;
            originScale.x = -Mathf.Abs(originScale.x);
            transform.localScale = originScale;
            animator.SetBool("Moving", true);
        }
        else
        {
            animator.SetBool("Moving", false);
        }
    }
    private void OnDrawGizmos()
    {
        Vector2 pos = transform.position;
        Vector2 leftLinePos = new Vector2(pos.x - groundCastXoffset, pos.y);
        Vector2 rightLinePos = new Vector2(pos.x + groundCastXoffset, pos.y);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(leftLinePos, leftLinePos + Vector2.down * groundCastLength);
        Gizmos.DrawLine(rightLinePos, rightLinePos + Vector2.down * groundCastLength);
    }
}
