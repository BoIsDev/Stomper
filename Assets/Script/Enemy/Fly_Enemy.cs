

using UnityEngine;
public class Fly_Enemy : Enemy
{
    [SerializeField] public float flySpeedEnemy = 2f;

    
    protected override void Start()
    {
        base.Start();
        base.moveSpeedEnemy = flySpeedEnemy;

    }

    protected override void Update()
    {
       base.Update();

    }


   

}
