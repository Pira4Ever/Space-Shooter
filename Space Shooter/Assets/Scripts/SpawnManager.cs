using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject[] powerUps;
    // Start is called before the first frame update
    void Start()
    {
        if(_enemyPrefab == null)
        {
            Debug.LogError("Enemy is null!");
        }
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }


    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 8f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        while(_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, -8f), 8f, 0);
            int randomPowerup = Random.Range(0, 2);
            Instantiate(powerUps[randomPowerup], postToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(7, 13));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}
