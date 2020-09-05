using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private SphereSettings _sphereSettings;
    private VectorSphere _spherePos;

    private float _speed;
    
    public void Init(SphereSettings sphereSettings, VectorSphere spherePos, float speed)
    {
        _sphereSettings = sphereSettings;
        _spherePos = spherePos;
        _speed = speed;
    }

    public void Update()
    {
        _spherePos.phi += _speed * Time.deltaTime;
        
        transform.position = _spherePos.ToCartesian();
        transform.LookAt(Vector3.zero);
    }
}
