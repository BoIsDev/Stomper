using UnityEngine;

public class WalkEnemy : Enemy
{
    [SerializeField] public float walkSpeedEnemy;

    void Start()
    {
        base.Start();
        base.moveSpeedEnemy = walkSpeedEnemy;
    }

    protected override void Update()
    {
        base.Update();
        // Bổ sung logic di chuyển đặc biệt cho WalkEnemy nếu cần
        // ...
    }

    // Các phương thức hoặc thuộc tính riêng của WalkEnemy nếu cần
}
