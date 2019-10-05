using System.Collections.Generic;
using System.Linq;
using Uitry;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject SpawnObject;

    private int _lastFrameCount;
    private System.Random _random;
    private float _timeDifference = 3;
    private IEnumerable<GameObject> _asteroidCollection;
    private IEnumerable<GameObject> _trashCollection;
    private Vector3 _spawnArea;
    private readonly float _heightRange = 5;

    private Ship ship;
    void Start()
    {
        ship = Ship.Instance;
        _trashCollection = GameObject.FindGameObjectsWithTag("trash");
        var list = _trashCollection.ToList();
        list.AddRange(GameObject.FindGameObjectsWithTag("solar_panel"));
        _trashCollection = list;
        _asteroidCollection = GameObject.FindGameObjectsWithTag("asteroid");
        _random = new System.Random();
        _lastFrameCount = 0;
        _spawnArea = new Vector3(SpawnObject.transform.position.x - 5, SpawnObject.transform.position.y);
        Debug.Log($"Trash collection size: {_trashCollection.Count()}");
    }

    private void FixedUpdate()
    {
        if (IsTimeToSpawn())
        {
            var randomValue = _random.Next();
            if (randomValue > int.MaxValue / 2)
                SpawnTrash();
            else
                SpawnAsteroid();
        }
    }

    void SpawnTrash()
    {
        var trash = TakeRandomElement(_trashCollection);
        Debug.Log($"Instantiated {trash.name}");
        SpawnSpaceElement(trash);
    }
    void SpawnAsteroid()
    {
        var asteroid = TakeRandomElement(_asteroidCollection);
        SpawnSpaceElement(asteroid);
    }

    void SpawnSpaceElement(GameObject elementToSpawn)
    {
        Vector3 spawnPosition = new Vector3(_spawnArea.x, Random.Range(_spawnArea.y - _heightRange, _spawnArea.y + _heightRange), _spawnArea.z);
        Quaternion spawnRotation = Quaternion.identity;
        var element = Instantiate(elementToSpawn, spawnPosition, spawnRotation);
        if (element.GetComponent<BoxCollider>() != null) {
            element.GetComponent<BoxCollider>().isTrigger = false;
        }
        if (element.GetComponent<SphereCollider>() != null)
        {
            element.GetComponent<SphereCollider>().isTrigger = false;
        }
        var rigidBody = element.GetComponent<Rigidbody>();
        var verticalMovement = _random.Next(-10, 10);
        var horizontalMovement = -100;
        rigidBody.AddForce(horizontalMovement, verticalMovement, 0);
        rigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
    }

    T TakeRandomElement<T>(IEnumerable<T> collection)
    {
        var list = collection.ToList();
        var size = list.Count;
        var elementIndex = _random.Next(0, size);
        return list[elementIndex];
    }

    bool IsTimeToSpawn()
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
