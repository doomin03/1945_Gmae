using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BulletData
{
    public bool isPlayer;
    public float damage;
    public float speed;
    public float delay;

    public Transform parent;
    public GameObject prefab;

    public Transform tempParent;

    public Transform posParent;
}

public abstract class Bullet : MonoBehaviour
{
    public BulletData bd = new BulletData();

    public abstract void Initialize();
    public virtual void Move()
    {
        if (bd.isPlayer)
        {
            transform.Translate(new Vector2(0f, Time.deltaTime * bd.speed));
        }
        else
        {
            transform.Translate(new Vector2(0f, Time.deltaTime * (bd.speed * -1)));
        }
    }



    public abstract void SetTempParent(Transform trans);

    private void Update() => Move();
    
}
