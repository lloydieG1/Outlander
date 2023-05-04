using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBg : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Material mat;
    Vector2 offset;

    // this number is calculated based on the quad size (1/50) and the tile size (1/2) and the orthographic size (20) minus a bit for some parallax.
    [SerializeField] private float scrollSpeed = 0.007f;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        mat = meshRenderer.material;
        offset = mat.mainTextureOffset;
    }

    // Update is called once per frame
    void Update()
    {
        offset = transform.position * scrollSpeed;
        mat.mainTextureOffset = offset;
    }
}
