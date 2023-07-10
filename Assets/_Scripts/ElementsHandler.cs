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
    [SerializeField] private Transform attackPoint;

    [Tooltip("Number of seconds it takes for next attack")]
    [SerializeField] private float AttackSpeed = 3f;
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

        if (lastAttack >= AttackSpeed)
        {
            FireProjectile();
            lastAttack = 0;
        }
    }

    public void CanAttack(bool _canAttack)
    {
        bCanAttack = _canAttack;
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
