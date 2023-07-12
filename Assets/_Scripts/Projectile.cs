using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private ElementsTypes projectileType;
    public ElementsTypes ProjectileType { get { return projectileType; } }  
    [SerializeField] private ParticleSystem destroyFx;
    [SerializeField] private float speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    public void SpawnProjectile()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        other.transform.TryGetComponent(out Obstacles obstacle);
        if (!obstacle)
            return;

        if (projectileType != obstacle.ObstacleType)
        {
            Destroy(gameObject);
            return;
        }

        obstacle.DisableObstacles();

        if(destroyFx != null)
        {
            Instantiate(destroyFx, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
