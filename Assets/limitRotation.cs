using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class limitRotation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sphere;

    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;

    private bool placed;
    private Vector3 lastSphereRotation;
    void Start()
    {
        placed = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 viewCameraRotation = transform.eulerAngles;
        if (placed)
        {
            viewCameraRotation.x = lastSphereRotation.x; // last rotation: the rotation 
            viewCameraRotation.y = lastSphereRotation.y; // when the sphere fixed
        }
        else
        {
            lastSphereRotation = transform.eulerAngles;
        }
        sphere.transform.eulerAngles = viewCameraRotation;
        sphere.transform.position = transform.position;

        Vector2 touchPosition = Input.GetTouch(0).position;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (raycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            placed = true;
        }

    }
}
