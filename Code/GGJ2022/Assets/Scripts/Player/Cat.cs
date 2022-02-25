using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator animator;

    public float speed = 1.5f;

    public bool isGrid = false;

    public Map map;

    private Vector2 originPos;

    

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        originPos = transform.position;
        
    }

    public void _Update()
    {
        if (!isGrid)
            HandleInput();
        else
            HandleGridInput();
    }
    public void _FixedUpdate()
    {
        PhysicsDetect();
        Move();
    }
    public virtual bool EnableSwitchCat()
    {
        return true;
    }
    public virtual void PlayDieAnim()
    {
        animator.Play("Die");
    }
    public virtual void Die()
    {
        transform.position = originPos;
        isGrid = false;

        animator.Play("Idle");
    }

    public virtual void Enter()
    {
        // 这里可以play animation？
    }
    public virtual void Exit()
    {

    }

    protected virtual void HandleGridInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToCat();
        }
            
    }
    public virtual void ToGrid()
    {
        isGrid = true;
    }
    public virtual void ToCat()
    {
        isGrid = false;
    }


    protected virtual void HandleInput()
    {
        Debug.LogError("");
    }
    protected virtual void Move()
    {
        Debug.LogError("");
    }
    protected virtual void PhysicsDetect()
    {
        
    }
    
}
