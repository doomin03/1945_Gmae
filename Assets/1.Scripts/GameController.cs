using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GamePlayType
{
    Play,
    Pause,
    Stop,
}
public class GameController : MonoBehaviour
{
    public static GameController Instance;
    private void Awake()=> Instance = this;

    public GamePlayType GamePlayType = GamePlayType.Play;
    public int life = 3;
    public float power = 1f;
    public int score = 0;
    public bool leftSub = false;
    public bool rightSub = false;

}
