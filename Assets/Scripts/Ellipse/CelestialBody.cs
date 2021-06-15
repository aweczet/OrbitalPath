using UnityEngine;

[CreateAssetMenu(fileName = "New Celestial Body", menuName = "CelestialBody")]
public class CelestialBody : ScriptableObject
{
    // https://upload.wikimedia.org/wikipedia/commons/6/64/Solar-System.pdf
    public float distance;
    public float radius;
    public Texture texture;

    // https://en.wikipedia.org/wiki/Orbital_period
    public float orbitPeriod;

    public float GetDistance(float scale)
    {
        return distance / scale;
    }

    public float GetRadius(float scale)
    {
        return radius / scale;
    }
}