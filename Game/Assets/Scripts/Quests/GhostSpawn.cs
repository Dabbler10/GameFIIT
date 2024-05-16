using System.Collections.Generic;
using UnityEngine;
public class GhostSpawn: MonoBehaviour
{
    private float spawnRadius = 5f;
    private int maxSpawnNumber = 4;
    public IEnumerable<GameObject> SpawnObjects(GameObject prefabToSpawn, Vector3 centerOfSpawn)
    {
        int objectCount = Random.Range(1, maxSpawnNumber);
        for (int i = 0; i < objectCount; i++)
        {
            var randomPosition = Random.insideUnitCircle * spawnRadius;
            var spawnPosition = centerOfSpawn + new Vector3(randomPosition.x,randomPosition.y,0);
            yield return Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }
}