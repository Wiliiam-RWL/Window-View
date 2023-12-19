using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class PlaceWindow : MonoBehaviour
{

    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public GameObject windowPrefab;

    public Camera MainCamera;
    // public Camera ViewCamera;

    // public GameObject sphere;

    // private GameObject view;

    private bool placed;
    // Start is called before the first frame update
    void Start()
    {
        placed = false;
        // view = null;
    }

    // Update is called once per frame
    void Update()
    {
        // ViewCamera.transform.rotation = Quaternion.Euler(0, MainCamera.transform.rotation.y, MainCamera.transform.rotation.z);
        Vector2 touchPosition = Input.GetTouch(0).position;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (!placed && raycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            placed = true;
            var hit = hits[0];
            Instantiate(windowPrefab, hit.pose.position,
                Quaternion.LookRotation(-hit.pose.up, Vector3.up));
            planeManager.enabled = false;
            foreach (var plane in planeManager.trackables)
            {
                //hide the plane
                plane.gameObject.SetActive(false);
            }
        }
    }
}
