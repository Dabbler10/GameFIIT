using System.Collections.Generic;
using UnityEngine;
public class GhostSpawn: MonoBehaviour
{
    private float spawnRadius = 7f;
    private int maxSpawnNumber = 3;
    public IEnumerable<GameObject> SpawnObjects(GameObject prefabToSpawn, Vector3 centerOfSpawn)
    {
        int objectCount = Random.Range(2, maxSpawnNumber);
        for (int i = 0; i < objectCount; i++)
        {
            var randomPosition = Random.insideUnitCircle * spawnRadius;
            var spawnPosition = centerOfSpawn + new Vector3(randomPosition.x,randomPosition.y,0);
            var ghost = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
            ghost.SetActive(true);
            var players = GameObject.FindGameObjectsWithTag("Player");
            if (players.Length == 2)
            {
                ghost.GetComponent<Ghost>().player1 = players[0].transform;
                ghost.GetComponent<Ghost>().player2 = players[1].transform;
            }
            yield return ghost;
        }
    }
}