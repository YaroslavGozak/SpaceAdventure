using Assets.Scripts.Common;
using System.Collections.Generic;
using System.Linq;
using Uitry;
using UnityEngine;

public class PM : MonoBehaviour
{

    public Rigidbody rb;
    public float thrustForce, x_edge, y_edge, k;
    public Vector3 pl_pos, movement;

    private int _maxSpeed = 3;
    private Ship _ship;
    private Dictionary<IModule, GameObject> _moduleObjects;
    private int _rotationTimeCounter = 0;
    private System.Random _random = new System.Random();

    private void Start()
    {
        _ship = Ship.Instance;
        _moduleObjects = new Dictionary<IModule, GameObject>();
    }

    private void Update()
    {
        _ship.UpdateModules();
    }
    void FixedUpdate()
    {
        validate_pos();

        if (_ship.Energy <= 0 )
        {
            k = 0.001f;
        }
        else
        {
            k = 0.01f;
        }

        //up
        if (Input.GetKey("w") && validate_pos() && rb.velocity.y < _maxSpeed)
        {
            var newPosition = Vector3.up * k * thrustForce;
            MoveShip(newPosition);
        }
        //down
        if (Input.GetKey("s") && validate_pos() && rb.velocity.y > -_maxSpeed)
        {
            var newPosition = Vector3.down * k * thrustForce;
            MoveShip(newPosition);
        }
        //left
        if (Input.GetKey("d") && validate_pos() && rb.velocity.x < _maxSpeed)
        {
            var newPosition = Vector3.right * k * thrustForce;
            MoveShip(newPosition);
        }
        //right
        if (Input.GetKey("a") && validate_pos() && rb.velocity.x > -_maxSpeed)
        {
            var newPosition = Vector3.left * k * thrustForce;
            MoveShip(newPosition);
        }
        //rotate to top
        if (Input.GetKey("q") && validate_pos())
        {
            RotateShip(rb.rotation * Quaternion.AngleAxis(k * thrustForce, Vector3.left));
        }
        //rotate to bottom
        if (Input.GetKey("e") && validate_pos())
        {
            RotateShip(rb.rotation * Quaternion.AngleAxis(-k * thrustForce, Vector3.left));
        }
    }

    private bool validate_pos()
    {
        if (rb.transform.position.y < y_edge && rb.transform.position.y > -y_edge && rb.transform.position.x < x_edge && rb.transform.position.x > -x_edge)
        {
            return true;
        }
        else
        {
            if (rb.transform.position.y > y_edge)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.down);
            }
            else if (rb.transform.position.y < -y_edge)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up);
            }
            else if (rb.transform.position.x < -x_edge)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.right);
            }
            else if (rb.transform.position.x > x_edge)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.left);
            }
            return false;
        }
    }

    private void OnCollisionEnter(Collision collisionInfo)
    {
        var other = collisionInfo.collider;
        //_ship.AddMsg($"Collided: {other.gameObject.name}");
        if (other.gameObject.name.Contains("SolarPanel"))
        {
            var solarPanel = GetSolarPanelObjectToUpdate();
            if (solarPanel != null)
            {
                Debug.Log($"Children: {gameObject.transform.childCount}");
                var module = new SolarPanelModule()
                {
                    Name = solarPanel.name
                };
                try
                {
                    _ship.AddModule(module);
                }
                catch (RamNotEnoughException)
                {
                    return;
                }

                solarPanel.SetActive(true);
                _ship.AddMsg("Solar Panel has been connected\n    charging...");

                Destroy(collisionInfo.gameObject);

                _moduleObjects.Add(module, solarPanel);
                _ship.OnModulaRemove += RemoveModuleHandler;
            }
        }
        if (other.gameObject.name.Contains("hub_col"))
        {
            var hub = GetHubObjectToUpdate();
            _ship.AddMsg("Repaiting hub");
            if (hub != null)
            {
                _ship.AddHub();

                hub.SetActive(true);
                _ship.AddMsg("New Mother Shell was found. Initializing...");
                Invoke("Msg3", 3f);
                Destroy(collisionInfo.gameObject);
            }
        }
        if (other.gameObject.name.Contains("Laser_LP_col"))
        {
            var laser = GetLaserShooterObjectToUpdate();
            //_ship.AddMsg("Repaiting laser");
            if (laser != null)
            {
                var module = new LaserModule()
                {
                    Name = laser.name
                };
                try
                {
                    _ship.AddModule(module);
                    _ship.OnModulaRemove += RemoveModuleHandler;
                }
                catch (RamNotEnoughException)
                {
                    return;
                }

                laser.SetActive(true);
                _ship.AddMsg("Laser has been connected\n    New task: destroy enemy satellites\n    Use 'space' to shoot");
                Destroy(collisionInfo.gameObject);
                _moduleObjects.Add(module, laser);
            }
        }
        if (other.gameObject.name.Contains("Solar_torax"))
        {
            var solarTorax = GetSolarToraxObjectToUpdate();
            //_ship.AddMsg("Repaiting solar torax");
            if (solarTorax != null)
            {
                var module = new SolarToraxModule()
                {
                    Name = solarTorax.name
                };
                try
                {
                    _ship.AddModule(module);
                    _ship.OnModulaRemove += RemoveModuleHandler;
                }
                catch (RamNotEnoughException)
                {
                    return;
                }

                solarTorax.SetActive(true);
                _ship.AddMsg("Solar Torax has been connected\n    Find solar panels for charging");
                _ship.AddTorax();
                Destroy(collisionInfo.gameObject);
                _moduleObjects.Add(module, solarTorax);
            }
        }
        if (other.gameObject.name.Contains("Asteroid") || other.gameObject.name.Contains("Trash"))
        {
            if (_ship.Modules.Any())
            {
                var moduleKey = _ship.Modules[_random.Next(_ship.Modules.Count)];
                moduleKey.Damage(30);
                _ship.AddMsg($"Mother Shell was hit. {moduleKey.Name} health {moduleKey.Health}");
            }
        }
    }

    void Msg3()
    {
        _ship.AddMsg("Status: critical low energy...\n    Find yellow Solar Torax and panels for it\n    to be able to accumulate energy");
    }

    private void MoveShip(Vector3 newPosition)
    {
        rb.AddForce(newPosition);
        _ship.SubstracEnergy(1);
        //Debug.Log("ENERGY: " + _ship.Energy);
    }

    private void RotateShip(Quaternion rotation)
    {
        if (_rotationTimeCounter++ >= 5)
        {
            _ship.SubstracEnergy(1);
            _rotationTimeCounter = 0;
        }
        rb.MoveRotation(rotation);

        //Debug.Log("ENERGY: " + _ship.Energy);
    }

    private void RemoveModuleHandler(object sender, ModuleRemoveEventArgs args)
    {
        RemoveModule(args.Module);
    }
    private void RemoveModule(IModule module)
    {
        var gameObject = _moduleObjects[module];
        gameObject.SetActive(false);
        _moduleObjects.Remove(module);
    }

    private GameObject GetSolarPanelObjectToUpdate()
    {
        var solarTorax = gameObject.transform.Find("Solar_torax_LP").gameObject;

        if (solarTorax.active)
        {
            var key = _moduleObjects.Keys.FirstOrDefault(obj => obj.Name.Contains("Solar_Wing_left"));
            var leftAttached = key != null;
            key = _moduleObjects.Keys.FirstOrDefault(obj => obj.Name.Contains("Solar_Wing_right"));
            var rightAttached = key != null;
            if (leftAttached && rightAttached)
            {
                //_ship.AddMsg("Attaching panel. Finding more damaged");
                var module = _moduleObjects.Keys.Where(obj => obj.Name.Contains("SolarPanel")).OrderBy(obj => obj.Health).First();
                if (module.Health == 100)
                    return null;
                return _moduleObjects[module];
            }
            if (!rightAttached)
            {
                //_ship.AddMsg("Getting right");
                var torax = gameObject.transform.Find("Solar_torax_LP").gameObject;
                return torax.transform.Find("Solar_Wing_right").gameObject;
            }
            else
            {
                //_ship.AddMsg("Getting left");
                var torax = gameObject.transform.Find("Solar_torax_LP").gameObject;
                return torax.transform.Find("Solar_Wing_left").gameObject;
            }
        }
        else
        {
            return null;
        }
        

    }

    private GameObject GetHubObjectToUpdate()
    {
        var key = _moduleObjects.Keys.FirstOrDefault(obj => obj.Name.Contains("Hub"));
        var hubAttached = key != null;
        if (!hubAttached)
        {
            //_ship.AddMsg("Getting hub");
            var hub = gameObject.transform.Find("Hub");
            return hub.gameObject;
        }
        return null;
    }

    private GameObject GetLaserShooterObjectToUpdate()
    {
        var key = _ship.Modules.FirstOrDefault(obj => obj.Name.Contains("Laser_LP"));
        var laserAttached = key != null;
        if (!laserAttached)
        {
            //_ship.AddMsg("Getting laser");
            var laser = gameObject.transform.Find("Laser_LP");
            return laser.gameObject;
        }
        return null;
    }




    private GameObject GetSolarToraxObjectToUpdate()
    {
        var key = _ship.Modules.FirstOrDefault(obj => obj.Name.Contains("Solar_torax_LP"));
        var solarTorax = key != null;
        if (!solarTorax)
        {
            //_ship.AddMsg("Getting solar torax");
            var laser = gameObject.transform.Find("Solar_torax_LP");
            return laser.gameObject;
        }
        return null;
    }
}
