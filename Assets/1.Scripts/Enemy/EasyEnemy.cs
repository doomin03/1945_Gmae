using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemy : Enemy
{
    [SerializeField]
    private GameObject[] items;

    [HideInInspector]
    public Transform firePosTrans;

    [SerializeField]
    private EnemyBullet bullet;
    [SerializeField]
    private Transform tempParent;
    [SerializeField]
    private HPController hpCont;

    public override void Initialize()
    {
        ed.obj = gameObject;
        ed.isBoss = false;
        ed.curHP = 5f;
        ed.maxHP = 5f;
        ed.speed = 1f;
        ed.score = 1;
        ed.itemObjs = items;

        firePosTrans = transform.GetChild(0).transform;

        InvokeRepeating("BulletCreate", 2f, 5f);
    }

    public override void Move()
    {
        base.Move();
    }

    public override void Damage(float damage)
    {
        ed.curHP-=damage;   

        if (ed.curHP <= 0)
        {
            ed.curHP=0;
            GameController.Instance.score += ed.score;
            CancelInvoke("BulletCreate");
            Debug.Log("Æ÷ÀÎÆ® È¹µæ");

            DropItem();
            Delete();
            ed.obj = null;
        }
        hpCont.serRenerSize(ed.curHP, ed.maxHP);
    }

    public override void BulletCreate()
    {
        EnemyBullet eb = Instantiate(bullet, firePosTrans);
        eb.SetTempParent(tempParent);
        eb.Initialize();
    }

    public override void SetTempParent(Transform trans)
    {
        tempParent = trans;
    }

    public override void DropItem()
    {
        int itemidx =Random.Range(0,items.Length); 
        int rand = Random.Range(0,100);
        if (rand < 100)
        {
            Transform trans = GameObject.Find("items").transform;
            Instantiate(items[itemidx], transform).transform.SetParent(trans);
        }
    }
    void Delete()
    {

        Destroy(ed.obj);
        ed.obj = null;
    }

}
