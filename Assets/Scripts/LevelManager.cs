using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<ItemGO> itemsToAddToLevel;
    [SerializeField] ItemSpawner itemSpawner;
    [SerializeField] int itemsNeededPerLevel = 3;
    [SerializeField] List<ItemGO.MaterialType> itemNeeded;

    void Start()
    {
        List<ItemGO> randomItemsList = new List<ItemGO>();
        while(itemsToAddToLevel.Count>0)
        {
            var randomItem = itemsToAddToLevel[Random.Range(0, itemsToAddToLevel.Count)];
            randomItemsList.Add(randomItem);
            itemsToAddToLevel.Remove(randomItem);
            
        }

        itemsToAddToLevel = randomItemsList;
        
        foreach (ItemGO item in itemsToAddToLevel)
            itemSpawner.SpawnItem(item);

        for(int i=0;i<itemsNeededPerLevel;i++)
        {
            AddItemNeeded(itemsToAddToLevel[i]);
            Debug.Log("added item " + itemNeeded);
        }
    }

    public void AddItemNeeded(ItemGO item)
    {
        itemNeeded.Add(item.matType);
    }

    public void RemoveItemNeeded(ItemGO item)
    {
        Debug.Log("Collected in level manager");
        itemNeeded.Remove(item.matType);
        CheckIfWon();
    }

    public List<ItemGO.MaterialType> GetItemsNeeded()
    {
        return itemNeeded;
    }

    private void CheckIfWon()
    {
        if(itemNeeded.Count == 0)
        {
            Debug.Log("You Win!");
        }
    }
}
