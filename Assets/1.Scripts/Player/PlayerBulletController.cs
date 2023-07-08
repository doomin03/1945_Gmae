using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletController : MonoBehaviour
{
    public Transform player;
    void Update()
    {
        Vector2 vec2 = new Vector2(player.position.x, player.position.y + 1);
        transform.position = vec2;
    }
}
