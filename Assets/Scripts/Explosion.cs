using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private ParticleSystem explosion;

    private void Awake()
    {
        explosion = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (!explosion.isPlaying)
        {
            gameObject.SetActive(false);
        }
        
    }
    private void OnEnable()
    {
        explosion.Play();
    }
}
