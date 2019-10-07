using System.Collections.Generic;
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
    private List<GameObject> _asteroidCollection;
    private List<GameObject> _trashCollection;
    private GameObject _hub;
    private Vector3 _spawnArea;
    private readonly float _heightRange = 5;
    private Ship _ship = Ship.Instance;
    private bool _hubSpawned = false;

    private Ship ship;
    void Start()
    {
        ship = Ship.Instance;
        _trashCollection = new List<GameObject> { GetPrefabByName("Solar_torax_LP_col"), GetPrefabByName("SolarPanel"), GetPrefabByName("Laser_LP_col") };
        _asteroidCollection = new List<GameObject> { GetPrefabByName("Asteroid"), GetPrefabByName("SpaceTrash") };
        _hub = GameObject.Find("hub_col");
        _random = new System.Random();
        _lastFrameCount = 0;
        _spawnArea = new Vector3(SpawnObject.transform.position.x - 5, SpawnObject.transform.position.y);

        //init start scene

        ship.SubstracEnergy(10000);
        ship.NullScore();
        ship.RemoveHub();
        _hubSpawned = false;

        _ship.AddMsg("System initializing... \n    system checking...");
        Invoke("startMsg1", 3f);
        Invoke("startMsg2", 7f);

    }

    void startMsg1()
    {
        _ship.AddMsg("Mother Shell was destroyed... \n    Use 'w','a','s','d','q','e' to collect\n    useful parts of padded satellites");
    }
    void startMsg2()
    {
        _ship.AddMsg("Mother Shell was destroyed... \n    Use 'w','a','s','d','q','e' to collect\n    useful parts of padded satellites\n    #1 Priority task: find another Shell");
    }

    private void FixedUpdate()
    {
        if (!_ship.IsHubAttached)
        {
            if (!_hubSpawned)
            {
                SpawnHub();
                _hubSpawned = true;
                Invoke("SpawnTorax", 3f);
                Invoke("SpawnPanel", 6f);
                Invoke("SpawnLaser", 9f);
                Invoke("SpawnTorax", 12f);
                Invoke("SpawnPanel", 15f);
                Invoke("SpawnLaser", 18f);
                Invoke("SpawnTorax", 21f);
                Invoke("SpawnPanel", 24f);
                Invoke("SpawnLaser", 27f);
            }
        }
        else
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
    void SpawnTorax()
    {
        SpawnSpaceElement(GetPrefabByName("Solar_torax_LP_col"));
    }
    void SpawnPanel()
    {
        SpawnSpaceElement(GetPrefabByName("SolarPanel"));
    }
    void SpawnLaser()
    {
        SpawnSpaceElement(GetPrefabByName("Laser_LP_col"));
    }

    void SpawnHub()
    {
        try
        {
            var player = GameObject.Find("Player");
            Vector3 spawnPosition = new Vector3(_spawnArea.x, player.transform.position.y, _spawnArea.z);
            Quaternion spawnRotation = new Quaternion(-90, 90, 0, 0);
            SpawnSpaceElement(_hub, spawnPosition, spawnRotation, true);
        }
        catch { }
    }

    void SpawnSpaceElement(GameObject elementToSpawn, Vector3 spawnPosition, Quaternion spawnRotation, bool spawnRandomHorizontalVector = false)
    {
        var element = Instantiate(elementToSpawn, spawnPosition, spawnRotation);
        if (element.GetComponent<BoxCollider>() != null) {
            element.GetComponent<BoxCollider>().isTrigger = false;
        }
        if (element.GetComponent<SphereCollider>() != null)
        {
            element.GetComponent<SphereCollider>().isTrigger = false;
        }
        var damage = element.GetComponent<Damageble>();
        if(damage == null)
        {
            element.AddComponent<Damageble>();
        }
        var rotate = element.GetComponent<Rotator>();
        if (rotate == null)
        {
            element.AddComponent<Rotator>();
        }
        var rigidBody = element.GetComponent<Rigidbody>();
        if(rigidBody == null)
        {
            element.AddComponent<Rigidbody>();
        }
        rigidBody = element.GetComponent<Rigidbody>();
        rigidBody.useGravity = false;
        var verticalMovement = 0;
        if (spawnRandomHorizontalVector)
        {
            verticalMovement = _random.Next(-10, 10);
        }
        var horizontalMovement = -100;
        rigidBody.AddForce(horizontalMovement, verticalMovement, 0);
        rigidBody.collisionDetectionMode = CollisionDetectionMode.Discrete;
        
    }

    void SpawnSpaceElement(GameObject elementToSpawn)
    {
        Quaternion spawnRotation = Quaternion.identity;
        Vector3 spawnPosition = new Vector3(_spawnArea.x, Random.Range(_spawnArea.y - _heightRange, _spawnArea.y + _heightRange), _spawnArea.z);
        SpawnSpaceElement(elementToSpawn, spawnPosition, spawnRotation);
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
            GameObject.Find("Player").GetComponent<PM>().y_edge = 6;
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
