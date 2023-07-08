using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private ElementsTypes selectedElement;
    [SerializeField] private LayerMask obstaclesLayer;

    [SerializeField] private Projectile[] projectilesList;
    [SerializeField] private int currentProjectileIndex;
    [SerializeField] private Transform attackPoint;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(FireProjectile), 0f ,3f);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void FireProjectile()
    {
        Projectile spawnedProjectile = Instantiate(projectilesList[currentProjectileIndex], attackPoint.position, Quaternion.identity);
        spawnedProjectile.SpawnProjectile();
    }


    public void UpdateElement(ElementsTypes _newElement)
    {
        selectedElement = _newElement;

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
}
