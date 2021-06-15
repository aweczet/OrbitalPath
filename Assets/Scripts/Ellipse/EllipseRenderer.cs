using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;

    public int segments;
    public Ellipse ellipse;

    private void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        CalculateEllipse();
    }
    
    private void CalculateEllipse()
    {
        Vector3[] points = new Vector3[segments + 1];
        for (int i = 0; i < segments; i++)
        {
            float t = (float) i / segments;
            Vector2 position2D = ellipse.Evaluate(t);
            points[i] = new Vector3(position2D.x, position2D.y, 0f);
        }

        points[segments] = points[0];

        _lineRenderer.positionCount = segments + 1;
        _lineRenderer.SetPositions(points);
    }
    
    private void OnValidate()
    {
        if (Application.isPlaying)
            CalculateEllipse();
    }


}
