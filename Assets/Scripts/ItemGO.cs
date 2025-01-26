using UnityEngine;

public class ItemGO : MonoBehaviour
{
    public MaterialType matType;
    public float spawnChance;
    public bool isGrabbed;

    [HideInInspector] public float itemWeight;
    
    public bool fallThroughBubbles;

    public enum MaterialType
    {
        Shell,
        Clam,
        Seaweed,
        Can,
        GlassBottle,
        PlasticBottle,
        Treasure,
        Coral,
        BagOfChips,
        LGDGamersOnly
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetType(matType);
        fallThroughBubbles = false;
        isGrabbed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isGrabbed)
        {
            fallThroughBubbles = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(!isGrabbed && collision.gameObject.tag == "Bubble")
        {
            if (fallThroughBubbles)
            {
                Destroy(collision.gameObject);
            }
            else
            {
                collision.transform.GetComponent<BubbleGO>().AddWeightCarried(itemWeight);
                collision.transform.GetComponent<BubbleGO>().itemOnBubble = this;
            }

        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (!isGrabbed && collision.gameObject.tag == "Bubble" && !fallThroughBubbles)
        {
            fallThroughBubbles = true;
            collision.transform.GetComponent<BubbleGO>().AddWeightCarried(-itemWeight);
            //GetComponent<BoxCollider>().excludeLayers = LayerMask.GetMask("Ground");
        }
    }

    public void SetType(MaterialType type)
    {
        if (type == MaterialType.Seaweed ||
            type == MaterialType.Can ||
            type == MaterialType.BagOfChips)
        {
            //set light weight
            itemWeight = 2;
        }
        else if(type == MaterialType.Shell ||
            type == MaterialType.Clam ||
            type == MaterialType.PlasticBottle ||
            type == MaterialType.Coral) 
        {
            //set medium weight
            itemWeight = 5;
        }
        else if(type == MaterialType.GlassBottle ||
            type == MaterialType.Treasure ||
            type == MaterialType.LGDGamersOnly) 
        {
            //set heavy weight
            itemWeight = 7;
        }
    }
}
