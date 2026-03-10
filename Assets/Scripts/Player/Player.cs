using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum Direction
{
    Forward,
    Backward,
    Left,
    Right
}

enum MoveState
{
    Idle,
    Walk
}

/// <summary>
/// 玩家类
/// </summary>
public class Player : MonoBehaviour
{
    // 移动速度
    public float moveSpeed = 10f;
    // 改变血量事件
    public static event Action<int> OnHpModifyRequested;
    // 移动方向
    Vector3 moveDir;
    // 刚体
    private Rigidbody2D rb;
    public Rigidbody2D Rb => rb;
    // 默认位置
    [SerializeField]
    private Vector2 defaultPos;
    // 最小移动范围
    [SerializeField]
    private Vector2 minBound;
    public Vector2 MinBound => minBound;
    // 最大移动范围
    [SerializeField]
    private Vector2 maxBound;
    public Vector2 MaxBound => maxBound;
    // 动画
    private Animator animator;

    private Direction dir;
    private MoveState moveState;

    private void Awake()
    {
        // 得到刚体
        rb = GetComponent<Rigidbody2D>();
        // 得到动画组件
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        // 游戏流程事件注册
        GameFlowEvents.OnGameStart += SetPos;
    }

    private void OnDisable()
    {
        // 游戏流程事件反注册
        GameFlowEvents.OnGameStart -= SetPos;
    }

    // Update is called once per frame
    void Update()
    {
        // 得到移动的方向
        GetMoveDir();
        // 每帧移动
        Move();
    }

    private void FixedUpdate()
    {
        
    }
    /// <summary>
    /// 得到移动方向
    /// </summary>
    private void GetMoveDir()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveDir = new Vector3(x, y, 0);
        if (moveDir.sqrMagnitude > 1) 
            moveDir = moveDir.normalized;
        if (moveDir.x != 0)
        {
            dir = moveDir.x > 0 ? Direction.Right : Direction.Left;
            moveState = MoveState.Walk;
        }
        else if (moveDir.y != 0)
        {
            dir = moveDir.y > 0 ? Direction.Backward : Direction.Forward;
            moveState = MoveState.Walk;
        }
        else
        {
            moveState = MoveState.Idle;
        }
        animator.SetFloat("Direction", (float)dir);
        animator.SetInteger("MoveState", (int)moveState);
    }

    /// <summary>
    /// 移动
    /// </summary>
    private void Move()
    {
        //Vector2 targetPos = rb.position + moveDir * moveSpeed * Time.fixedDeltaTime;
        //// 限制玩家移动范围
        //targetPos.x = Mathf.Clamp(targetPos.x, minBound.x, maxBound.x);
        //targetPos.y = Mathf.Clamp(targetPos.y, minBound.y, maxBound.y);
        //rb.MovePosition(targetPos);

        Vector3 targetPos = transform.position + moveDir * moveSpeed * Time.deltaTime;
        // 限制玩家移动范围
        targetPos.x = Mathf.Clamp(targetPos.x, minBound.x, maxBound.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minBound.y, maxBound.y);
        transform.position = targetPos;
    }
    /// <summary>
    /// 受伤
    /// </summary>
    /// <param name="demage"></param>
    public void Wound(int demage)
    {
        OnHpModifyRequested?.Invoke(-demage);
    }

    /// <summary>
    /// 加血
    /// </summary>
    /// <param name="hp"></param>
    public void AddHp(int hp)
    {
        OnHpModifyRequested?.Invoke(hp);
    }

    /// <summary>
    /// 重置位置
    /// </summary>
    public void SetPos()
    {
        rb.position = defaultPos;
    }

}
