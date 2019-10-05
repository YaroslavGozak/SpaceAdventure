using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    private IEnumerable<GameObject> _trashCollection;
    public GameObject Asteroid;
    public Vector3 SpawnZone;

    private int _lastFrameCount;
    private System.Random _random;
    private float _timeDifference = 3;

    void Start()
    {
        _trashCollection = GameObject.FindGameObjectsWithTag("trash");
        _random = new System.Random();
        _lastFrameCount = 0;

        SpawnAsteroid();
    }

    private void FixedUpdate()
    {
        if (CheckForTimeToSpawn())
        {
            SpawnTrash();
        }
    }

    void SpawnTrash()
    {
        Vector3 trashSpawnPosition = new Vector3(SpawnZone.x, Random.Range(-SpawnZone.y, SpawnZone.y), SpawnZone.z);
        Quaternion trashSpawnRotation = Quaternion.identity;
        var trash = Instantiate(TakeRandomElement(_trashCollection), trashSpawnPosition, trashSpawnRotation);
        var rigidBody = trash.GetComponent<Rigidbody>();
        var verticalMovement = _random.Next(-10, 10);
        var horizontalMovement = -100;
        rigidBody.AddForce(horizontalMovement, verticalMovement, 0);
    }
    void SpawnAsteroid()
    {
        Vector3 asteroidSpawnPosition = new Vector3(Random.Range(-SpawnZone.x, SpawnZone.x), SpawnZone.y, SpawnZone.z);
        Quaternion asteroidSpawnRotation = Quaternion.identity;
        var asteroid = Instantiate(Asteroid, asteroidSpawnPosition, asteroidSpawnRotation);
        var rigidBody = asteroid.GetComponent<Rigidbody>();
        var verticalMovement = _random.Next(-10, 10);
        var horizontalMovement = -100;
        rigidBody.AddForce(horizontalMovement, verticalMovement, 0);
    }

    T TakeRandomElement<T>(IEnumerable<T> collection)
    {
        var list = collection.ToList();
        var size = list.Count;
        var elementIndex = _random.Next(0, size);
        return list[elementIndex];
    }

    bool CheckForTimeToSpawn()
    {
        var lastTime = _lastFrameCount * Time.deltaTime;
        var currentTime = Time.frameCount * Time.deltaTime;
        if (currentTime - lastTime > _timeDifference)
        {
            _lastFrameCount = Time.frameCount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
