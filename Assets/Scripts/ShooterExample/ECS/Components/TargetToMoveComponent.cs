
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct TargetToMove : IComponentData
{
	public float3 XYTarget;
}

public class TargetToMoveComponent : ComponentDataWrapper<TargetToMove> { }