using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AmmoManager : MonoBehaviour {

    public GameObject AmmoPrefab = null;

    public int PoolSize = 100;

    private GameObject[] AmmoArray;

    public static AmmoManager AmmoManagerSingleton = null;

    public Queue<Transform> AmmoQueue = new Queue<Transform>();

    private void Awake()
    {
        if(AmmoManagerSingleton != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        AmmoManagerSingleton = this;
    }
    private void Start()
    {
        AmmoArray = new GameObject[PoolSize];

        for(int i = 0; i < PoolSize; i++)
        {
            AmmoArray[i] = Instantiate(AmmoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
            Transform ObjTransform = AmmoArray[i].GetComponent<Transform>();
            ObjTransform.parent = transform;
            AmmoQueue.Enqueue(ObjTransform);
            AmmoArray[i].SetActive(false);
        }
    }
    
    public static Transform SpawnAmmo(Vector3 Position, Quaternion Rotation)
    {
        Transform SpawnedAmmo = AmmoManagerSingleton.AmmoQueue.Dequeue();

        SpawnedAmmo.gameObject.SetActive(true);
        SpawnedAmmo.position = Position;
        SpawnedAmmo.rotation = Rotation;
        //so this whole structure will go on each archer, becasue it is spawning on the archer, so each archer gets an ammo spawner
        AmmoManagerSingleton.AmmoQueue.Enqueue(SpawnedAmmo);

        //print(SpawnedAmmo);
        return SpawnedAmmo;

    }
}
