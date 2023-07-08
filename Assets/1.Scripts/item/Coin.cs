using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Item
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();

    public override void Initialize()
    {
        itemDate.obj = gameObject;
        itemDate.speed = 3f;
        GetComponent<SpriteAnimation>().SetSprite(sprites, 0.2f);
    }

    // Start is called before the first frame update
    void Start()
    {
       
        Initialize();
        
    }

}
