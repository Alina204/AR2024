using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // needed for TMP_texy
using UnityEngine.InputSystem; //needed for the Inputsystem
using UnityEngine.XR.ARFoundation; //needed for AR Raycast
using UnityEngine.XR.ARSubsystems; // needed for Trackbletypes


public class touchInput : MonoBehaviour
{
    [SerializeField] private TMP_Text debugtext; // Debug text object 
    [SerializeField] private GameObject ballprefab;// prefab to swan

    private ARRaycastManager arrcm; //Reference to the AR Raycast Manager
    [SerializeField]
    private ARCameraManager aRCameraManager;

    private List<ARRaycastHit> hits = new List<ARRaycastHit>(); // list to store ARRaycastlist
    TrackableType trackableTypes = TrackableType.PlaneWithinPolygon; // determine what kind of trakable to detect

    [SerializeField] private ARFaceManager arFaceManager;
    [SerializeField] private ARPlaneManager arPlaneManager;

    private void Start()
    {
        arrcm = GetComponent<ARRaycastManager>();
    }

    public void SingleTap(InputAction.CallbackContext ctx)
    {
        // check that the Input was completed
        if (ctx.phase == InputActionPhase.Performed)
        {
            var touchPos = ctx.ReadValue<Vector2>(); // read position of tap
            debugtext.text = touchPos.ToString(); // write to debugtext

            if (arrcm.Raycast(touchPos, hits, trackableTypes))
            {
                var ball = Instantiate(ballprefab, hits[0].pose.position, new Quaternion());
            }
        }
    }

    public void DoubleTap(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            debugtext.text = "Change Camera"; // Debugtext

            // if cammera is facing the worls, request to be facing user
            if(aRCameraManager.currentFacingDirection == CameraFacingDirection.World)
            {
                GetComponent<ARRaycastManager>().enabled = false;
                GetComponent<ARPlaneManager>().enabled = false;
                GetComponent<ARFaceManager>().enabled = true;
                aRCameraManager.requestedFacingDirection = CameraFacingDirection.User;

            }
            else
            {
                GetComponent<ARRaycastManager>().enabled = true;
                GetComponent<ARPlaneManager>().enabled = true;
                GetComponent<ARFaceManager>().enabled = false;
                aRCameraManager.requestedFacingDirection = CameraFacingDirection.World;
            }
        }
    }
}

