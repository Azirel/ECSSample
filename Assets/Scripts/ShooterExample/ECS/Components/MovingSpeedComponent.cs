using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct MovingSpeed : IComponentData
{
	public float speed;
}

public class MovingSpeedComponent : ComponentDataWrapper<MovingSpeed> { }