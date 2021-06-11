using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour
{
    public Transform orbitingObject;
    public Ellipse orbitPath;

    public float orbitProgress = 0f;
    public float orbitPeriod = 3f;
    public bool orbitActive = true;

    private void Start()
    {
        if (orbitingObject == null)
        {
            orbitActive = false;
            return;
        }
        SetOrbitingObjectPosition();
        StartCoroutine(StartOrbiting());
    }

    private void SetOrbitingObjectPosition()
    {
        Vector2 orbitPosition = orbitPath.Evaluate(orbitProgress);
        orbitingObject.localPosition = new Vector3(orbitPosition.x + transform.parent.GetChild(0).localPosition.x, 0, orbitPosition.y + transform.parent.GetChild(0).localPosition.z);
    }

    private IEnumerator StartOrbiting()
    {
        if (orbitPeriod < .1f)
            orbitPeriod = .1f;

        float orbitSpeed = 1f / orbitPeriod;
        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingObjectPosition();
            yield return null;
        }
    }
}
