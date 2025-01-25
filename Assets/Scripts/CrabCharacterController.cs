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
    private Vector2 moveDir;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = Physics.gravity.y;

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        controller.Move(new Vector3(moveDir.x, 0, moveDir.y) * Time.deltaTime * playerSpeed);

        // Makes the player jump
        //if (Input.GetButtonDown("Jump") && groundedPlayer)
        //{
        //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        //}

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
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
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Stayed with " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Bubble"))
        {
            //GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = (collision.transform);

        }
    }
    //private void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Bubble"))
    //    {
    //        Debug.Log("Exited " + collision.gameObject.name);
    //        transform.parent = null;
    //    }
    //}

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
            //groundedPlayer = false;
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }
    }
}
