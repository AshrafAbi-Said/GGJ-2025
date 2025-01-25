using UnityEngine;

public class BubbleGO : MonoBehaviour
{
    [SerializeField] float bubbleSpeed = 5f;
    Rigidbody bubbleRb;
    Vector3 bubbleDirection;
    public float timeBeforePopping = 3;
    float totalTimeBeforePop;
    [SerializeField] float weightMultiplier;
    private float weightCarried;
    [SerializeField] Material bubbleMaterial;
    [SerializeField] GameObject particlesGO;
    void Start()
    {
        bubbleRb = GetComponent<Rigidbody>();
        bubbleDirection = Vector3.up;
        bubbleMaterial = GetComponent<Renderer>().material;
        Debug.Log("Bubble renderer is " + bubbleMaterial.name);
        Debug.Log("Bubble input is " + bubbleMaterial.GetColor("_External_Color"));
        totalTimeBeforePop = timeBeforePopping;
    }

    void Update()
    {
        if (timeBeforePopping > 0)
        {
            FloatBubble();
            timeBeforePopping -= (Time.deltaTime + weightMultiplier*weightCarried);
            Debug.Log("Bubble input before is " + bubbleMaterial.GetColor("_External_Color"));
            Debug.Log("Bubble input before is " + bubbleMaterial.GetColor("_Internal_Color"));
            float colorVal = (totalTimeBeforePop - timeBeforePopping)/totalTimeBeforePop;
            bubbleMaterial.SetColor("_External_Color", Color.Lerp(Color.green, Color.red, colorVal));
            bubbleMaterial.SetColor("_Internal_Color", Color.Lerp(Color.blue, Color.red, colorVal));
            Debug.Log("Bubble input after is " + bubbleMaterial.GetColor("_External_Color"));
            Debug.Log("Bubble input after is " + bubbleMaterial.GetColor("_Internal_Color"));

        }
        else
        {
            particlesGO.SetActive(true);
            Destroy(particlesGO, 2);
            transform.DetachChildren();
            Destroy(gameObject);
        }
    }

    public void FloatBubble()
    {
        bubbleRb.linearVelocity = bubbleDirection * bubbleSpeed;
    }
    public void SetWeightCarried(float weight)
    {
        weightCarried = weight;
    }
    public void AddSuddenWeight(float addedWeight)
    {
        timeBeforePopping -= addedWeight/10;
    }
}
