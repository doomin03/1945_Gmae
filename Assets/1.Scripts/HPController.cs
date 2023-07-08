using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hpRander;
    public void serRenerSize(float curHP,float maxHP)
    {
        Vector2 size = new Vector2(curHP/maxHP,1f);
        hpRander.transform.localScale = size;
    }
}
