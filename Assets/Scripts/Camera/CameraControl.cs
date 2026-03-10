using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 摄像机控制
/// </summary>
public class CameraControl : MonoBehaviour
{
    // 跟随目标
    public Transform target;
    // 玩家x轴移动限制距离
    public float playerX = 12;
    // 玩家y轴移动限制距离
    public float playerY = 16;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        Vector3 playerPos = target.position;
        // 将位置限制在最大距离之内
        playerPos.x = Mathf.Clamp(playerPos.x, -playerX, playerX);
        playerPos.y = Mathf.Clamp(playerPos.y, -playerY, playerY);
        playerPos.z = transform.position.z;
        transform.position = playerPos;
    }
    
}
