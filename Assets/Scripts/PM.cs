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
        Debug.Log($"Collided: {other.gameObject.name}");
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

                Destroy(collisionInfo.gameObject);

                _moduleObjects.Add(module, solarPanel);
                _ship.OnModulaRemove += RemoveModuleHandler;
            }
        }
        if (other.gameObject.name.Contains("hub_col"))
        {
            var hub = GetHubObjectToUpdate();
            Debug.Log("Adding hub: ", hub);
            if (hub != null)
            {
                _ship.AddHub();

                hub.SetActive(true);
                Debug.Log("Hub added: ", hub);
                Destroy(collisionInfo.gameObject);
            }
        }
        if (other.gameObject.name.Contains("Laser_LP_col"))
        {
            var laser = GetLaserShooterObjectToUpdate();
            Debug.Log("Adding laser: ", laser);
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
                Debug.Log("Laser added: ", laser);
                Destroy(collisionInfo.gameObject);
                _moduleObjects.Add(module, laser);
            }
        }
        if (other.gameObject.name.Contains("Solar_torax"))
        {
            var solarTorax = GetSolarToraxObjectToUpdate();
            Debug.Log("Adding solar torax: ", solarTorax);
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
                Debug.Log("SolarToraxModule added: ", solarTorax);
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
                Debug.Log($"Ship hit. {moduleKey.Name} health {moduleKey.Health}");
            }
        }
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
        var key = _moduleObjects.Keys.FirstOrDefault(obj => obj.Name.Contains("Solar_Wing_left"));
        var leftAttached = key != null;
        key = _moduleObjects.Keys.FirstOrDefault(obj => obj.Name.Contains("Solar_Wing_right"));
        var rightAttached = key != null;
        if (leftAttached && rightAttached)
        {
            Debug.Log("Attaching panel. Finding more damaged");
            var module = _moduleObjects.Keys.Where(obj => obj.Name.Contains("SolarPanel")).OrderBy(obj => obj.Health).First();
            if (module.Health == 100)
                return null;
            return _moduleObjects[module];
        }
        if (!rightAttached)
        {
            Debug.Log("Getting right");
            var torax = gameObject.transform.Find("Solar_torax_LP").gameObject;
            return torax.transform.Find("Solar_Wing_right").gameObject;
        }
        else
        {
            Debug.Log("Getting left");
            var torax = gameObject.transform.Find("Solar_torax_LP").gameObject;
            return torax.transform.Find("Solar_Wing_left").gameObject;
        }

    }

    private GameObject GetHubObjectToUpdate()
    {
        var key = _moduleObjects.Keys.FirstOrDefault(obj => obj.Name.Contains("Hub"));
        var hubAttached = key != null;
        if (!hubAttached)
        {
            Debug.Log("Getting hub");
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
            Debug.Log("Getting laser");
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
            Debug.Log("Getting solar");
            var laser = gameObject.transform.Find("Solar_torax_LP");
            return laser.gameObject;
        }
        return null;
    }
}