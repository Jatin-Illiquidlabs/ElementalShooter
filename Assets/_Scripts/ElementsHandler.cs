using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ElementsTypes
{
    Ice,
    Fire,
    Pulse,
    Electric
}

public class ElementsHandler : MonoBehaviour
{
    [SerializeField] private ElementsTypes selectedElements;
    [SerializeField] private LayerMask obstaclesLayer;

    [SerializeField] private Projectile projectile;
    [SerializeField] private Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(FireProjectile), 0f ,3f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckObstacles();

        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);
    }


    public void FireProjectile()
    {
        Projectile spawnedProjectile = Instantiate(projectile, attackPoint.position, Quaternion.identity);
        spawnedProjectile.SpawnProjectile();
    }

    private void CheckObstacles()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, obstaclesLayer))
        {
            //hit.transform.TryGetComponent(out Obstacles obstacle);
            //if (!obstacle)
            //    return;

            //if (selectedElements != obstacle.ObstacleType)
            //{
            //    return;
            //}

            //obstacle.DisableObstacles();

        }
    }

}
