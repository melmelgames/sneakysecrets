using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{

    [SerializeField] private bool hasDocuments;
    [SerializeField] private bool topSneaky;


    // Start is called before the first frame update
    void Start()
    {
        hasDocuments = true;
    }

    private void Update()
    {
        if (!hasDocuments)
        {
            gameObject.SetActive(false);
        }
    }

    public GameObject StealDocuments()
    {
        if (hasDocuments)
        {
            if (topSneaky)
            {
                GameObject documents =  ObjectPooler.instance.SpawnFromPool("topSneaky", gameObject.transform.position, Quaternion.identity);
                hasDocuments = false;
                return documents;
            }
            if (!topSneaky)
            {
                int rdnIdx = Random.Range(0, 3);
                switch (rdnIdx)
                {
                    case 0:
                        //Spawn redacted document
                        GameObject documents = ObjectPooler.instance.SpawnFromPool("redacted", gameObject.transform.position, Quaternion.identity);
                        hasDocuments = false;
                        return documents;
                    case 1:
                        //Spawn secret document
                        documents = ObjectPooler.instance.SpawnFromPool("secret", gameObject.transform.position, Quaternion.identity);
                        hasDocuments = false;
                        return documents;
                    case 2:
                        //Spawn top secret document
                        documents = ObjectPooler.instance.SpawnFromPool("topSecret", gameObject.transform.position, Quaternion.identity);
                        hasDocuments = false;
                        return documents;
                }
            }
        }
        return null;
    }

}
