using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class JobsGameManager : ClassicSceneManager
{
    TransformAccessArray transforms;
    MovementJob jobsMovement;
    JobHandle moveHandle;
    NativeList<Vector3> targets;

    [Range(0, 100)] public float speed = 13;

    private void OnDisable()
    {
        moveHandle.Complete();
        transforms.Dispose();
        targets.Dispose();
    }

    private void Start()
    {
        transforms = new TransformAccessArray(0, -1);
        targets = new NativeList<Vector3>(0, Allocator.Persistent);
        jobsMovement = new MovementJob()
        {
            heightBounds = heightBounds,
            widthBounds = widthBounds,
            speed = speed,
            targetTreshold = 0.1f,
            random = Random.Range(-100, 100),
            targets = targets.ToDeferredJobArray()
        };
    }

    protected override void Update()
    {
        moveHandle.Complete();

        if (Input.GetKeyUp(KeyCode.Space))
            SpawnUnits(unitySpawnIncrement);

        jobsMovement.deltaTime = Time.deltaTime;
        jobsMovement.random = Random.Range(float.MinValue, float.MaxValue);

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
            targets.Add(Vector3.zero);
        }
        currentAmount += amount;
        amountDisplayer.text = (currentAmount / 1000).ToString() + "k";
        jobsMovement.targets = targets.ToDeferredJobArray();
    }

    [ContextMenu("Test")]
    public void Test()
    {
        print(0.5f % 3.5f);
    }

}
