using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class JobsGameManager : ClassicSceneManager
{
	TransformAccessArray transforms;
	MovementJob jobsMovement;
	JobHandle moveHandle;

	[Range(0, 100)] public float speed = 13;

	private void OnDisable()
	{
		moveHandle.Complete();
		transforms.Dispose();
	}

	private void Start()
	{
		transforms = new TransformAccessArray(0, -1);
	}

	protected override void Update()
	{
		moveHandle.Complete();

		if (Input.GetKeyUp(KeyCode.Space))
			SpawnUnits(unitySpawnIncrement);

		jobsMovement = new MovementJob()
		{
			heightBounds = heightBounds,
			widthBounds = widthBounds,
			speed = speed,
			targetTreshold = 0.1f,
			deltaTime = Time.deltaTime,
			random = Random.Range(-100, 100)
		};

		moveHandle = jobsMovement.Schedule(transforms);
		JobHandle.ScheduleBatchedJobs();

		UpdateFPS();
	}

	protected override void SpawnUnits(int amount)
	{
		moveHandle.Complete();
		transforms.capacity = transforms.length + amount;
		for (int i = 0; i < amount; ++i)
		{
			var instance = Instantiate(unitPrefab,
			position: new Vector3(
			Random.Range(widthBounds.x, widthBounds.y), 0,
			Random.Range(heightBounds.x, heightBounds.y)),
			Quaternion.identity);

			transforms.Add(instance.transform);
		}
		currentAmount += amount;
		amountDisplayer.text = currentAmount.ToString();
	}

	[ContextMenu("Test")]
	public void Test()
	{
		print(0.5f % 3.5f);
	}

}
