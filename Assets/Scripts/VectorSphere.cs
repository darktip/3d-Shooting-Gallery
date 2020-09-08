using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Structure for spherical coordinates
public struct VectorSphere
{
    public float r;
    public float phi;
    public float theta;

    public VectorSphere(float theta, float phi, float r)
    {
        this.theta = theta;
        this.phi = phi;
        this.r = r;
    }

    public Vector3 ToCartesian() // Converting from spherical to cartesian coordinate system
    {
        float x = r * Mathf.Sin(theta) * Mathf.Cos(phi);
        float z = r * Mathf.Sin(theta) * Mathf.Sin(phi);
        float y = r * Mathf.Cos(theta);

        return new Vector3(x, y, z);
    }
}
