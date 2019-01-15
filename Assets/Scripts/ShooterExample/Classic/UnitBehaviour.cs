using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{
	public Vector2 widthBounds;
	public Vector2 heightBounds;
	public float targetTreshold = 0.1f;
	public float speed = 1;

	protected Vector3 target;

	void Update()
	{
		if (Vector3.SqrMagnitude(transform.position - target) < targetTreshold * targetTreshold)
			target = new Vector3(Random.Range(widthBounds.x, widthBounds.y), 1, Random.Range(heightBounds.x, heightBounds.y));
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
	}
}
