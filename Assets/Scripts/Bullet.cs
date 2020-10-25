using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private ObjectPooler objectPooler;

    // Start is called before the first frame update
    void Start()
    {
        objectPooler = ObjectPooler.instance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject explosion = objectPooler.SpawnFromPool("explosion", transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }

}
