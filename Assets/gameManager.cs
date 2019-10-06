﻿using System.Collections.Generic;
using System.Linq;
using Uitry;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject SpawnObject;

    private int _lastFrameCount;
    private int count = 500;
    private System.Random _random;
    private float _timeDifference = 3;
    private IEnumerable<GameObject> _asteroidCollection;
    private List<GameObject> _trashCollection;
    private Vector3 _spawnArea;
    private readonly float _heightRange = 5;
    private Ship _ship = Ship.Instance;

    private Ship ship;
    void Start()
    {
        ship = Ship.Instance;
        _trashCollection = new List<GameObject> { /*GetPrefabByName("SpaceTrash"),*/ GetPrefabByName("SolarPanel") };
        _asteroidCollection = new List<GameObject> { GetPrefabByName("Asteroid") };
        _random = new System.Random();
        _lastFrameCount = 0;
        _spawnArea = new Vector3(SpawnObject.transform.position.x - 5, SpawnObject.transform.position.y);
        Debug.Log($"Trash collection size: {_trashCollection.Count()}");
        Debug.Log($"Asteroid collection size: {_asteroidCollection.Count()}");

        //init start scene

        ship.SubstracEnergy(10000); 
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

        if (_ship.Energy == 0)
        {
            if (count == 0)
            {
                EndGame();
            }
            count--;
        }
        else
        {
            count = 500;
        }
    }

    void SpawnTrash()
    {
        var trash = TakeRandomElement(_trashCollection);
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


    bool isGameEnd = false;

    public void EndGame()
    {
        if (!isGameEnd)
        {
            isGameEnd = true;
            Debug.Log("Game Over");
            GameObject.Find("Player").GetComponent<Rigidbody>().useGravity = true;
            GameObject.Find("Player").GetComponent<playerMovement>().y_edge = 6;
            Invoke("GoToQuitMenu",4f);
        }
    }
    void GoToQuitMenu()
    {
        SceneManager.LoadScene("EndGame");
    }

  

    GameObject GetPrefabByName(string name)
    {
        var gameObject = Resources.Load<GameObject>($"Prefabs/{name}");
        Debug.Log($"gameObject with name {name}: " + gameObject);
        return gameObject;
    }
}
