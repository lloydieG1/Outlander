using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IndicatorController : MonoBehaviour
{
    [SerializeField] private GameObject hudIndicatorPrefab;
    [SerializeField] private List<GameObject> planets;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Vector2 indicatorSize;
    [SerializeField] private float padding;

    private Dictionary<GameObject, GameObject> planetToIndicatorMap;

    void Start()
    {
        planetToIndicatorMap = new Dictionary<GameObject, GameObject>();

        foreach (GameObject planet in planets)
        {
            GameObject indicator = Instantiate(hudIndicatorPrefab, canvas.transform);
            indicator.SetActive(false);
            planetToIndicatorMap.Add(planet, indicator);

            Sprite planetSprite = planet.GetComponent<SpriteRenderer>().sprite;
            indicator.GetComponent<Image>().sprite = planetSprite;

            indicator.GetComponent<RectTransform>().sizeDelta = indicatorSize;
        }
    }

    void Update()
    {
        foreach (KeyValuePair<GameObject, GameObject> entry in planetToIndicatorMap)
        {
            GameObject planet = entry.Key;
            GameObject indicator = entry.Value;

            Vector3 screenPos = mainCamera.WorldToScreenPoint(planet.transform.position);

            if (screenPos.z < 0 || screenPos.x < 0 || screenPos.x > Screen.width || screenPos.y < 0 || screenPos.y > Screen.height)
            {
                indicator.SetActive(true);
                Vector2 indicatorPosition = GetIndicatorCanvasPosition(screenPos);
                indicator.GetComponent<RectTransform>().anchoredPosition = indicatorPosition;
            }
            else
            {
                indicator.SetActive(false);
            }
        }
    }

    private Vector2 GetIndicatorCanvasPosition(Vector3 screenPosition)
    {
        float x = screenPosition.x;
        float y = screenPosition.y;

        if (x < padding)
        {
            x = padding;
        }
        else if (x > Screen.width - padding)
        {
            x = Screen.width - padding;
        }

        if (y < padding)
        {
            y = padding;
        }
        else if (y > Screen.height - padding)
        {
            y = Screen.height - padding;
        }

        Vector2 canvasPosition = new Vector2(x - Screen.width / 2, y - Screen.height / 2);
        return canvasPosition;
    }
}
