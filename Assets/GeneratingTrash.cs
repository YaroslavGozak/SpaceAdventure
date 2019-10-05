using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratingTrash : MonoBehaviour
{
    public GameObject myPrefab;
    // Start is called before the first frame update
    private List<GameObject> _trashItems;
    private int _lastFrameCount;
    private float _speed = 0.05F;
    private System.Random _random;
    void Start()
    {
        _trashItems = new List<GameObject>();
        _lastFrameCount = 0;
        _random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = GameObject.Find("Setelite").transform.transform.position;

        foreach (var item in _trashItems)
        {
            item.transform.position = new Vector3(item.transform.position.x + _speed, item.transform.position.y, item.transform.position.z);
        }

        if (Time.frameCount - _lastFrameCount > _random.Next(80, 100))
        {
            var yShift = _random.Next(-20, 20);

            var trash = Instantiate(myPrefab, new Vector3(playerPosition.x - 20, playerPosition.y + yShift, playerPosition.z), Quaternion.identity);

            trash.AddComponent<CapsuleCollider>();

            trash.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
            trash.tag = "trash";
            _trashItems.Add(trash);

            _lastFrameCount = Time.frameCount;
        }

        _trashItems.RemoveAll(i => i.transform.position.x - playerPosition.x > 30);
    }
}
