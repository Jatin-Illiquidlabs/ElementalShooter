using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour
{
    [SerializeField] private ElementsTypes obstacleType;
    public ElementsTypes ObstacleType { get { return obstacleType; } }

    [SerializeField] private float destroyTime = 0f;


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
        
    }

    public void DisableObstacles()
    {
        Invoke(nameof(RemoveBlocker), destroyTime);

        ActivatePath();
    }

    private void RemoveBlocker()
    {

    }

    private void ActivatePath()
    {
        GetComponent<MeshRenderer>().enabled = true;

        bHasDisabledObstacle = true;
    }

}
