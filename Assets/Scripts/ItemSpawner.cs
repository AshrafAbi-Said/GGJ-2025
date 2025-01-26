using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] List<SpawnPosition> spawnPositions;

    public void SpawnItem(ItemGO itemToAdd)
    {
            Debug.Log("Spawning items " + itemToAdd.name);
            SpawnPosition spawnPosition = FindEmptySpot();
            Instantiate(itemToAdd, spawnPosition.transform.position, spawnPosition.transform.rotation);
            spawnPosition.itemHeld = itemToAdd;
    }

    private SpawnPosition FindEmptySpot()
    {
        SpawnPosition spawnPosition = null;
        while(spawnPosition == null)
        {
            SpawnPosition spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count)];
            if (spawnPos.itemHeld == null)
                spawnPosition = spawnPos;
        }
        return spawnPosition;
    }
}
