using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    public static void PlaySound(AudioClip audioclip, float volume)
    {
        GameObject audioGameObject = new GameObject("AudioSource");
        AudioSource audioSource = audioGameObject.AddComponent<AudioSource>();
        DestroyAfterDeath destroyScript = audioGameObject.AddComponent<DestroyAfterDeath>();
        audioSource.PlayOneShot(audioclip, volume);
    }
}
