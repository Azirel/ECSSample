
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct MovingBounds : IComponentData
{
	public float2 widthBounds;
	public float2 heightBounds;
}

public class MovingBoundsComponent : ComponentDataWrapper<MovingBounds> { }