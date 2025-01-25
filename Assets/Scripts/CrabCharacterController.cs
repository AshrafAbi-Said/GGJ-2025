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
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpHeight;
    [SerializeField] private float throwHeight;

    private Vector2 moveDir;
    private float jumpVal;

    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float gravityValue = Physics.gravity.y;

    private void Start()
    {

    }

    void Update()
    {

        if (groundedPlayer)
        {
            jumpVal = 0;
        }
        else
        {
            jumpVal += gravityValue * Time.deltaTime;

        }

        transform.Translate(new Vector3(moveDir.x, jumpVal, moveDir.y) * Time.deltaTime * playerSpeed);
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
            collision.transform.SetParent(transform);
            collision.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            collision.transform.position = transform.position + Vector3.up;
        }

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground")) 
        {
            groundedPlayer = true;
        }
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
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("Exited " + collision.gameObject.name);
            groundedPlayer = false;
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveDir = context.ReadValue<Vector2>().normalized;
    }
    
    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("Try Jump");
        if (context.started && groundedPlayer)
        {
            Debug.Log("Jump");
            groundedPlayer = false;
            jumpVal = Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);

        }
    }

    public void Drop(InputAction.CallbackContext context) 
    {
        if (context.started)
        {
            GameObject item = transform.GetComponentInChildren<ItemGO>().gameObject;
            if (item == null)
                return;

            item.transform.position = transform.position + transform.forward * 1.5f;
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

            item.transform.SetParent(null);
            item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            item.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, Mathf.Sqrt(jumpHeight * -2.0f * gravityValue), 0);
        }
    }
}
