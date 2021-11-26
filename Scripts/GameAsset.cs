using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAsset : MonoBehaviour
{
    private static GameAsset instance;
    // Start is called before the first frame update
    public static GameAsset GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }
    public Sprite GroundSprite;
    public Transform pfGround;
    public Sprite ZombieSprite;
    public Transform pfZombie;
    public Sprite PlayerSprite;
    public Transform pfPlayer;
}
