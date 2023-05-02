using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class IndicatorController : MonoBehaviour
{
    [SerializeField] private GameObject hudIndicatorPrefab;
    [SerializeField] private List<GameObject> planets;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector2 indicatorSize; // New serialized field for size
    [SerializeField] private float padding; // New serialized field for padding

    private Dictionary<GameObject, GameObject> planetToIndicatorMap;

    void Start()
    {
        planetToIndicatorMap = new Dictionary<GameObject, GameObject>();

        // Instantiate indicators for each planet and add them to the dictionary
        foreach (GameObject planet in planets)
        {
            GameObject indicator = Instantiate(hudIndicatorPrefab, canvas.transform);
            indicator.SetActive(false);
            planetToIndicatorMap.Add(planet, indicator);

            // Set the indicator image based on the planet's sprite
            Sprite planetSprite = planet.GetComponent<SpriteRenderer>().sprite;
            indicator.GetComponent<Image>().sprite = planetSprite;

            // Set the size of the indicator
            indicator.GetComponent<RectTransform>().sizeDelta = indicatorSize;
        }
    }

    void Update()
    {
        foreach (KeyValuePair<GameObject, GameObject> entry in planetToIndicatorMap)
        {
            GameObject planet = entry.Key;
            GameObject indicator = entry.Value;

            // Get the viewport position and radius of the planet
            Vector3 planetViewportPos = mainCamera.WorldToViewportPoint(planet.transform.position);
            float radius = planet.transform.localScale.x / 2;

            // Calculate the viewport bounds considering the planet radius
            Vector3 minBounds = mainCamera.WorldToViewportPoint(planet.transform.position - new Vector3(radius, radius, 0));
            Vector3 maxBounds = mainCamera.WorldToViewportPoint(planet.transform.position + new Vector3(radius, radius, 0));

            // Check if the planet is out of bounds
            if (planetViewportPos.z < 0 || minBounds.x > 1 || maxBounds.x < 0 || minBounds.y > 1 || maxBounds.y < 0)
            {
                indicator.SetActive(true);
                Vector2 indicatorPosition = GetIndicatorCanvasPosition(planet.transform.position);
                indicator.GetComponent<RectTransform>().anchoredPosition = indicatorPosition;
            }
            else
            {
                indicator.SetActive(false);
            }
        }
    }

private Vector2 GetIndicatorCanvasPosition(Vector3 targetPosition)
{
    // Calculate the direction from the camera to the target
    Vector3 dir = (targetPosition - mainCamera.transform.position).normalized;
    
    // Convert the direction vector to viewport space
    Vector3 viewportDir = mainCamera.WorldToViewportPoint(mainCamera.transform.position + dir) - new Vector3(0.5f, 0.5f, 0);
    float aspectRatio = mainCamera.aspect;

    // Determine the intersection point between the direction vector and the viewport edge
    if (Mathf.Abs(viewportDir.x) > Mathf.Abs(viewportDir.y) * aspectRatio)
    {
        viewportDir *= (0.5f / Mathf.Abs(viewportDir.x));
    }
    else
    {
        viewportDir *= (0.5f / Mathf.Abs(viewportDir.y));
    }

    // Calculate the adjusted viewport position
    Vector3 adjustedViewportPos = new Vector3(0.5f, 0.5f, 0) + viewportDir;
    Vector3 canvasBoundaryIntersection = mainCamera.ViewportToWorldPoint(adjustedViewportPos);
    Vector2 screenPoint = mainCamera.WorldToScreenPoint(canvasBoundaryIntersection);
    Vector2 canvasPosition;
    RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPoint, canvas.worldCamera, out canvasPosition);

    // Adjust the position based on padding
    Vector2 paddingVector = new Vector2(Mathf.Sign(canvasPosition.x) * padding, Mathf.Sign(canvasPosition.y) * padding);
    canvasPosition -= paddingVector;

    return canvasPosition;
}

}

