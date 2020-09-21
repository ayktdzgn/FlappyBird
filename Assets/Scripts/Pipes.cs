using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Pipes : MonoBehaviour,IPooledObject
{
    public float speed = -1f;

    public void OnObjectSpawn()
    {
        Vector3 force = new Vector3(speed, 0, 0);
        GetComponent<Rigidbody2D>().velocity = force;
    }
}
