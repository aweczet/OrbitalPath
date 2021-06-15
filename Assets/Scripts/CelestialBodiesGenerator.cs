using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

public class CelestialBodiesGenerator : MonoBehaviour
{
    public Material[] materials;
    
    private int _celestialCount = 5000;
    private float _maxRadius = 100f;
    private List<GameObject> _celestialBodies;

    private void Start()
    {
        _celestialBodies = GeneratreCelestialBodies();
    }

    private List<GameObject> GeneratreCelestialBodies()
    {
        var temporaryBodies = new List<GameObject>();
        var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Rigidbody rigidbody = sphere.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;

        for (int i = 0; i < _celestialCount; i++)
        {
            var celestialBody = Instantiate(sphere);
            celestialBody.transform.position = transform.position + new Vector3(
                Random.Range(-_maxRadius, _maxRadius), Random.Range(-10f, 10f), Random.Range(-_maxRadius, _maxRadius));
            celestialBody.transform.localScale *= Random.Range(.5f, 1f);

            celestialBody.GetComponent<Renderer>().material = materials[Random.Range(0, materials.Length)];
            
            temporaryBodies.Add(celestialBody);
        }
        
        Destroy(sphere);

        return temporaryBodies;
    }

    private void Update()
    {
        List<GameObject> celestialBodiesToRemove = new List<GameObject>();
        foreach (GameObject celestialBody in _celestialBodies)
        {
            Vector3 difference = transform.position - celestialBody.transform.position;
            float distance = difference.magnitude;

            if (distance > 5 * _maxRadius)
            {
                celestialBodiesToRemove.Add(celestialBody);
            }
            
            Vector3 gravityDirection = difference.normalized;
            float gravity = 6.7f * (transform.localScale.x * celestialBody.transform.localScale.x * 80) / (distance * distance);
            
            Vector3 gravityVector = gravityDirection * gravity;
            Rigidbody rigidbody = celestialBody.transform.GetComponent<Rigidbody>(); 
            rigidbody.AddForce(celestialBody.transform.forward, ForceMode.Acceleration);
            rigidbody.AddForce(gravityVector, ForceMode.Acceleration);
        }

        foreach (var body in celestialBodiesToRemove)
        {
            _celestialBodies.Remove(body);
            Destroy(body);
        }
    }
}
