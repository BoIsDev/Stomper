using UnityEngine;

public class WalkEnemy : Enemy
{
    public float walkSpeedEnemy =1f;
   
    protected override void Start()
    {
        base.Start();
        base.moveSpeedEnemy = walkSpeedEnemy;
    }
    protected override void Update()
    {
        base.Update();
    }
}
