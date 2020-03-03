using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject camPivot;

    private const float YMin = -2.0f;
    private const float YMax = 30.0f;

    public Transform lookAt;
    public Transform camTransform;

    private Camera cam;

    public Vector3 distance;
    Vector3 camPosition;
    Vector3 desPos;
    Vector3 camMask;
    public float sensivityX;
    public float sensivityY;

    private float DistanceAway;
    public float DistanceUp = -2;                    //how high the camera is above the player
    public float rotateAround = 70f;
    float cameraHeight = 55f;
    float cameraPan = 0f;

    private float currentX;
    private float currentY;

    float camYPos;

    public LayerMask CamOcclusion;

    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        
        camYPos = gameObject.transform.position.y;
    }

    void Update()
    {
        currentX += Input.GetAxis("Mouse X") * sensivityX;
        currentY += Input.GetAxis("Mouse Y") * sensivityY;
        currentY = Mathf.Clamp(currentY, YMin, YMax);

        Quaternion rotation = Quaternion.Euler(cameraHeight, rotateAround, cameraPan);
        Vector3 vectorMask = Vector3.one;
        Vector3 rotateVector = rotation * vectorMask;

        Vector3 targetOffset = new Vector3(lookAt.position.x, (lookAt.position.y + 2f), lookAt.position.z);
        camPosition = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;
        camMask = targetOffset + Vector3.up * DistanceUp - rotateVector * DistanceAway;

        occludeRay(ref targetOffset);

        //Vector3 desPos = new Vector3(gameObject.transform.position.x, camYPos, gameObject.transform.position.z);
        desPos = new Vector3(0f, camYPos, 0f);

        Quaternion desRot = Quaternion.Euler(0f, gameObject.transform.eulerAngles.y, 0f);
        camPivot.transform.position = desPos;
        camPivot.transform.rotation = desRot;
        
    }

    void FixedUpdate()
    {
        Quaternion rotation = Quaternion.Euler(-currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * distance;
        camTransform.LookAt(lookAt.position);
    }

    void occludeRay(ref Vector3 targetFollow)
    {
        #region prevent wall clipping
        //declare a new raycast hit.
        RaycastHit wallHit = new RaycastHit();
        //linecast from your player (targetFollow) to your cameras mask (camMask) to find collisions.
        if (Physics.Linecast(targetFollow, camMask, out wallHit, CamOcclusion))
        {
            desPos = new Vector3(wallHit.point.x + wallHit.normal.x * 0.5f, camPosition.y, wallHit.point.z + wallHit.normal.z * 0.5f);
        }
        #endregion
    }

}
