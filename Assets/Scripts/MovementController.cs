using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [SerializeField]
    float speed=5f;
    [SerializeField]
    float lookSens=3f;
    Rigidbody thisRigidBody;

    [SerializeField]
    Camera gameFpsCamera;

    private Vector3 movement=new Vector3(0f,0f,0f);
    private Vector3 rotationn=new Vector3(0f,0f,0f);
    float cameraUpAndDownrotation=0f;
    float currentCameraUpAndDownRotation=0f;

    // Start is called before the first frame update
    private void Awake() {
        thisRigidBody=GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //calculate movement veloctiy as a 3d vector
        float x_movement=Input.GetAxis("Horizontal");
        float z_movement=Input.GetAxis("Vertical");

        Vector3 _movement_horiz=transform.right*x_movement;
        Vector3 _movementVertical=transform.forward*z_movement;

        //final movement velocity
        Vector3 _movementVelocity=((_movement_horiz+_movementVertical).normalized*speed);
        Movee(_movementVelocity);

        //calculate 3d rotation for the player
        float _yRotation=Input.GetAxis("Mouse X");
        Vector3 _rotationVector=new Vector3(0,_yRotation*lookSens,0);

        //Apply rotation
        Rotate(_rotationVector);

        //camera rotation
        float _cameraUpRotation=Input.GetAxis("Mouse Y")*(lookSens);
        RotateCamera(_cameraUpRotation);



    }
    void RotateCamera(float rotation)
    {
        cameraUpAndDownrotation=rotation;        
    }
    void Rotate(Vector3 rotation)
    {
        rotationn=rotation;
    }
    void Movee(Vector3 velocity_mov)
    {
        movement=velocity_mov;
    }
    private void FixedUpdate() {
        //for physics
        if(movement!=Vector3.zero)
        {
            thisRigidBody.MovePosition(thisRigidBody.position+movement*Time.fixedDeltaTime);
        }
        thisRigidBody.MoveRotation(thisRigidBody.rotation*Quaternion.Euler(rotationn));
        currentCameraUpAndDownRotation-=cameraUpAndDownrotation;
        currentCameraUpAndDownRotation=Mathf.Clamp(currentCameraUpAndDownRotation,-85,65);
        gameFpsCamera.transform.localEulerAngles=new Vector3(currentCameraUpAndDownRotation,0f,0f);
    }
}
