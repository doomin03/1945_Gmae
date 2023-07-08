using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class Boss :Enemy
{
    [SerializeField]
    private GameObject[] items;

    [HideInInspector]
    public Transform firePosTrans;

    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private Transform tempParent;
    [SerializeField]
    private HPController hpCont;

    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Sprite hit;
    [SerializeField]private List<Sprite> exSprites;



    [SerializeField] private Transform parttenl;

    private void Start()
    {
        Initialize();
        InvokeRepeating("BulletCreateParttenl", 3f, 0.1f);
    }
    private void Update()
    {
        BossMove();
        
        parttenl.Rotate(new Vector3(0, 0, Time.deltaTime*300f));
    }
    public  void BulletCreateParttenl()
    {
        Bullet eb = Instantiate(bullet, parttenl);
        //eb.SetTempParent(tempParent);
        eb.transform.SetParent(tempParent);
        eb.Initialize();
    }
    public override void Initialize()
    {
        ed.obj = gameObject;
        ed.isBoss = true;
        ed.curHP = 1000f;
        ed.maxHP = 1000f;
        ed.speed = 1f;
        ed.score = 1000;
        ed.itemObjs = items;

        firePosTrans = transform.GetChild(0).transform;
        GetComponent<SpriteAnimation>().SetSprite(sprites, 0.2f);
    }

    public override void BulletCreate()
    {
        Bullet eb = Instantiate(bullet, firePosTrans);
        eb.SetTempParent(tempParent);
        eb.Initialize();
    }

    public override void Damage(float damage)
    {
        ed.curHP -= damage;

        //GetComponent<SpriteAnimation>().SetSprite(hit, sprites, 0.1f);
        if(ed.curHP >0)
        {
            GetComponent<SpriteAnimation>().SetSprite(hit, sprites, 0.1f);
        }
        if (ed.curHP <= 0)
        {
            ed.curHP = 0;
            GameController.Instance.score += ed.score;
            CancelInvoke("BulletCreate");
            Debug.Log("Æ÷ÀÎÆ® È¹µæ");

            DropItem();
            GetComponent<BoxCollider2D>().enabled= false;
            GetComponent<SpriteAnimation>().SetSprite(exSprites, 0.1f, Delete);
            
            ed.obj = null;
        }

        hpCont.serRenerSize(ed.curHP, ed.maxHP);
        
    }

    public override void DropItem()
    {
        int itemidx = Random.Range(0, items.Length);
        int rand = Random.Range(0, 100);
        itemidx = 2;

        if (rand < 100)
        {
            Transform trans = GameObject.Find("items").transform;
            Instantiate(items[itemidx], transform).transform.SetParent(trans);
        }
    }


    public override void SetTempParent(Transform trans)
    {
        tempParent = trans;
    }

    void BossMove()
    {
        if (transform.position.y >= 2)
        {
            ed.obj.transform.Translate(new Vector2(0f, (Time.deltaTime * ed.speed) * -1));

        }
    }

    void Delete()
    {
        Destroy(gameObject);        
    }
}
