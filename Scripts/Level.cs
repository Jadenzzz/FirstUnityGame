using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float GROUND_HEIGHT = 1.5f;
    private float MOVING_SPEED = 3f;
    private const float X_EDGE = -20f;
    private const float Y_EDGGE = -10f;
    private const float SPAWNER_POSITION_X = 20f;

    private static Level instance;

    public static Level GetInstance()
    {
        return instance;
    }

    private float SpawnTimerMax;
    private float SpawnTimer;
    private float GroundGap;
    private List<Ground> groundList;
    private List<Zombie> zomList;
    private int score = 0;
    private GameState state;

    private enum Difficulty
    {
        Easy,
        Medium, 
        Hard
    }

    private enum GameState
    {
        Playing,
        Dead,
    }

    private void SetDiff(Difficulty d)
    {
        switch (d)
        {
            case Difficulty.Easy:
                MOVING_SPEED = 3f;
                break;
            case Difficulty.Medium:
                MOVING_SPEED = 5f;
                break;
            case Difficulty.Hard:
                MOVING_SPEED = 9f;
                break;
        }
    }

    private Difficulty GetDiff()
    {
        if (this.score >= 2) return Difficulty.Medium;
     
        if (this.score >= 5) return Difficulty.Hard;

        return Difficulty.Easy;
    }

    private void Awake()
    {
        instance = this;
        groundList = new List<Ground>();
        zomList = new List<Zombie>();
        SetDiff(Difficulty.Easy);
        state = GameState.Playing;
    }
    private void Start()
    {
        Player.GetInstance().OnDied += Player_OnDied;
        CreateGround(0f, 0f, 4f);
    }

    private void Player_OnDied(object sender, System.EventArgs e)
    {
        state = GameState.Dead;
    }
    private void Update()
    {
        if (state == GameState.Playing)
        {
            GetDiff();
            GroundMove();
            GroundSpawning();
            ZombieMove();
        }
        
    }
    private void CreateGround(float x, float y, float width)
    {
        Transform g = Instantiate(GameAsset.GetInstance().pfGround);
        g.position = new Vector3(x, y);

        SpriteRenderer groundRender = g.GetComponent<SpriteRenderer>();
        groundRender.size = new Vector2(width, GROUND_HEIGHT);

        BoxCollider2D groundCollider = g.GetComponent<BoxCollider2D>();
        groundCollider.size = new Vector2(width, GROUND_HEIGHT);
        Ground gr = new Ground(g);
        groundList.Add(gr);
    }

    private void CreateZombie(float x, float y)
    {
        Transform g = Instantiate(GameAsset.GetInstance().pfZombie);
        g.position = new Vector3(x, y);
        Zombie gr = new Zombie(g);
       
        zomList.Add(gr);
    }

    private void GroundMove()
    {
        for (int i = 0; i < groundList.Count; i++)
        {
            Ground g = groundList[i];
            g.Move(MOVING_SPEED);
            if (g.X() < X_EDGE)
            {
                Destroy(g.GetTransform().gameObject);
                groundList.Remove(g);
                i--;
            }
        }
    }

    private void ZombieMove()
    {
        for (int i = 0; i < zomList.Count; i++)
        {
            Zombie g = zomList[i];
            g.Move(MOVING_SPEED);
            if (g.X() < X_EDGE)
            {
                Destroy(g.GetTransform().gameObject);
                zomList.Remove(g);
                i--;
            }
            if (g.Y() < Y_EDGGE)
            {
                Destroy(g.GetTransform().gameObject);
                zomList.Remove(g);
                this.score += 1;
                i--;
                
            }
        }
    }

    private void GroundSpawning()
    {
        int zom = Random.Range(1, 100);
        GroundGap = Random.Range(-6f, 6f);
        SpawnTimerMax = Random.Range(1.5f, 4f);
        SpawnTimer -= Time.deltaTime;
        SetDiff(GetDiff());
        if (SpawnTimer < 0)
        {
            
           
            SpawnTimer += SpawnTimerMax;
            if(zom%2 == 0)
            {
                CreateGround(SPAWNER_POSITION_X, GroundGap, 5f);
                CreateZombie(SPAWNER_POSITION_X, GroundGap+1f);
            }
            else
            {
                CreateGround(SPAWNER_POSITION_X, GroundGap, 5f);
            }
        }
    }
    
    public int GetScore()
    {
        return this.score;
    }

}
