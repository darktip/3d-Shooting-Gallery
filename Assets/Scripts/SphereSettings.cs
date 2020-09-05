﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sphere Settings", menuName = "Sphere Settings", order = 50)]
public class SphereSettings : ScriptableObject
{
    [SerializeField] private float radius;
    [SerializeField] private float spacing;
    
    public float Spacing => spacing;
    public float Radius => radius;
}
