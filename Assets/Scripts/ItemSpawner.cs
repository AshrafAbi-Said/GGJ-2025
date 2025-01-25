using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] ItemGO lightItem;
    [SerializeField] ItemGO mediumItem;
    [SerializeField] ItemGO heavyItem;
    [SerializeField] List<SpawnPosition> spawnPositions;
    [SerializeField] List<ItemGO> lightItemsList;
    [SerializeField] List<ItemGO> mediumItemsList;
    [SerializeField] List<ItemGO> heavyItemsList;
    [SerializeField] int lightItemCount = 5;
    [SerializeField] int mediumItemCount = 5;
    [SerializeField] int heavyItemCount = 5;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lightItemsList = new List<ItemGO>();
        mediumItemsList = new List<ItemGO>();
        heavyItemsList = new List<ItemGO>();

        SpawnItem(lightItemsList, lightItemCount, lightItem);
        SpawnItem(mediumItemsList, mediumItemCount, mediumItem);
        SpawnItem(heavyItemsList, heavyItemCount, heavyItem);
    }

    private void SpawnItem(List<ItemGO> listOfItems, int numOfItems, ItemGO itemToAdd)
    {
        for(int i = 0; i < numOfItems; i++)
        {
            Debug.Log("Spawning items " + itemToAdd.name);
            SpawnPosition spawnPosition = FindEmptySpot();
            var itemCreated = Instantiate(itemToAdd, spawnPosition.transform.position, spawnPosition.transform.rotation);
            spawnPosition.itemHeld = itemToAdd;
            listOfItems.Add(itemCreated);

        }
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
