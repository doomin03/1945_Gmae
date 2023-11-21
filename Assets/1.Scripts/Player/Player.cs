using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Direction
    {
        Center = 0,
        Left,
        Right
    }
    private Direction dir = Direction.Center;

    [SerializeField]
    private List<Sprite> centerSP;
    [SerializeField]
    private List<Sprite> leftSP;
    [SerializeField]
    private List<Sprite> rightSP;
    [SerializeField]
    private SpriteRenderer sr;
    [SerializeField]
    private GameObject[] LifeObj;
    [SerializeField]
    private Follower[] followers;
    [SerializeField]
    private Transform parent;
    [SerializeField]
    private MyBullet bullet;

    private float speed = 3f;
    private float damage = 1f;
    private float powerCnt;

    private List<MyBullet> myBullets = new List<MyBullet>();

    // Start is called before the first frame update
    void Start()
    {
        //powerCnt = Resources.Load<MyBullet>($"PlayerBullet/PlayerBullet/").L;
        for (int i = 0;i<9;i++)
        {

           myBullets.Add( Resources.Load<MyBullet>($"PlayerBullet/PlayerBullet {i+1}"));
        }
        bullet = myBullets[0];
        dir = Direction.Center;
        GetComponent<SpriteAnimation>().SetSprite(centerSP, 0.2f);

        InvokeRepeating("CreateBullet", 1f, 0.2f);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.Instance.GamePlayType!=GamePlayType.Play) 
            return;
        // 캐릭터 이동 범위 지정
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float clampX = Mathf.Clamp(transform.position.x + x, -2, 2);

        float y = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;
        float clampY = Mathf.Clamp(transform.position.y + y, -4, -1);

        
        transform.position = new Vector3(clampX, clampY, 0f);
        
        if( x < 0 && dir != Direction.Left)
        {
            dir = Direction.Left;
            GetComponent<SpriteAnimation>().SetSprite(leftSP, 0.2f);
        }
        else if(x > 0 && dir != Direction.Right)
        {
            dir = Direction.Right;
            GetComponent<SpriteAnimation>().SetSprite(rightSP, 0.2f);
        }
        else if(x == 0 && dir != Direction.Center)
        {
            dir = Direction.Center;
            GetComponent<SpriteAnimation>().SetSprite(centerSP, 0.2f);
        }
    }

    public void CreateBullet()
    {
        MyBullet eb = Instantiate(bullet, transform);
        eb.transform.localPosition = new Vector2(0f, 1f);
        eb.SetTempParent(parent);
        eb.Damage = damage;
        eb.Initialize();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Enemy"))
        {
            collision.GetComponent<Enemy>().Damage(10000);
        }
        else if (collision.tag.Equals("Coin"))
        {
            GameController.Instance.score += 100;
            Destroy(collision.gameObject);
        }
        else if (collision.tag.Equals("Power"))
        {
            GameController.Instance.power += 1;
            bullet = myBullets[(int)GameController.Instance.power-1];
            Destroy(collision.gameObject);
        }
        else if (collision.tag.Equals("SubPlayer"))
        {
            Destroy(collision.gameObject);

            foreach (var item in followers)
            {
                if (!item.IsOpen)
                {
                    item.IsOpen = true;
                    item.Delaytime= 1;
                    item.gameObject.SetActive(true);

                    break;
                }
            }
        }
    }

    public void Die()
    {
        CancelInvoke("CreateBullet");
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CapsuleCollider2D>().enabled = false;

        GameController.Instance.life--;
        if (GameController.Instance.life >= 0)
        {
            LifeObj[GameController.Instance.life].SetActive(false);
        }

        StartCoroutine("ReLife");
        if (GameController.Instance.life <= 0)
        {
            GameController.Instance.GamePlayType = GamePlayType.Stop;
        }
    }

    IEnumerator ReLife()
    {
        bool show = false;
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < 10; i++)
        {
            GetComponent<SpriteRenderer>().enabled = !show;
            show = !show;
            yield return new WaitForSeconds(0.2f);
        }
        gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CapsuleCollider2D>().enabled = true;
        InvokeRepeating("CreateBullet", 0f, 0.3f);
    }
}
