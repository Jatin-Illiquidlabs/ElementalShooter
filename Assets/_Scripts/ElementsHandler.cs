using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public enum ElementsTypes
{
    Ice,
    Fire,
    Pulse,
    Lightning
}

public class ElementsHandler : MonoBehaviour
{
    [SerializeField] private List<ElementsTypes> selectedElement;

    [SerializeField] private Projectile[] projectilesList;
    [SerializeField] private int currentProjectileIndex;
    [SerializeField] private Transform[] attackPoint;
    [SerializeField] private LayerMask obstaclesLayer;

    [Tooltip("Number of seconds it takes for next attack")]
    [SerializeField] private float attackSpeed = 3f;
    [SerializeField] private int bulletsCount = 1;
    [SerializeField] private int bulletsMultipliers = 1;
    private float lastAttack = 0f;
    private bool bCanAttack = false;

    // Start is called before the first frame update
    void Start()
    {
        bCanAttack = false;

        //InvokeRepeating(nameof(FireProjectile), 0f ,3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!bCanAttack)
            return;

        CheckObstacles();
        Debug.DrawRay(transform.position, transform.forward * 5, Color.green);

        lastAttack += Time.deltaTime;

        if (lastAttack >= attackSpeed)
        {
            FireProjectile();

            for (int i = 0; i < bulletsMultipliers - 1; i++)
            {
                float waitTime = ((float)(i + 1) )/ 4;
                Invoke(nameof(FireProjectile), waitTime);
            }

            lastAttack = 0;
        }
    }

    private void CheckObstacles()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 5f, obstaclesLayer))
        {
            hit.transform.TryGetComponent(out Obstacles obstacle);

            if (!obstacle)
                return;

            UpdateElement(obstacle.ObstacleType);
        }
    }

    public void CanAttack(bool _canAttack)
    {
        bCanAttack = _canAttack;
    }

    public void FireProjectile()
    {
        for (int i = 0; i < bulletsCount; i++)
        {
            Projectile spawnedProjectile = Instantiate(projectilesList[currentProjectileIndex], attackPoint[i].position, Quaternion.identity);
            spawnedProjectile.SpawnProjectile();
        }
    }


    private void UpdateElement(ElementsTypes _newElement)
    {
        //selectedElement = _newElement;

        if (!selectedElement.Contains(_newElement))
            return;

        switch(_newElement)
        {
            case ElementsTypes.Ice:
                currentProjectileIndex = 0;
                break;

            case ElementsTypes.Fire:
                currentProjectileIndex = 1;
                break;

            case ElementsTypes.Lightning:
                currentProjectileIndex = 2;
                break;
        }

    }

    public void AddElement(ElementsTypes _newElement)
    {
        selectedElement.Add(_newElement);
    }
}
