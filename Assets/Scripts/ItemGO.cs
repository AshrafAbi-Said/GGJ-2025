using UnityEngine;

public class ItemGO : MonoBehaviour
{
    public MaterialType matType;

    private float itemWeight;
    [SerializeField] private bool fallThroughBubbles;

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
        fallThroughBubbles = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Bubble" && fallThroughBubbles)
    //    {
    //        collision.gameObject.SetActive(false);
    //    }
    //}

    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Bubble" && !fallThroughBubbles)
    //    {
    //        fallThroughBubbles = true;
    //        //GetComponent<BoxCollider>().excludeLayers = LayerMask.GetMask("Ground");
    //    }
    //}

    public void SetType(MaterialType type)
    {
        if (type == MaterialType.Seaweed ||
            type == MaterialType.Can ||
            type == MaterialType.BagOfChips)
        {
            //set light weight
        }
        else if(type == MaterialType.Shell ||
            type == MaterialType.Clam ||
            type == MaterialType.PlasticBottle ||
            type == MaterialType.Coral) 
        {
            //set medium weight
        }
        else if(type == MaterialType.GlassBottle ||
            type == MaterialType.Treasure ||
            type == MaterialType.LGDGamersOnly) 
        {
            //set heavy weight
        }
    }
}
