using UnityEngine;

public class BgFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    private Vector3 initialRotation;

    private void Start()
    {
        initialRotation = transform.rotation.eulerAngles;
    }

    private void Update()
    {
        // Set the background's position to match the camera's position
        Vector3 cameraPosition = cameraTransform.position;
        transform.position = new Vector3(cameraPosition.x, cameraPosition.y, transform.position.z);

        // Keep the background's rotation constant
        transform.rotation = Quaternion.Euler(initialRotation);
    }
}
