/// <summary>
/// 游戏流程接口
/// </summary>
public interface IGameFlow
{
    /// <summary>
    /// 选择武器
    /// </summary>
    void ChooseWeapon();

    /// <summary>
    /// 开始游戏
    /// </summary>
    void StartGame();

    /// <summary>
    /// 结束游戏
    /// </summary>
    void EndGame();

    /// <summary>
    /// 重置游戏
    /// </summary>
    void ResetGame();
}
