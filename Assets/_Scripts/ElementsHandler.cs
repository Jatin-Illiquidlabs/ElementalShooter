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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckObstacles();

        Debug.DrawRay(transform.position, transform.forward * 10, Color.green);
    }

    private void CheckObstacles()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 10f, obstaclesLayer))
        {
            hit.transform.TryGetComponent(out Obstacles obstacle);
            if (!obstacle)
                return;

            if (selectedElements != obstacle.ObstacleType)
            {
                return;
            }

            obstacle.DisableObstacles();

        }
    }
}
