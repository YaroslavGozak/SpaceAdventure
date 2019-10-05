using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject Trash;
    public GameObject Asteroid;
    public Vector3 spawnZone;
    void Start()
    {
        for (int i = 0; i < 5; i++)
            {
                spawnTrash();
            }
    }

    void spawnTrash()
    {
        Vector3 trashSpawnPosition = new Vector3(spawnZone.x, Random.Range(-spawnZone.y, spawnZone.y), spawnZone.z);
        Quaternion trashSpawnRotation = Quaternion.identity;
        Instantiate(Trash, trashSpawnPosition, trashSpawnRotation);
    }
    void spawnAsteroid()
    {
        Vector3 asteroidSpawnPosition = new Vector3(Random.Range(-spawnZone.x, spawnZone.x), spawnZone.y, spawnZone.z);
        Quaternion asteroidSpawnRotation = Quaternion.identity;
        Instantiate(Trash, asteroidSpawnPosition, asteroidSpawnRotation);
    }
}
