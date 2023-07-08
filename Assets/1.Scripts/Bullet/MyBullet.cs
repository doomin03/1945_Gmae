using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBullet : Bullet
{
    [SerializeField]
    private Transform tempParent;

    

    public float Damage
    {
        get;
        set;
    }
    public override void Initialize()
    {
        bd.isPlayer = true;
        bd.damage = 1;
        bd.delay = 1f;
        bd.speed = 3f;
        bd.tempParent = tempParent;

        transform.SetParent(tempParent);

        Destroy(gameObject, 7f);
    }

    public override void Move()
    {
        base.Move();
    }





    public override void SetTempParent(Transform trans)
    {
        tempParent = trans;
    }

    void Update() => Move();

    

    
}
