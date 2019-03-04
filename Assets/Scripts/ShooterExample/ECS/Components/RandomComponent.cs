
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct RandomData : IComponentData
{
	public uint randomSeed;
	public uint randomIterator;
}

public class RandomComponent : ComponentDataWrapper<RandomData> { }