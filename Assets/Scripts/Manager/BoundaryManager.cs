using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
/// <summary>
/// 边界
/// </summary>
public struct Boundary
{
    public float right;
    public float left;
    public float up;
    public float down;
}
/// <summary>
/// 边界管理器
/// </summary>
public class BoundaryManager : MonoBehaviour
{
    public static BoundaryManager instance;
    // 右边界
    public Collider2D right;
    // 左边界
    public Collider2D left;
    // 上边界
    public Collider2D up;
    // 下边界
    public Collider2D down;

    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 获取边界
    /// </summary>
    /// <param name="bounds">对象边界包围盒</param>
    /// <returns></returns>
    public Boundary GetBoundary(Bounds bounds)
    {
        Boundary boundary = new Boundary();
        boundary.right = right.bounds.min.x - bounds.extents.x;
        boundary.left = left.bounds.max.x + bounds.extents.x;
        boundary.up = up.bounds.min.y - bounds.extents.y;
        boundary.down = down.bounds.max.y + bounds.extents.y;
        return boundary;
    }

    /// <summary>
    /// 把位置限制在边界内部
    /// </summary>
    /// <param name="position"></param>
    /// <param name="boundary"></param>
    /// <returns></returns>
    public Vector2 ClampPosition(Vector2 position, Boundary boundary)
    {
        position.x = Mathf.Clamp(position.x, boundary.left, boundary.right);
        position.y = Mathf.Clamp(position.y, boundary.down, boundary.up);
        return position;
    }
}
