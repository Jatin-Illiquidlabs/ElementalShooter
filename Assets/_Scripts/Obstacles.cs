using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Obstacles : MonoBehaviour
{
    [Header("Obstacles")]
    [SerializeField] private GameObject blockerObject;
    [SerializeField] private GameObject clearPathObject;

    [Header("Details")]
    [SerializeField] private ElementsTypes obstacleType;
    public ElementsTypes ObstacleType { get { return obstacleType; } }

    [SerializeField] private float destroyTime = 0f;
    [SerializeField] private bool bCanUpdateMaterial = false;
    [SerializeField] private bool bIsMoving = false;
    [SerializeField] private string changeValueName;
    [SerializeField] private ParticleSystem destroyFx;
    [SerializeField] private Transform damagePoint;
    [SerializeField] private Animator animator;
    public Transform DamagePoint { get { return damagePoint; } }

    private float changeValue = -1;
    private bool bStartChangingMaterial = false;

    private Material materialToUpdate;


    private bool bHasDisabledObstacle = false;
    public bool HasDisabledObstacles { get { return bHasDisabledObstacle; } }

    // Start is called before the first frame update
    void Start()
    {
        bHasDisabledObstacle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bStartChangingMaterial)
        {
            changeValue += Time.deltaTime * 2;

            materialToUpdate.SetFloat("_"+changeValueName, changeValue);
            
        }
    }

    public void DisableObstacles()
    {
        if (bCanUpdateMaterial)
        {
            materialToUpdate = clearPathObject.GetComponentInChildren<MeshRenderer>().material;
            bStartChangingMaterial = true;
        }

        if (bIsMoving && animator != null)
        {
            animator.StopPlayback();
            animator.enabled = false;
        }

        Invoke(nameof(RemoveBlocker), destroyTime);

        ActivatePath();
    }

    private void RemoveBlocker()
    {
        if (!blockerObject)
            return;

        blockerObject.SetActive(false);
    }

    private void ActivatePath()
    {
        bHasDisabledObstacle = true;

        if (destroyFx != null)
        {
            //destroyFx.Play();
            Destroy(destroyFx, 2f);
        }

        if (!clearPathObject)
            return;

        clearPathObject.SetActive(true);
    }

}
