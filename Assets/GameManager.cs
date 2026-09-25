using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        INACTIVE,
        ALIVE,
        DEAD
    }

    public GameState gameState;
    [SerializeField] public EnemySpawner spawner;
    [SerializeField] public RandomSpawner randomSpawner;
    [SerializeField] public SetSpawner setSpawner;
    public static GameManager Instance {get; private set;}
    [SerializeField] private Transform groundTf;
    [SerializeField] private Player player;
    [SerializeField] private GameObject[] stateUI;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private GameObject currentUI => stateUI[(int)gameState];
    void Awake()
    {
        if(Instance == null) Instance = this;
        else if(Instance != this) Destroy(this);
        enemies = new();
    }

    void Start()
    {
        SwitchState(GameState.INACTIVE);
        spawner = randomSpawner;
    }

    public void SetSaw()
    {
        spawner = setSpawner;
    }

    public void SetTri()
    {
        spawner = randomSpawner;
    }

    void Update()
    {
        groundTf.position = new Vector2(
            player.transform.position.x,
            groundTf.position.y
        );
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RespawnPlayer()
    {
        player.SetAlive(true);
        SwitchState(GameState.ALIVE);
        spawner.SpawnEnemies();
    }

    public void ToMenu()
    {
        player.gameObject.SetActive(true);
        player.transform.position = Vector2.zero;
        SwitchState(GameState.INACTIVE);

        for(int i = enemies.Count-1; i >= 0; i--)
        {
            Destroy(enemies[i]);
        }
        enemies.Clear();
    }

    public void KillPlayer()
    {
        player.gameObject.SetActive(false);
        player.SetAlive(false);
        SwitchState(GameState.DEAD);
    }

    public void SwitchState(GameState newState)
    {
        currentUI.SetActive(false);
        gameState = newState;
        currentUI.SetActive(true);
    }
}
