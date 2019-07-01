using UnityEngine;
using System.Collections;


[RequireComponent(typeof(PigSound))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class PigController : MonoBehaviour {


    [Range(1f, 100f)]
    public float force = 20;

    [SerializeField]
    float rotateSpeed = 3.0F;

    [SerializeField]
    ThirdPersonCamera.Follow cameraFollow;

    [SerializeField]
    [Range(0.5f,2.5f)]
    float maxVelocityMagnitude = 1f;

    [SerializeField]
    public float velMag;//for read


    Animator animator;
    Rigidbody rb;
    PigSound soundController;


    public bool isGrounded;

    public enum RotationMode {
        Keyboard, Joystick
    }

    public RotationMode rotationMode;


    Vector3 moveForce = Vector3.zero;
    public bool IsIdle { get; private set; }

    bool isIdleAniCoroutineRunning;
    float dist2Ground;


	void Start ()
	{
        dist2Ground = GetComponent<Collider>().bounds.extents.y;
		rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        soundController = GetComponent<PigSound>();
	}

	void FixedUpdate ()
	{

        //isGrounded = CheckGrounded();

        //transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        if(rotationMode==RotationMode.Keyboard){
            transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        }else if(rotationMode==RotationMode.Joystick){
            transform.Rotate(0, Input.GetAxis("JoyX") * rotateSpeed, 0);
        }
       

        moveForce = new Vector3(0, 0, Input.GetAxis("Vertical"));

        moveForce = transform.TransformDirection(moveForce);
        moveForce *= force;

        velMag = rb.velocity.magnitude;

        if(rb.velocity.magnitude < maxVelocityMagnitude && moveForce.magnitude > 0){
            rb.AddForce(moveForce);
            soundController.Oink();
        }


        //CheckGrounded();

        //if (moveForce.magnitude > 1 )
        if(rb.velocity.magnitude > 0.2 || Input.GetAxis("JoyX") > 0f)
        {
            animator.SetInteger("WalkSpeed", 1);
            IsIdle = false;
        }
        else
        {
            IsIdle = true;
            animator.SetInteger("WalkSpeed", 0);
            if(!isIdleAniCoroutineRunning) StartCoroutine(PlayIdleAnimations());
        }

       
	}

    public bool CheckGrounded(){
        return Physics.Raycast(transform.position, Vector3.down, dist2Ground);
    }

    IEnumerator PlayIdleAnimations(){
        isIdleAniCoroutineRunning = true;

        if(!IsIdle)yield return new WaitForSeconds(10f);

        //Debug.Log("look around animation");
        animator.SetTrigger("IdleLookAround");
        yield return new WaitForSeconds(12f);

        isIdleAniCoroutineRunning = false;

    }




}