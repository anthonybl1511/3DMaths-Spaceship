using UnityEngine;
using UnityEngine.UIElements;

public class RotationController : MonoBehaviour
{
    private float rotationAnglezTilt = 40f;
    private float rotationAngley = 0.1f;
    private float rotationAnglex = 0.05f;
    private float rotationAnglexTilt = 15f;
    private float speed = 40.0f;

    private float horizontalInput;
    private float verticalInput;

    void Update()
    {
        if(verticalInput == 0)
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }
        
        if(horizontalInput == 0 )
        {
            verticalInput = Input.GetAxis("Vertical");
        }

        Quaternion xRotation = CalculateRotationQuaternion(rotationAnglex * verticalInput, Vector3.right);
        Quaternion xRotationTilt = CalculateRotationQuaternion(rotationAnglexTilt * verticalInput, Vector3.right);
        Quaternion yRotation = CalculateRotationQuaternion(rotationAngley * horizontalInput, Vector3.up);
        Quaternion zRotationTilt = CalculateRotationQuaternion(rotationAnglezTilt * -horizontalInput, Vector3.forward);
        transform.localRotation = zRotationTilt;
        if(verticalInput != 0)
        {
            transform.localRotation = xRotationTilt;
        }
        transform.parent.rotation *= yRotation;
        transform.parent.rotation *= xRotation;

        transform.parent.position = transform.position + transform.forward * Time.deltaTime * speed;
    }

    Quaternion CalculateRotationQuaternion(float angle, Vector3 axis)
    {
        // angle en radians
        float angleInRadians = angle * Mathf.Deg2Rad;

        // quaternion de rotation
        Quaternion rotationQuaternion = new Quaternion(
            axis.x * Mathf.Sin(angleInRadians / 2), // ici ca vaut 0
            axis.y * Mathf.Sin(angleInRadians / 2), // ici ca vaut 0 pour l'axe z
            axis.z * Mathf.Sin(angleInRadians / 2), // ici ca vaut 0 pour l'axe y
            Mathf.Cos(angleInRadians / 2)
        );

        return rotationQuaternion;
    }
}