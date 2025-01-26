using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<ItemGO> itemsToAddToLevel;
    [SerializeField] ItemSpawner itemSpawner;
    [SerializeField] int itemsNeededPerLevel = 3;
    [SerializeField] List<ItemGO> itemsNeeded;

    void Start()
    {
        foreach (ItemGO item in itemsToAddToLevel)
            itemSpawner.SpawnItem(item);
    }



}
