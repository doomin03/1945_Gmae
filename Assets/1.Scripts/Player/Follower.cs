using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private Transform tempParent;

    public bool IsOpen { get; set; }
    public float Delaytime { get; set; }
    private void Start()
    {
        Delaytime = 1f;
        InvokeRepeating("CreateBullet", 1f, Delaytime);
    }
    void CreateBullet()
    {
        Bullet bul = Instantiate(bullet,transform);
        bul.SetTempParent(tempParent);
        bul.Initialize();
    }
}
