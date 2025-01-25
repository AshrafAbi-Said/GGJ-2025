using UnityEngine;

public class BubbleGO : MonoBehaviour
{
    [SerializeField] float bubbleSpeed = 5f;
    Rigidbody bubbleRb;
    Vector3 bubbleDirection;
    public float timeBeforePopping = 3;
    void Start()
    {
        bubbleRb = GetComponent<Rigidbody>();
        bubbleDirection = Vector3.up;
    }

    void Update()
    {
        if (timeBeforePopping > 0)
        {
            FloatBubble();
            timeBeforePopping -= Time.deltaTime;
        }
        else
        {
            transform.DetachChildren();
            Destroy(gameObject);
        }
    }

    public void FloatBubble()
    {
        bubbleRb.linearVelocity = bubbleDirection * bubbleSpeed;
    }
}
