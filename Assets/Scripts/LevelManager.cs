using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<ItemGO> itemsToAddToLevel;
    [SerializeField] ItemSpawner itemSpawner;
    [SerializeField] List<ItemGO.MaterialType> itemNeeded;
    [SerializeField] GameObject winCam;
    [SerializeField] GameObject player;
    [SerializeField] GameObject roof;
    [SerializeField] GameObject canvas;

    int shellCount = 0;
    int canCount = 0;
    int glassCount = 0;
    int plasticCount = 0;
    int chipsCount = 0;

    void Start()
    {
        winCam.SetActive(false);

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

        for(int i=0;i<itemsToAddToLevel.Count;i++)
        {
            AddItemNeeded(itemsToAddToLevel[i]);
            Debug.Log("added item " + itemNeeded.ToString());
            CheckItemCount(itemsToAddToLevel[i].matType);
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

            StartCoroutine(Win());
        }
    }

    private IEnumerator Win()
    {
        canvas.SetActive(false);
        player.transform.GetChild(1).gameObject.SetActive(false);
        player.transform.GetChild(2).gameObject.SetActive(false);
        winCam.SetActive(true);

        yield return new WaitForSeconds(3);

        roof.SetActive(true);
    }

    private void CheckItemCount(ItemGO.MaterialType matType)
    {
        switch (matType)
        {
            case ItemGO.MaterialType.BagOfChips:
                chipsCount++;
                LevelCanvasController.instance.chipsText.text = chipsCount.ToString();
                break;
            case ItemGO.MaterialType.Can:
                canCount++;
                LevelCanvasController.instance.canText.text = canCount.ToString();
                break;
            case ItemGO.MaterialType.GlassBottle:
                glassCount++;
                LevelCanvasController.instance.glassText.text = glassCount.ToString();
                break;
            case ItemGO.MaterialType.PlasticBottle:
                plasticCount++;
                LevelCanvasController.instance.plasticText.text = plasticCount.ToString();
                break;
            case ItemGO.MaterialType.Shell:
                shellCount++;
                LevelCanvasController.instance.shellText.text = shellCount.ToString();
                break;
            default:
                break;
        }
        Debug.Log("material in check count is " + matType);
    }
}
