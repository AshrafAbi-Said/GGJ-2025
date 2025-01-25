using UnityEngine;
using UnityEngine.InputSystem;

public class CrabCharacterController : MonoBehaviour
{
    //[SerializeField] float speed = 10f;
    //[SerializeField] float jumpPower = 10f;
    //Rigidbody characterRb;
    //void Start()
    //{
    //    characterRb = GetComponent<Rigidbody>();
    //}

    //void Update()
    //{
    //    Move();
    //}


    //private void Move()
    //{
    //    characterRb.linearVelocity = (Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward + new Vector3(0, characterRb.linearVelocity.y, 0)).normalized * speed;
    //    Debug.Log("Velocity " + characterRb.linearVelocity);

    //}
    //void Jump()
    //{

    //}
    [SerializeField] private float playerForwardForce;
    [SerializeField] private float maxPlayerSpeedGrounded;
    [SerializeField] private float maxPlayerSpeedFalling;
    [SerializeField] private float groundDrag;
    [SerializeField] private float airDrag;
    [SerializeField] private float playerHeight;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float throwHeight;
    [SerializeField] private float weight;

    private Vector3 moveDir;
    //private float jumpVal;
    private Rigidbody rb;
    private float timeSinceFalling;

    private bool groundedPlayer;
    private float gravityValue = Physics.gravity.y;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible= false;

        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = maxPlayerSpeedGrounded;

        timeSinceFalling = 0;
    }

    void Update()
    {
        //transform.Translate(new Vector3(moveDir.x, jumpVal, moveDir.y) * Time.deltaTime * playerSpeed);
    }

    private void FixedUpdate()
    {
        transform.forward = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z).normalized;


        groundedPlayer = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f, LayerMask.GetMask("Ground"));

        if (groundedPlayer)
        {
            timeSinceFalling = 0;
            rb.linearDamping = groundDrag;
            rb.maxLinearVelocity = maxPlayerSpeedGrounded;
        }
        else
        {
            timeSinceFalling += Time.deltaTime;

            rb.linearDamping = airDrag;
            if (timeSinceFalling >= 3)
            {
                rb.maxLinearVelocity = maxPlayerSpeedFalling;
            }

        }


        moveDir = transform.forward * Input.GetAxisRaw("Vertical") + transform.right * Input.GetAxisRaw("Horizontal");
        rb.AddForce(moveDir * playerForwardForce, ForceMode.Force);

        Debug.Log(rb.linearVelocity.magnitude);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bubble"))
        {
            //GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = (collision.transform);

        }
        else if (collision.gameObject.tag == "Item")
        {
            Debug.Log("PICKED UP");
            weight += collision.transform.GetComponent<ItemGO>().itemWeight;
            collision.transform.SetParent(transform);
            collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            collision.transform.position = transform.position + Vector3.up;
            collision.transform.GetComponent<ItemGO>().isGrabbed = true;
        }

        //if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        //{
        //    groundedPlayer = true;
        //}
    }
    private void OnCollisionStay(Collision collision)
    {
        //Debug.Log("Stayed with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bubble"))
        {
            //GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = (collision.transform);

        }
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //    {
    //        Debug.Log("Exited " + collision.gameObject.name);
    //        groundedPlayer = false;
    //    }
    //}

    public void Move(InputAction.CallbackContext context)
    {
        //Vector2 inputs = context.ReadValue<Vector2>().normalized;
        //moveDir = transform.forward * inputs.y + transform.right * inputs.x;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        //Jump cancelation
        rb.linearDamping = 0;
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

            item.GetComponent<ItemGO>().isGrabbed = false;
            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            item.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, Mathf.Sqrt(jumpHeight * -2.0f * gravityValue), 0);
        }
    }
}
