using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : Bullet
{
    private Transform tempParent;

    
    public override void Initialize()
    {
        bd.isPlayer = false;
        bd.damage = 1;
        bd.delay = 1f;
        bd.speed = 3f;
        bd.posParent = transform;
    }
    void Update() => Move();
    public override void Move()
    {
        base.Move();
    }



    public override void SetTempParent(Transform trans)
    {
        tempParent = trans;
    }
}
