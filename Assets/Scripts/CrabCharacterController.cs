using UnityEngine;
using UnityEngine.InputSystem;

public class CrabCharacterController : MonoBehaviour
{
    [SerializeField] private float playerForwardForce;
    [SerializeField] private float maxPlayerSpeedGrounded;
    [SerializeField] private float playerHeight;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float throwHeight;
    [SerializeField] private float weight;

    [SerializeField] private Transform playerFollower;

    private Vector3 moveDir;
    //private float jumpVal;
    private Rigidbody rb;

    private bool groundedPlayer;
    private float gravityValue = Physics.gravity.y;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //transform.Translate(new Vector3(moveDir.x, jumpVal, moveDir.y) * Time.deltaTime * playerSpeed);
    }

    private void FixedUpdate()
    {
        transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;


        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, LayerMask.GetMask("Ground"));

        //if (groundedPlayer)
        //{
        //    timeSinceFalling = 0;
        //    rb.linearDamping = groundDrag;
        //    rb.maxLinearVelocity = maxPlayerSpeedGrounded;
        //}
        //else
        //{
        //    timeSinceFalling += Time.deltaTime;

        //    rb.linearDamping = airDrag;
        //    //if (timeSinceFalling >= 3)
        //    {
        //        rb.maxLinearVelocity = maxPlayerSpeedFalling;
        //    }

        //}

        rb.linearVelocity = new Vector3(
            Mathf.Clamp(rb.linearVelocity.x, -maxPlayerSpeedGrounded, maxPlayerSpeedGrounded),
            rb.linearVelocity.y,
            Mathf.Clamp(rb.linearVelocity.z, -maxPlayerSpeedGrounded, maxPlayerSpeedGrounded));

        Debug.Log("Crab weight: " + weight);


        moveDir = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
        rb.AddForce(moveDir * playerForwardForce, ForceMode.Force);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bubble"))
        {
            //GetComponent<Rigidbody>().isKinematic = true;
            //transform.parent = (collision.transform);
            collision.transform.GetComponent<BubbleGO>().AddSuddenWeight(weight); 
            collision.transform.GetComponent<BubbleGO>().AddWeightCarried(weight);


        }
        else if (collision.gameObject.tag == "Item")
        {
            Debug.Log("PICKED UP");
            if(!collision.transform.GetComponent<ItemGO>().isGrabbed)
            {
                weight += collision.transform.GetComponent<ItemGO>().itemWeight;
                collision.transform.GetComponent<ItemGO>().isGrabbed = true;
            }
            collision.transform.SetParent(transform);
            collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            collision.transform.position = transform.position + Vector3.up;
        }

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        //{
        //    groundedPlayer = true;
        //}
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    //Debug.Log("Stayed with " + collision.gameObject.name);
    //    if (collision.gameObject.CompareTag("Bubble"))
    //    {
    //        //GetComponent<Rigidbody>().isKinematic = true;
    //        //transform.parent = (collision.transform);

    //    }
    //}
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            Debug.Log("Exited " + collision.gameObject.name);
            collision.transform.GetComponent<BubbleGO>().AddWeightCarried(-weight);
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        //Vector2 inputs = context.ReadValue<Vector2>().normalized;
        //moveDir = transform.forward * inputs.y + transform.right * inputs.x;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        //Jump cancelation
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);

        Debug.Log("Try Jump");
        if (context.started && groundedPlayer)
        {
            Debug.Log("Jump");
            rb.AddForce(transform.up * jumpHeight, ForceMode.Impulse);
            //groundedPlayer = false;
            //jumpVal = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);

        }
    }

    public void Drop(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            GameObject item = transform.GetComponentInChildren<ItemGO>().gameObject;
            if (item == null)
                return;

            weight -= item.GetComponent<ItemGO>().itemWeight;
            item.GetComponent<ItemGO>().isGrabbed = false;
            item.transform.position = transform.position + transform.forward * 1.5f;
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            item.transform.SetParent(null);
        }
    }

    public void Throw (InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Throw");

            GameObject item = transform.GetComponentInChildren<ItemGO>().gameObject;
            if (item == null)
                return;

            weight -= item.GetComponent<ItemGO>().itemWeight;
            item.GetComponent<ItemGO>().isGrabbed = false;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            item.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, Mathf.Sqrt(throwHeight * -2.0f * gravityValue), 0);
        }
    }
}
