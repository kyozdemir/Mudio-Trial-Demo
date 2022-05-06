using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : CustomBehaviour
{
    public Enemy EnemyPrefab;

    public Queue<Enemy> Enemies = new Queue<Enemy>();
    private int mQueueLength = 50;

    public Enemy SpawnEnemy(Vector3 spawnPos)
    {
        Enemy enemy;

        if (Enemies.Count >= mQueueLength)
        {
            enemy = Enemies.Dequeue();
        }
        else
        {
            enemy = Instantiate(EnemyPrefab,spawnPos, Quaternion.identity, transform);
            enemy.Initialize(GameManager);
        }

        enemy.Activate(GameManager.WaveManager.GetCurrentEnemySpeed());
        return enemy;
    }

    public void DespawnEnemy(Enemy enemy)
    {
        enemy.Deactivate();
        Enemies.Enqueue(enemy);
    }
}
