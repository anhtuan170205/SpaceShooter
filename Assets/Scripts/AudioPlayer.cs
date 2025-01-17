using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [Header("Shooting")]
    [SerializeField] AudioClip shootingClip;
    [SerializeField] [Range(0, 1)] float shootingVolume = 1f;

    public void PlayShootingClip()
    {
        playClip(shootingClip, shootingVolume);
    }
    [Header("Hit")]
    [SerializeField] AudioClip hitClip;
    [SerializeField] [Range(0, 1)] float hitVolume = 1f;
    public static AudioPlayer instance;
    void Awake()
    {
        ManageSingleton();
    }
    void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PlayHitClip()
    {
        playClip(hitClip, hitVolume);
    }
    
    void playClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 position = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}
