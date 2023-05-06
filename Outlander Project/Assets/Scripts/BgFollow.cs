using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class BgFollow : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private string sortingLayerName = "Default";
    [SerializeField] private int orderInLayer;

    private Vector3 initialRotation;

    private void Awake()
    {
        // Set the sorting layer and order in layer for the mesh renderer
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            meshRenderer.sortingLayerName = sortingLayerName;
            meshRenderer.sortingOrder = orderInLayer;
        }
    }

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
