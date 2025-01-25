using UnityEngine;

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

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);
        //Debug.Log("move is " + move);
        //if (move != Vector3.zero)
        //{
        //    gameObject.transform.forward = move;
        //}

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * gravityValue);
        }

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
}
