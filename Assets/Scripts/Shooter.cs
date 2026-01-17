using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifetime = 5f;
    [SerializeField] float firingRate = 0.2f;
    [Header("AI")]
    [SerializeField] bool useAI;
    [SerializeField] float minimumFirerate = 0.5f;
    [SerializeField] float firerateVariance = 0.2f;

    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;
    AudioPlayer audioPlayer;
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    void Start()
    {
        if (useAI)
        {
            isFiring = true;
        }
    }

    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if(!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while(true)
        {
            if (useAI)
            {
                firingRate = Random.Range(firingRate - firerateVariance, firingRate + firerateVariance);
                firingRate = Mathf.Clamp(firingRate, minimumFirerate, float.MaxValue);
            }
            GameObject projectile = Instantiate(projectilePrefab, 
                                            transform.position, 
                                            Quaternion.identity);

            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.linearVelocity = transform.up * projectileSpeed;
            }
            Destroy(projectile, projectileLifetime);
            audioPlayer.PlayShootingClip();
            yield return new WaitForSeconds(firingRate);
        }
    }
}
