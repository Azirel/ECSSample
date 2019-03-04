using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using System.ComponentModel;

public class MovingSystem : JobComponentSystem
{
	struct MovingJob : IJobProcessComponentData<Position, MovingBounds, TargetToMove, MovingSpeed>
	{
		public float deltaTime;
		public float distanceTreshold;
		public float random;

		void IJobProcessComponentData<Position, MovingBounds, TargetToMove, MovingSpeed, RandomData>.Execute(ref Position position, [ReadOnly(true)] ref MovingBounds bounds, ref TargetToMove xyTarget, [ReadOnly(true)] ref MovingSpeed speed)
		{
			if (Vector3.SqrMagnitude(position.Value - xyTarget.XYTarget) < distanceTreshold * distanceTreshold)
			{
				var random = new Unity.Mathematics.Random();
				TargetToMove = new Unity.Mathematics.Random()
			}
		}
	}
}
