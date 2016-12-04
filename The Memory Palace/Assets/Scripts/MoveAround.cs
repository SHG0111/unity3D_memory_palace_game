using UnityEngine;

public class MoveAround : MonoBehaviour
{

    public Transform target;
    public Camera mainCamera;

    private float initHeightAtDist;
    private bool dzEnabled;

    // Calculate the frustum height at a given distance from the camera.
    float FrustumHeightAtDistance(float distance)
    {
        return 2.0f * distance * Mathf.Tan(mainCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
    }

    // Calculate the FOV needed to get a given frustum height at a given distance.
    float FOVForHeightAndDistance(float height, float distance)
    {
        return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
    }

    // Start the dolly zoom effect.
    void StartDZ()
    {
        var distance = Vector3.Distance(transform.position, target.position);
        initHeightAtDist = FrustumHeightAtDistance(distance);
        dzEnabled = true;
    }

    // Turn dolly zoom off.
    void StopDZ()
    {
        dzEnabled = false;
    }

    void Start()
    {
        StartDZ();
    }

    void Update()
    {
        if (dzEnabled)
        {
            // Measure the new distance and readjust the FOV accordingly.
            var currDistance = Vector3.Distance(transform.position, target.position);
            mainCamera.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, currDistance);
        }

        if (mainCamera.fieldOfView > 40f)
        {
            // Zoom around the door.
            transform.Translate(Vector3.forward * Time.deltaTime);
            transform.Translate(Vector3.left * Time.deltaTime);
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
