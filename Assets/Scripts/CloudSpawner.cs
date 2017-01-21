using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
	public GameObject cloudPrefab;
	public int spawnGapInSeconds;
	public int spawnLimit;
	private int cloudCount;

	void Start()
	{
		SpawnNewCloud();
		StartCoroutine(SpawnTimer());
	}

	void SpawnNewCloud()
	{
		GameObject newCloud = (GameObject)Instantiate(cloudPrefab, transform.position, Quaternion.identity);
		cloudCount++;

		// add random force
		newCloud.transform.eulerAngles = new Vector3(0, 0, UnityEngine.Random.Range(0, 360));
		var force = newCloud.transform.up * UnityEngine.Random.Range(10f, 50f);
		newCloud.GetComponent<Rigidbody>().AddForce(force);
	}

	IEnumerator SpawnTimer()
	{
		while (cloudCount < spawnLimit)
		{
			yield return new WaitForSeconds(spawnGapInSeconds);
			SpawnNewCloud();
		}
	}
}