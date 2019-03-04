using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Jobs;
using Unity.Jobs;
using Unity.Burst;
using Unity.Collections;

[BurstCompile]
public struct MovementJob : IJobParallelForTransform
{
	public Vector2 widthBounds;
	public Vector2 heightBounds;
	public float targetTreshold;
	public float speed;
	public float deltaTime;
	public float random;
	public NativeArray<Vector3> targets;

	public void Execute(int index, TransformAccess transform)
	{
		if (Vector3.SqrMagnitude(transform.position - targets[index]) < targetTreshold * targetTreshold)
			targets[index] = new Vector3(widthBounds.x + (random * (index + 1)) % (widthBounds.y - widthBounds.x), 1, heightBounds.x + (random * (index + 1)) % (heightBounds.y - heightBounds.x));
		transform.position = Vector3.MoveTowards(transform.position, targets[index], speed * deltaTime);
	}
}
