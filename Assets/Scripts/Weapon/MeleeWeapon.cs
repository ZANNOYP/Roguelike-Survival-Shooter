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
    // 挥砍轨迹
    public AnimationCurve posXCurve;
    public AnimationCurve posYCurve;
    public AnimationCurve rotCurve;
    // 一次攻击已伤害敌人
    private HashSet<EnemyHealth> hitTargets = new HashSet<EnemyHealth>();
    // 击退数据
    private RepelData repelData;

    public override void Atk()
    {
        // 没有敌人进入范围 直接返回
        EnemyBase enemyControl = EnemyManager.Instance.GetAtkEnemy(data.weaponConfig);
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
        float duration = config.atkDuration;

        float timer = 0;
        float speed = 3f;

        float nowAngle = 0;
        float posY = 0;

        // 未达到攻击持续时间 不停改变武器角度
        while (timer <= duration) 
        {
            timer += speed * Time.deltaTime;
            float t = timer / duration;

            nowAngle = dir.x >= 0 ? rotCurve.Evaluate(t) : -rotCurve.Evaluate(t);
            transform.localRotation = Quaternion.Euler(0, 0, nowAngle);

            posY = dir.x >= 0 ? posYCurve.Evaluate(t) : -posYCurve.Evaluate(t);
            transform.localPosition = new Vector3(posXCurve.Evaluate(t), posY, 0);

            yield return null;
        }
        // 挥砍结束 恢复武器状态 失活碰撞器
        transform.localRotation = Quaternion.identity;
        transform.localPosition = Vector3.zero;
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
                MusicManager.instance.PlayEff(Eff_Type.Hit);
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
