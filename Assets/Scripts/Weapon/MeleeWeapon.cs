using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
/// <summary>
/// 近战武器
/// </summary>
public class MeleeWeapon : Weapon
{
    // 碰撞器
    public Collider2D col;
    // 一次攻击已伤害敌人
    private HashSet<EnemyHealth> hitTargets = new HashSet<EnemyHealth>();
    // 击退数据
    private RepelData repelData;

    public override void Atk()
    {
        // 没有敌人进入范围 直接返回
        EnemyControl enemyControl = EnemyManager.Instance.GetAtkEnemy(data.weaponConfig);
        if (enemyControl == null) return;
        Vector3 dir = (enemyControl.transform.position - transform.position).normalized;
        MeleeWeaponConfig mwConfig = data.weaponConfig as MeleeWeaponConfig;
        if (repelData == null)
        {
            repelData = new RepelData();
        }
        repelData.repelSpeed = mwConfig.repelSpeed;
        repelData.repelTime = mwConfig.repelTime;
        StartCoroutine(Swing(dir, mwConfig));
    }

    /// <summary>
    /// 武器挥砍旋转协程
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    private IEnumerator Swing(Vector2 dir, MeleeWeaponConfig config)
    {
        // 清空列表 改变武器状态 激活碰撞器
        hitTargets.Clear();
        isSwing = true;
        col.enabled = true;

        // 计算角度
        float atkAngle = config.atkAngle;
        float duration = config.atkDuration;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        float startAngle = dir.x >= 0 ? angle + atkAngle / 2 : angle - atkAngle / 2;
        float endAngle = dir.x >= 0 ? angle - atkAngle / 2 : angle + atkAngle / 2;
        float nowAngle = startAngle;
        float timer = 0;
        float speed = 3f;
        // 未达到攻击持续时间 不停改变武器角度
        while (timer <= duration) 
        {
            timer += speed * Time.deltaTime;
            float t = timer / duration;
            nowAngle = Mathf.Lerp(startAngle, endAngle, t);
            weaponRoot.rotation = Quaternion.Euler(0, 0, nowAngle);

            yield return null;
        }
        // 挥砍结束 恢复武器状态 失活碰撞器
        isSwing = false;
        col.enabled = false;
    }

    /// <summary>
    /// 武器碰撞判定 用于子类脚本调用
    /// </summary>
    /// <param name="col"></param>
    public void Hit(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            EnemyHealth enemy = col.GetComponent<EnemyHealth>();
            if (hitTargets.Contains(enemy)) return;
            if (enemy != null)
            {
                hitTargets.Add(enemy);
                float damage = data.GetDamage() * playerData.damageMultiplier;
                enemy.ChangeHp(-damage, repelData);
            }
        }
    }

    /// <summary>
    /// 设置碰撞器大小
    /// </summary>
    public void SetColSize()
    {
        MeleeWeaponConfig meleeWeaponConfig = data.weaponConfig as MeleeWeaponConfig;
        BoxCollider2D boxCol = col as BoxCollider2D;
        boxCol.offset = meleeWeaponConfig.colOffset;
        boxCol.size = meleeWeaponConfig.colSize;
    }
}
