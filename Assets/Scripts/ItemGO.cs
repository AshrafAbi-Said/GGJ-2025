using UnityEngine;

public class ItemGO : MonoBehaviour
{
    public MaterialType matType;
    public float spawnChance;
    public bool isGrabbed;
    [SerializeField] GameObject VFXItem;
    [HideInInspector] public float itemWeight;
    
    public bool fallThroughBubbles;

    private LevelManager levelManager;

    public enum MaterialType
    {
        Shell,
        Can,
        GlassBottle,
        PlasticBottle,
        BagOfChips,
        LGDGamersOnly
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetType(matType);
        fallThroughBubbles = false;
        isGrabbed = false;

        levelManager = GameObject.Find("Level Manager").GetComponent<LevelManager>();
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
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                VFXItem.SetActive(true);
            }
        }

        //if (!isGrabbed && collision.gameObject.tag == "Building")
        //{
        //    levelManager.RemoveItemNeeded(this);
        //    collision.gameObject.GetComponent<BuildingGO>().collectItem(this);
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isGrabbed && other.tag == "Building")
        {
            levelManager.RemoveItemNeeded(this);
            other.GetComponent<BuildingGO>().collectItem(this);

            Destroy(gameObject);
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            VFXItem.SetActive(false);
        }
    }

    public void SetType(MaterialType type)
    {
        if (type == MaterialType.Can ||
            type == MaterialType.BagOfChips)
        {
            //set light weight
            itemWeight = 2;
        }
        else if(type == MaterialType.Shell ||
            type == MaterialType.PlasticBottle) 
        {
            //set medium weight
            itemWeight = 5;
        }
        else if(type == MaterialType.GlassBottle ||
            type == MaterialType.LGDGamersOnly) 
        {
            //set heavy weight
            itemWeight = 7;
        }
    }
}
