using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class PlayerMovement : MonoBehaviour
{
    //Input Speed
    public float movementSpeed = 5.0f;
    public float rotationSpeed = 200.0f;

    //JumpSettings
    [SerializeField]
    private float jumpForce = 4f;
    public Rigidbody playerRB;

    //AnimatorComponents
    const string STATE_ON_THE_GROUND = "IsOnTheGround";
    const string STATE_JUMPING = "Jumping";
    const string STATE_KICKING = "Kick";
    const string STATE_CLONE = "Clone";
    const string STATE_SLIDE = "Slide";

    //RayCast
    
    public GameObject rayCastStartPosition;
    [SerializeField]
    private float raySize = 1.0f;

    public LayerMask groundMask;
    public bool isTouchingTheGround;

    private Animator playerAnim;
    public float x, y;

    //ClonePrefab
    public GameObject clonePrefab;
   
    //GameObjectToSpawnAt
    public Transform spawnPoint;


   
    void Start()
    {
        playerAnim = GetComponent<Animator>();

        playerAnim.SetBool(STATE_JUMPING, false);
        playerAnim.SetBool(STATE_ON_THE_GROUND, true);
        playerAnim.SetBool(STATE_KICKING, false);
        playerAnim.SetBool(STATE_CLONE, false);
        playerAnim.SetBool(STATE_SLIDE, false);

    }

    private void Awake()
    {
        playerRB = GetComponent<Rigidbody>();
    }

   


    void Update()
    {
        Walk();
            
        //Jump
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        //AnimationStateShift
        playerAnim.SetBool(STATE_JUMPING, IsTouchingTheGround());
        playerAnim.SetBool(STATE_ON_THE_GROUND, IsTouchingTheGround());

        //RayCastDraw
        Debug.DrawRay(rayCastStartPosition.transform.position, Vector2.down * raySize, Color.black);

        //Kick
        if (Input.GetKeyDown(KeyCode.Q))
            Kick();

        //Clone
        if (Input.GetKeyDown(KeyCode.E))
            Clone();

        //Slide
        if (Input.GetKeyDown(KeyCode.LeftShift))
            Slide();

       


    }

    private void Walk()
    {
        
            x = Input.GetAxis("Horizontal");
            y = Input.GetAxis("Vertical");

            transform.Rotate(0, x * Time.deltaTime * rotationSpeed, 0);
            transform.Translate(0, 0, y * Time.deltaTime * movementSpeed);

            playerAnim.SetFloat("VelX", x);
            playerAnim.SetFloat("VelY", y);  
    }


    private void Jump()
    {
        if (IsTouchingTheGround())
            playerRB.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
    }

    private void Kick()
    {
        this.gameObject.GetComponent<Animator>().Play("Kick");
        playerAnim.SetBool(STATE_KICKING, false);      
    }

    private void Clone()
    {
        this.gameObject.GetComponent<Animator>().Play("Clone");
        this.playerAnim.SetBool(STATE_CLONE, false);

        GameObject clone = Instantiate(clonePrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private void Slide()
    {
        this.gameObject.GetComponent<Animator>().Play("Slide");
        playerAnim.SetBool(STATE_SLIDE, false);

    }

    private bool IsTouchingTheGround()
    {
        if(Physics.Raycast(rayCastStartPosition.transform.position, Vector2.down, raySize, groundMask))
        {
            isTouchingTheGround = true;
            playerAnim.speed = 1.0f;
            return true;
        }

        else
        {
            isTouchingTheGround = false;
            return false;
        }
    }
}
