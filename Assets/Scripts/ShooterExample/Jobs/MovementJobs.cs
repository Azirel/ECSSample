using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Burst;

[BurstCompile]
public struct MovementJob : IJobParallelForTransform
{
	public Vector2 widthBounds;
	public Vector2 heightBounds;
	public float targetTreshold;
	public float speed;
	public float deltaTime;
	public float random;
	public float[] targets;

	private Vector3 target;

	public void Execute(int index, TransformAccess transform)
	{
		if (Vector3.SqrMagnitude(transform.position - target) < targetTreshold * targetTreshold)
			target = new Vector3(widthBounds.x + (random * (index + 1)) % (widthBounds.y - widthBounds.x), 1, heightBounds.x + (random * (index + 1)) % (heightBounds.y - heightBounds.x));
		transform.position = Vector3.MoveTowards(transform.position, target, speed * deltaTime);
	}
}
