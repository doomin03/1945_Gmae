using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : Bullet
{
    private Transform tempParent;

    private Transform player = null;

    public override void Initialize()
    {
        bd.isPlayer = false;
        bd.damage = 1;
        bd.delay = 1f;
        bd.speed = 3f;
        bd.posParent = transform;

        if (GameObject.FindGameObjectWithTag("Player") == null)
            return;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector2 vec = transform.position - player.position;
        // Mathf.Rad2Deg = 360 / (PI * 2)
        float angle = Mathf.Atan2(vec.y, vec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        transform.SetParent(tempParent);

        Destroy(gameObject, 10f);
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            collision.GetComponent<Player>().Die();
        }
    }
}
