using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ItemDate
{
    public GameObject obj;
    public float speed;
}
public abstract class Item : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    public ItemDate itemDate = new ItemDate(); 
    public abstract void Initialize();

    public virtual void Move()
    {
        itemDate.obj.transform.Translate(Vector3.down*Time.deltaTime* itemDate.speed);
    }

    private void Update()
    {
        if (itemDate.obj == null)
            return;
        Move();
    }
}
