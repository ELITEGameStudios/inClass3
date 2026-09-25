using UnityEngine;

public class RandomSpawner : MonoBehaviour, EnemySpawner
{
    public GameObject prefab;
    public int range;
    public void SpawnEnemies()
    {
        int i = 10;
        while (i < range)
        {
            GameObject newObj = Instantiate(prefab, Vector3.right * i, Quaternion.identity);
            GameManager.Instance.AddEnemy(newObj);
            i+= Random.Range(0, 10);
        }
    }
}
