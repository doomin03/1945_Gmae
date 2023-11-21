using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Easy,
    Normal,
    Hard,
    Boss
}

public struct EnemySpawnData
{
    public EnemyType type;
    public int point;
    public int hp;
    public int score;
}

public class EnemyContoller : MonoBehaviour
{
    EnemySpawnData esd = new EnemySpawnData();

    [SerializeField] private Enemy[] enemys;
    [SerializeField] private GameObject[] points;
    [SerializeField] protected GameObject boss_Point;
    [SerializeField] private Transform tempParent;

    float Boss_Spawn_score = 100.0f;

    //public delegate void BossSpawnDelegate();

    //public BossSpawnDelegate bossSpawnDelegate;


    private List<Enemy> enemies = new List<Enemy>();
    
    // Start is called before the first frame update
    void Start()
    { 
        InvokeRepeating("SpawnEnemy", 2f, 3);
        //bossSpawnDelegate = SpawnBoss;
    }
    

    void SpawnEnemy()
    {
        int rand = Random.Range(0,100);
        if(rand < 60)
        {
            esd.type= EnemyType.Easy;
        }
        else if (rand<90)
        {
            esd.type = EnemyType.Normal;
        }
        else
        {
            esd.type = EnemyType.Hard;
        }
        
        esd.point = Random.Range(0,points.Length);
        Enemy enemy = Instantiate(enemys[(int)esd.type], points[esd.point].transform);
        enemy.Initialize();
        enemy.SetTempParent(tempParent);
        enemies.Add(enemy);
    }

    void SpawnBoss() {
        
        if (GameController.Instance.score >= Boss_Spawn_score) {
            Enemy boss = Instantiate(enemys[3], boss_Point.transform);
            boss.Initialize();
            boss.SetTempParent(tempParent);
            enemies.Add(boss);
            Boss_Spawn_score += 2000.0f;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0)
            return;

        foreach (var item in enemies)
        {
            item.Move();
        }
        SpawnBoss();
        //if (GameController.Instance.score > 100) {
        //    bossSpawnDelegate.Invoke();
        //}

    }
}
