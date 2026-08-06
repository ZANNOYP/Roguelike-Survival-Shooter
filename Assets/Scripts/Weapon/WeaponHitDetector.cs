using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器检测
/// </summary>
public class WeaponHitDetector : MonoBehaviour
{
    // 近战武器
    public MeleeWeapon meleeWeapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        meleeWeapon.Hit(collision);
    }

}
