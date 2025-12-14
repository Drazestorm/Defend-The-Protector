using UnityEngine;

public class Enemy_Respawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] respawnPoints;
    [SerializeField] private float cooldown;
    [Space]
    [SerializeField] private float cooldownDecreaseRate = 0.5f;
    [SerializeField] private float cooldownCap = 0.7f;
    private float timer;

    private Transform player;
    private void Awake()
    {
        player = FindFirstObjectByType<Player>().transform;
    }
    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0)
        {
            timer = cooldown;
            CreateNewEnemy();

            cooldown = Mathf.Max(cooldownCap, cooldown - cooldownDecreaseRate);
        }
    }

    private void CreateNewEnemy()
    {
        int respawnPointIndex = Random.Range(0, respawnPoints.Length);
        Vector3 spawnPoint = respawnPoints[respawnPointIndex].position;

        GameObject newenemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

        if (newenemy.transform.position.x > player.position.x)
            newenemy.GetComponent<Enemy>().Flip();
        
    }
}
