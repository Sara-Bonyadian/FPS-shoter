using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;


public class PlayerNetwork : NetworkBehaviour
{
    

    [SerializeField] private Transform spawendObjectPrefab;
    private Transform spawendObjectTransoform;
    public Camera camera;

    CharacterController controller;

    Animator anim;
    float xMove;
    float zMove;

    float speed = 10f;
    public float jumpHeight = 8f;

    public float gravity = -9.81f;
    public bool isGrounded;
    Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!IsOwner) return;
        if(IsOwner && camera.gameObject.activeInHierarchy == false)
        {
            camera.gameObject.SetActive(true);
        }

        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //    spawendObjectTransoform=Instantiate(spawendObjectPrefab);
        //    spawendObjectTransoform.GetComponent<NetworkObject>().Spawn(true);
        //};

        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    spawendObjectTransoform.GetComponent<NetworkObject>().Despawn(true);

        //    Destroy(spawendObjectTransoform.gameObject);
        //}

        //float horizInput = Input.GetAxis("Horizontal") * 10 * Time.deltaTime;
        //controller.Move(new Vector3(0, 0, horizInput));

        //anim.SetFloat("speed", horizInput);

        //Vector3 movDir = new Vector3(0, 0, 0);

        //if (Input.GetKey(KeyCode.W)) movDir.z = +1f;
        //if (Input.GetKey(KeyCode.S)) movDir.z = -1f;
        //if (Input.GetKey(KeyCode.A)) movDir.x = -1f;
        //if (Input.GetKey(KeyCode.D)) movDir.x = +1f;

        //float moveSpeed = 3f;
        //transform.position += movDir * moveSpeed * Time.deltaTime;

        isGrounded = Physics.CheckSphere(groundCheck.position, 20f, groundMask);
        
        if ( velocity.y < 0 && isGrounded)
        {
            velocity.y = -6f;
            anim.SetBool("jump", false);

        }

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");
        Vector3 move = transform.right * xMove + transform.forward * zMove;
        controller.Move(3 * move * speed * Time.deltaTime);

        anim.SetFloat("velx", Mathf.Abs(xMove));
        anim.SetFloat("velz", Mathf.Abs(zMove));

        if (Input.GetKeyDown(KeyCode.T) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -9f * gravity);
            anim.SetBool("jump", true);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

    }


}
