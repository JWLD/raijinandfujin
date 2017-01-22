using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
	public GameObject[] cloudPrefabs;
	public float spawnGap;
	public float timeUntilMove;
	public float minSpeed;
	public float maxSpeed;

	public int spawnLimit;
	private int cloudCount = 0;

	private GameObject latestCloud;
	private GameObject cloudHolder;

	void Start()
	{
		cloudHolder = new GameObject();
		cloudHolder.name = "cloudHolder";
		StartCoroutine(SpawnTimer());
	}

	// slowly spawn a new cloud in the game
	void SpawnNewCloud()
	{
		// instantiate random prefab from list, and increment counter
		int randomPrefab = UnityEngine.Random.Range(0, cloudPrefabs.Length);
		latestCloud = (GameObject)Instantiate(cloudPrefabs[randomPrefab], transform.position, Quaternion.identity);
		latestCloud.transform.parent = cloudHolder.transform;
		cloudCount++;
	}

	// add force to the latest cloud
	void FireOffCloud()
	{
		// add random force
		Vector3 randomDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), 0f);
		var force = randomDirection.normalized * UnityEngine.Random.Range(minSpeed, maxSpeed);
		latestCloud.GetComponent<Rigidbody>().AddForce(force);
	}

	// spawn every few seconds
	IEnumerator SpawnTimer()
	{
		while (cloudCount < spawnLimit)
		{
			if (cloudCount > 0)
				yield return new WaitForSeconds(spawnGap);
			SpawnNewCloud();

			yield return new WaitForSeconds(timeUntilMove);
			FireOffCloud();
		}
	}
}