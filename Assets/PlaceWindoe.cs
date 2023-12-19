using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections.Generic;

public class PlaceWindoe : MonoBehaviour
{

    public ARRaycastManager raycastManager;
    public ARPlaneManager planeManager;
    public GameObject windowPrefab;

    public Camera MainCamera;
    // public Camera ViewCamera;

    // public GameObject sphere;

    private GameObject view;

    public GameObject preSphere;

    private bool placed;
    // Start is called before the first frame update
    void Start()
    {
        placed = false;
        view = null;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cameraRotation = MainCamera.transform.eulerAngles;
        preSphere.transform.eulerAngles = cameraRotation;
        preSphere.transform.position = MainCamera.transform.position;
        // ViewCamera.transform.rotation = Quaternion.Euler(0, MainCamera.transform.rotation.y, MainCamera.transform.rotation.z);
        Vector2 touchPosition = Input.GetTouch(0).position;
        List<ARRaycastHit> hits = new List<ARRaycastHit>();

        if (!placed && raycastManager.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            foreach (var hit in hits)
            {
                // Check if the hit plane is vertical
                if (Vector3.Dot(hit.pose.up, Vector3.up) < 0.5f)
                {
                    placed = true;
                    // Place the window prefab directly on the hit point and orient it based on the hit normal
                    Instantiate(windowPrefab, hit.pose.position, Quaternion.LookRotation(-hit.pose.up, Vector3.up));
                    // view = Instantiate(sphere, MainCamera.transform.position, ViewCamera.transform.rotation);
                    // view.transform.parent = null;
                    planeManager.enabled = false;
                    foreach (var plane in planeManager.trackables)
                    {
                        //hide the plane
                        plane.gameObject.SetActive(false);
                    }
                    break;
                }
            }
        }
    }
}
