using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.UI;
using Unity.Entities;
using Unity.Collections;

public class ECSGameManager : ClassicSceneManager
{
	public GameObject SimpleUnityPrefab;
	EntityManager EntityManager;

	protected void Awake()
	{

	}

	protected void Start()
	{
		EntityManager = World.Active.GetOrCreateManager<EntityManager>();
	}

	protected override void Update()
	{
		UpdateFPS();
	}

	protected override void SpawnUnits(int amount)
	{
		NativeArray<Entity> entities = new NativeArray<Entity>(amount, Allocator.Temp);
		EntityManager.Instantiate(SimpleUnityPrefab, entities);

	}
}
