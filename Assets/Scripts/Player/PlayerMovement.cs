using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    public static bool inventoryActive = false;
    public static bool chestOpen = false;
    public Transform droppedItemTransform;

    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float xSensitivity = 100f; //Vertical
    [SerializeField] private float ySensitivity = 100f; //Horizontal
    [SerializeField] private float minRot = -90f;
    [SerializeField] private float maxRot = 30f;
    [SerializeField] private float gravity = -9.81f;
    private float speed;
    private float xRotation = 0f;
    public bool moving = false;
    public bool sprinting = false;
    public bool mountedToTurret = false;

    private CharacterController controller;
    private Transform camTransform;
    private Vector3 velocity;
    private Animator animator;

    public void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        animator = GetComponentInChildren<Animator>();
        camTransform = GetComponentInChildren<Camera>().transform;
        controller = GetComponent<CharacterController>();

    }

    private void Update()
    {
        ///Position
        //Input
        if (!PlayerInteraction.interactingWithWorkstation && !StateHandler.instance.gamePaused)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            if (x != 0 || z != 0) moving = true;
            else moving = false;

            if (moving)
            {
                if (!AudioManager.instance.IsPlaying("FOOTSTEP")) AudioManager.instance.PlaySound("FOOTSTEP");
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    sprinting = true;
                    animator.SetBool("SPRINTING", true);
                    speed = sprintSpeed;
                }
                else
                {
                    sprinting = false;
                    animator.SetBool("SPRINTING", false);
                    speed = walkSpeed;
                }
            }
            else
            {
                sprinting = false;
                AudioManager.instance.StopSound("FOOTSTEP");
                animator.SetBool("SPRINTING", false);
            }

            //Action
            if (!mountedToTurret)
            {
                Vector3 move = transform.right * x + transform.forward * z;
                controller.Move(move * speed * Time.deltaTime);
                velocity.y += gravity * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
            }

            ///Rotation
            if (!inventoryActive)
            {
                //Input
                float mouseX = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
                float mouseY = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;

                //Action
                xRotation -= mouseY;
                xRotation = Mathf.Clamp(xRotation, minRot, maxRot);
                camTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                transform.Rotate(Vector3.up * mouseX);

            }
        }

    }

    public void MountTurret()
    {
        PlayerAttack.instance.coolingDown = false;
        PlayerAttack.instance.coolDownCounter = 0f;
        if (mountedToTurret)
        {
            //Dismount
            PlayerAttack.instance.coolDownTime = 0.333f;
            mountedToTurret = false;
            transform.position = TurretItem.instance.playerStandPosition.position;
            transform.rotation = TurretItem.instance.playerStandPosition.rotation;
            camTransform.rotation = Quaternion.identity;
            ySensitivity = 100f;
            minRot = -90f;
            maxRot = 30f;
        }
        else
        {
            //Mount
            PlayerAttack.instance.coolDownTime = 0.1f;
            mountedToTurret = true;
            transform.position = TurretItem.instance.playerSitPosition.position;
            transform.rotation = TurretItem.instance.playerSitPosition.rotation;
            camTransform.rotation = Quaternion.identity;
            ySensitivity = 30f;
            minRot = -20f;
            maxRot = 20f;
        }
    }

}
