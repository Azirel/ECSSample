using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClassicSceneManager : MonoBehaviour
{
	public GameObject unitPrefab;
	[Range(0, 5000)] public int unitySpawnIncrement;

	public Vector2 widthBounds;
	public Vector2 heightBounds;

	public Text amountDisplayer;
	public Text fpsDisplayer;

	public int fpsFrameBuffer = 10;

	protected int currentAmount = 0;
	protected int frameCounter = 0;
	protected float deltaTimeCounter;

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
			SpawnUnits(unitySpawnIncrement);
		++frameCounter;
		deltaTimeCounter += Time.deltaTime;
		if (frameCounter % fpsFrameBuffer == 0)
		{
			fpsDisplayer.text = (1 / (deltaTimeCounter / fpsFrameBuffer)).ToString();
			frameCounter = 0;
			deltaTimeCounter = 0;
		}
	}

	public void SpawnUnits(int amount)
	{
		for (int i = 0; i < amount; ++i)
		{
			Instantiate(unitPrefab,
			position: new Vector3(
			Random.Range(widthBounds.x, widthBounds.y), 0,
			Random.Range(heightBounds.x, heightBounds.y)),
			Quaternion.identity);
		}
		currentAmount += amount;
		amountDisplayer.text = currentAmount.ToString();
	}
}
