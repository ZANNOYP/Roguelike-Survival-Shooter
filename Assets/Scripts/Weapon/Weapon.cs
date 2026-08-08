using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    // 武器运行时数据
    public WeaponData data = new WeaponData();
    // 玩家
    public Transform player;
    // 攻击协程
    public Coroutine attackCoroutine;
    // 武器旋转根对象
    public Transform weaponRoot;
    // 武器旋转速度
    public float rotateSpeed = 180f;
    // 武器图片渲染
    public SpriteRenderer sr;
    // 玩家数据
    protected PlayerData playerData;
    // 正在挥砍
    protected bool isSwing;
    // 是否瞄准完毕
    protected bool isAimReady;
    
    // Start is called before the first frame update
    void Start()
    {
        playerData = DataManager.instance.playerRuntimeData;
    }

    private void Update()
    {
        WeaponAiming();
    }

    /// <summary>
    /// 武器瞄准
    /// </summary>
    public void WeaponAiming()
    {
        if (isSwing) return;
        // 没有敌人进入范围 直接返回
        EnemyControl enemyControl = EnemyManager.Instance.GetNearestEnemy();
        if (enemyControl == null)
        {
            weaponRoot.rotation = Quaternion.identity;
            sr.flipY = false;
            return;
        }
        Vector3 dir = (enemyControl.transform.position - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

        weaponRoot.rotation = Quaternion.RotateTowards(weaponRoot.rotation, targetRotation, rotateSpeed * Time.deltaTime);
        
        Vector2 right = weaponRoot.right;
        sr.flipY = right.x >= 0 ? false : true; 

        float angle2 = Quaternion.Angle(weaponRoot.rotation, targetRotation);
        if (angle2 < 10f)
        {
            isAimReady = true;
        }
        else
        {
            isAimReady = false;
        }
    }

    /// <summary>
    /// 攻击
    /// </summary>
    public abstract void Atk();

    /// <summary>
    /// 设置武器贴图
    /// </summary>
    public void SetWeaponSprite()
    {
        WeaponConfig config = data.weaponConfig;
        sr.sprite = config.weaponSprite;
        sr.transform.localPosition = config.spritePos;
        sr.transform.localScale = config.spriteScale * Vector2.one;
    }

    
}
