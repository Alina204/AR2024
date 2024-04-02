using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class Face : MonoBehaviour
{
    [SerializeField] private ARCameraManager aRCameraManager;
    [SerializeField] private ARFaceManager arFace;

    private void OnEnable() => arFace.facesChanged += OnFaceChange;
    private void OnDisable() => arFace.facesChanged -= OnFaceChange;
    private List<ARFace> faces = new List<ARFace>();

    private void OnFaceChange(ARFacesChangedEventArgs eventArgs)
    {
        foreach (var newFace in eventArgs.added)
        {
            faces.Add(newFace);
        }
        foreach (var lostFace in eventArgs.removed)
        {
            faces.Remove(lostFace);
        }
    }
    [SerializeField] private TMP_Text debugtext;
    // Update is called once per frame
    void Update()
    {
        if(aRCameraManager.currentFacingDirection == CameraFacingDirection.User)
        {
            if (faces.Count > 0)
            {
                Vector3 lowerLippos = faces[0].vertices[14];
                debugtext.text = lowerLippos.ToString("F3");
            }
        }
    }
    
}
