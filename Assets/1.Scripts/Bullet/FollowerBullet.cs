using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FollowerBullet : Bullet
{
    [SerializeField]
    private Transform tempParent;
    
    public override void Initialize()
    {
        bd.damage = 10;
        bd.speed = 5f;
        

        transform.SetParent(tempParent);

    }

    public override void Move()
    {
        base.Move();
    }



    public override void SetTempParent(Transform trans)
    {
        tempParent = trans;
    }
    
    private void Update()=>Move();

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            
            collision.GetComponent<Enemy>().Damage(10);
            Destroy(gameObject);

        }
    }

}
