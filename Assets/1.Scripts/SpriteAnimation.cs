using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteAnimation : MonoBehaviour
{
    private List<Sprite> sprites = new List<Sprite>();
    private SpriteRenderer sr;

    private float spriteDelayTime;
    private float delayTime = 0f;

    private int spriteAnimationIndex = 0;

    private UnityAction Action = null;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprites.Count == 0)
            return;

        delayTime += Time.deltaTime;
        if(delayTime > spriteDelayTime)
        {
            delayTime = 0;
            sr.sprite = sprites[spriteAnimationIndex];
            spriteAnimationIndex++;

            if (spriteAnimationIndex > sprites.Count - 1)
            {
                if(Action == null)
                {
                    spriteAnimationIndex = 0;
                }
                else
                {
                    
                    sprites.Clear();
                    Action();
                    Action = null;
                }
            }
            
           
        }
    }
    void init()
    {
        delayTime = 0f;
        sprites.Clear();
        spriteAnimationIndex = 0;

    }
    public void SetSprite(List<Sprite> argSprites, float delayTime)
    {
        init();
        sprites = argSprites.ToList();
        spriteDelayTime = delayTime;
    }
    public void SetSprite(List<Sprite> argSprites, float delayTime ,UnityAction Action)
    {
        this.Action = Action;
        init();
        sprites = argSprites.ToList();
        spriteDelayTime = delayTime;
    }
    public void SetSprite(Sprite sprite, List<Sprite> argSprites,float delaytime)
    {
        init();
        sr.sprite = sprite;
        StartCoroutine(ReturnSprite(argSprites, delaytime));
    }

    IEnumerator ReturnSprite(List<Sprite> argSprites, float delaytime)
    {
        yield return new WaitForSeconds(0.05f);
        SetSprite(argSprites, delaytime);
    }
}
