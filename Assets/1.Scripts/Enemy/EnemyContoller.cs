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

    [SerializeField] private Transform tempParent;

    private List<Enemy> enemies = new List<Enemy>();
    
    // Start is called before the first frame update
    void Start()
    { 
        InvokeRepeating("SpawnEnemy", 2f, 3);
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

    // Update is called once per frame
    void Update()
    {
        if (enemies.Count == 0)
            return;

        foreach (var item in enemies)
        {
            item.Move();
        }
    }
}
