using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
	public GameObject[] cloudPrefabs;
	public float spawnGap;
	public float timeUntilRain;
	public float minSpeed;
	public float maxSpeed;

	public int spawnLimit;
	private int cloudCount = 0;

	private GameObject latestCloud;
	private GameObject cloudHolder;

	void Start()
	{
		StartCoroutine(SpawnTimer());
	}

	public List<GameObject> GetAllClouds()
	{
		var clouds = new List<GameObject>();
		var numChildren = transform.childCount;
		for(var i = 0; i < numChildren; i++)
		{
			clouds.Add(transform.GetChild(i).gameObject);
		}
		return clouds;
	}

	// slowly spawn a new cloud in the game
	void SpawnNewCloud()
	{
		// instantiate random prefab from list, and increment counter
		int randomPrefab = UnityEngine.Random.Range(0, cloudPrefabs.Length);
		latestCloud = (GameObject)Instantiate(cloudPrefabs[randomPrefab], transform.position, Quaternion.identity);
		latestCloud.transform.parent = this.transform;
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

			yield return new WaitForSeconds(timeUntilRain);
			latestCloud.GetComponent<Cloud>().ActivateRain();
			FireOffCloud();
		}
	}
}
