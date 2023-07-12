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

    [SerializeField] private Projectile[] projectilesList;
    [SerializeField] private int currentProjectileIndex;
    [SerializeField] private Transform[] attackPoint;

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
