using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public static void PlaySound(string tag, Vector3 position, float volume)
    {
        GameObject audioGameObject = ObjectPooler.instance.SpawnFromPool(tag, position, Quaternion.identity);
        AudioSource audioSource = audioGameObject.GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.Play();
    }
}
