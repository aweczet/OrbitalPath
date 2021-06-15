using System.Collections;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    public CelestialBody celestialBody;
    
    private Transform _orbitingObject;
    private Ellipse _orbitPath;
    
    private float _orbitProgress;
    private float _orbitPeriod;
    private bool _orbitActive = true;
    
    private float _distanceScale = 10000000;
    private float _radiusScale = 10000;

    private void Start()
    {
        _orbitingObject = transform.GetChild(0);
        if (_orbitingObject == null)
        {
            _orbitActive = false;
            return;
        }
        _orbitPeriod = celestialBody.orbitPeriod * 5;
        float distance = celestialBody.GetDistance(_distanceScale);
        _orbitPath = new Ellipse(distance, distance);

        float radius = celestialBody.GetRadius(_radiusScale);
        _orbitingObject.localScale = new Vector3(radius, radius, radius);
        
        _orbitingObject.GetComponent<Renderer>().material.SetTexture("_MainTex", celestialBody.texture);

        SetOrbitingObjectPosition();
        StartCoroutine(StartOrbiting());
    }

    private void SetOrbitingObjectPosition()
    {
        Vector2 orbitPosition = _orbitPath.Evaluate(_orbitProgress);
        Vector3 parentPosition;
        if ((parentPosition = transform.parent.GetChild(0).localPosition) == null)
        {
            parentPosition = transform.position;
        }
        _orbitingObject.localPosition = new Vector3(orbitPosition.x + parentPosition.x, 0, orbitPosition.y + parentPosition.z);
    }

    private IEnumerator StartOrbiting()
    {
        if (_orbitPeriod < .1f)
            _orbitPeriod = .1f;

        float orbitSpeed = 1f / _orbitPeriod;
        while (_orbitActive)
        {
            _orbitProgress += Time.deltaTime * orbitSpeed;
            _orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }
}
