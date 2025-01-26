using System.Collections.Generic;
using UnityEngine;

public class BuildingGO : MonoBehaviour
{
    private List<ItemGO> itemsCollected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        itemsCollected = new List<ItemGO>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void collectItem(ItemGO item)
    {
        itemsCollected.Add(item);
        item.GetComponent<Collider>().enabled = false;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Debug.Log("Collected " + item + "\nList is now: " + itemsCollected);
    }
}
