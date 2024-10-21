using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingBagAnchor : MonoBehaviour
{
    [SerializeField] private Transform anchorPoint;
    private LineRenderer lineRenderer;
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.005f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.SetPosition(1, anchorPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, this.transform.position);
    }
}
