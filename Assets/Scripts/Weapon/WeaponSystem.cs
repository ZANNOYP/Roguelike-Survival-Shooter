using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 武器系统
/// </summary>
public class WeaponSystem : MonoBehaviour
{
    public static WeaponSystem instance;
    // 武器列表
    public List<Weapon> weapons = new List<Weapon>();
    // 玩家
    public Transform player;
    // 武器旋转根对象
    public Transform weaponRoot;
    // 玩家数据
    private PlayerData playerData;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        playerData = DataManager.instance.playerRuntimeData;
    }

    /// <summary>
    /// 装备武器
    /// </summary>
    /// <param name="weConfig"></param>
    public void Equip(WeaponConfig weConfig)
    {
        GameObject weaponObj = GameObject.Instantiate(weConfig.prefab, weaponRoot);
        Weapon weapon = weaponObj.GetComponent<Weapon>();

        weapon.data.Init(weConfig);
        weapon.player = player;
        weapon.weaponRoot = weaponRoot;
        weapon.SetWeaponSprite();
        RangedWeapon rangedWeapon = weapon as RangedWeapon;
        if (rangedWeapon != null)
        {
            rangedWeapon.SetFirePointPos();
        }
        MeleeWeapon meleeWeapon = weapon as MeleeWeapon;
        if (meleeWeapon != null)
        {
            meleeWeapon.SetColSize();
        }
        weapons.Add(weapon);
        weapon.attackCoroutine = StartCoroutine(AttackCoroutine(weapon));
    }

    /// <summary>
    /// 攻击协程
    /// </summary>
    /// <param name="weapon"></param>
    /// <returns></returns>
    private IEnumerator AttackCoroutine(Weapon weapon)
    {
        while (true)
        {
            weapon.Atk();
            float interval = weapon.data.weaponConfig.atkInterval / playerData.fireRateMultiplier;
            MeleeWeaponConfig mwConfig = weapon.data.weaponConfig as MeleeWeaponConfig;
            if (mwConfig != null)
            {
                interval += mwConfig.atkDuration;
            }
            yield return new WaitForSeconds(interval);
        }
    }

    /// <summary>
    /// 卸载武器
    /// </summary>
    /// <param name="we"></param>
    public void UnEquip(Weapon we)
    {
        if (!weapons.Contains(we)) return;

        weapons.Remove(we);
        StopCoroutine(we.attackCoroutine);
        we.attackCoroutine = null;
        Destroy(we.gameObject);

    }

    /// <summary>
    /// 卸载所有武器
    /// </summary>
    public void UnEquipAll()
    {
        while (weapons.Count > 0)
        {
            UnEquip(weapons[0]);
        }
    }
}
