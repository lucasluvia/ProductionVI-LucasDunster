using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class newPlayerBehaviour : MonoBehaviour
{
    [Header("Player Variables")]
    public bool isGrav;
    public bool isRunning;
    public bool inDetector;

    Rigidbody body;
    CanvasController canvas;

    [Header("Movement Properties")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float runSpeed = 10.0f;
    [SerializeField] private float gravityStrength = -30.0f;
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private Vector3 velocity;

    [Header("Mouse Looking")]
    public GameObject followTarget;
    public float aimSensitivity = 1.0f;

    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector3.zero;

    [Header("Gravity Control")]
    [SerializeField] private float viewDistance;
    public bool inGravityState;
    public bool inBufferUpdate;

    [Header("Ground Detection Properties")]
    public Transform groundCheck;
    public float groundRadius = 0.5f;
    public LayerMask groundMask;
    public bool isGrounded;

    private int bufferCounter = 0;

    void Start()
    {
        canvas = GameObject.FindWithTag("Canvas").GetComponent<CanvasController>();
        body = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!inBufferUpdate)
        {
            doGroundedUpdate();
            doGravityUpdate();
        }
        else
        {
            BufferUpdate();
        }

    }


    private void doGroundedUpdate()
    {
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.x * aimSensitivity, Vector3.up);
        followTarget.transform.rotation *= Quaternion.AngleAxis(lookInput.y * aimSensitivity, Vector3.left);

        var angles = followTarget.transform.localEulerAngles;
        angles.z = 0;

        var angle = followTarget.transform.localEulerAngles.x;

        if (angle > 180 && angle < 300)
        {
            angles.x = 300;
        }
        else if (angle < 180 && angle > 40)
        {
            angles.x = 40;
        }

        followTarget.transform.localEulerAngles = angles;

        transform.rotation = Quaternion.Euler(0, followTarget.transform.rotation.eulerAngles.y, 0);
        followTarget.transform.localEulerAngles = new Vector3(angles.x, 0, 0);


        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask);

        if (!isGrounded)
        {
            velocity.y = 1.0f * gravityStrength;
            inputVector = Vector3.zero;
        }
        else
        {
            velocity.y = 0.0f;
        }

        if (!(inputVector.magnitude > 0)) moveDirection = Vector3.zero;

        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x + transform.up * velocity.y;
        float currentSpeed = isRunning ? runSpeed : walkSpeed;

        Vector3 movementDirection = moveDirection * (currentSpeed * Time.deltaTime);

        transform.position += movementDirection;

        if (transform.rotation.x > 0.6)
        {
            transform.rotation = Quaternion.Euler(new Vector3(70.0f, 0, 0));
        }
        if (transform.rotation.x < -0.6)
        {
            transform.rotation = Quaternion.Euler(new Vector3(-70.0f, 0, 0));
        }
    }

    private void doGravityUpdate()
    {
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * viewDistance, Color.yellow);
        RaycastHit hitData;

        if (Physics.Raycast(ray, out hitData))
        {
            if(hitData.distance < viewDistance)
            {
                if (hitData.collider.tag == "Pingable")
                {
                    var other = hitData.transform.gameObject.GetComponent<WallController>();
                    if (other != null)
                    {
                        canvas.SetCrosshairState(true);
                        if (isGrav)
                        {
                            GameObject.Find("GameController").GetComponent<GameController>().Rotate(other.direction);
                            inBufferUpdate = true;
                            isGrav = false;
                        }
                    }
                    else
                        canvas.SetCrosshairState(false);
                }
                else
                    canvas.SetCrosshairState(false);
            }
            else
                canvas.SetCrosshairState(false);
        }
        else
            canvas.SetCrosshairState(false);
    }

    private void BufferUpdate()
    {
        if(bufferCounter != 20)
        {
            bufferCounter++;
        }
        else
        {
            inBufferUpdate = false;
            inGravityState = false;
            bufferCounter = 0;
        }

    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }
    
    public void OnGrav(InputValue value)
    {
        isGrav = value.isPressed;
    }

    public void OnRun(InputValue value)
    {
        isRunning = value.isPressed;
    }
    
    public void OnMouseLook(InputValue value)
    {
        if (Time.timeScale != 0)
        {
            lookInput = value.Get<Vector2>();
            float lookParam = Mathf.InverseLerp(40, 340, followTarget.transform.localEulerAngles.x);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Detector"))
        {
            if (other.GetComponent<DetectionComponent>())
            {
                other.GetComponent<DetectionComponent>().isPlayerHere = true;
            }
            inDetector = true;
            if(other.transform.parent.GetComponent<PatrolDetector>())
            {
                other.transform.parent.GetComponent<PatrolDetector>().pauseDetector = true;
            }
        }
        if(other.CompareTag("Exit"))
        {
            canvas.ShowWinScreen();
        }
        if(other.CompareTag("Enemy") || other.CompareTag("DeathPlane"))
        {
            canvas.ShowDeathScreen();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Detector"))
        {
            other.GetComponent<DetectionComponent>().isPlayerHere = false;
            inDetector = false;
            if (other.transform.parent.GetComponent<PatrolDetector>())
            {
                other.transform.parent.GetComponent<PatrolDetector>().pauseDetector = false;
            }
        }
    }



}
