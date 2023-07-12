using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float touchSpeed = 3f;
    [SerializeField] private float maxX = 5;
    [SerializeField] private float minX = -5;
    private bool bIsMoving = false;

    [SerializeField] private ElementsHandler elementHandler;
    [SerializeField] private Animator animator;

    private bool bIsGameover = false;
    private bool bLevelCompelete = false;

    private int gemsCollected = 0;
    [SerializeField] private AudioClip gemsAudio;
    [SerializeField] private AudioSource gemsAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        bIsGameover = false;
        bLevelCompelete = false;
        bIsMoving = false;

        animator.SetBool("IsMoving", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsGameover)
            return;

#if UNITY_EDITOR

        if (Input.GetKey(KeyCode.W))
        {
            bIsMoving = true;
            elementHandler.CanAttack(true);
            animator.SetBool("IsMoving", true);
        }
        else
        {
            bIsMoving = false;
            elementHandler.CanAttack(false);
            animator.SetBool("IsMoving", false);
        }

        float _newPosX = transform.position.x + Input.GetAxis("Horizontal") * 5 * Time.deltaTime;

        //Mathf.Clamp(_newPosX, -2f, 2f);

        if (_newPosX > maxX)
        {
            _newPosX = maxX;
        }

        if (_newPosX < minX)
        {
            _newPosX = minX;
        }

        transform.position = new Vector3(_newPosX, transform.position.y, transform.position.z);

#endif


        PlayerMovement();

        if(bIsMoving) 
            transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    void PlayerMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    bIsMoving = true;
                    elementHandler.CanAttack(true);
                    animator.SetBool("IsMoving", true);
                    break;

                case TouchPhase.Moved:

                    float _newPosX = transform.position.x + touch.deltaPosition.x * touchSpeed * Time.deltaTime;

                    if (_newPosX > maxX)
                    {
                        _newPosX = maxX;
                    }

                    if (_newPosX < minX)
                    {
                        _newPosX = minX;
                    }

                    transform.position = new Vector3(_newPosX, transform.position.y, transform.position.z);

                    break;

                case TouchPhase.Ended:
                    bIsMoving = false;
                    elementHandler.CanAttack(false);
                    animator.SetBool("IsMoving", false);
                    break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.TryGetComponent(out Portal portal);

        if (portal != null)
        {
            elementHandler.UpdateElement(portal.ElementsType);

            Destroy(portal.gameObject);
        }
         
        if (other.CompareTag("Collectible"))
        {
            gemsCollected += 1;
            EventsManager.Instance.PickupCoin(gemsCollected);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            bLevelCompelete = true;
        }

        other.TryGetComponent(out Obstacles obstacle);


        if (!obstacle || obstacle.HasDisabledObstacles)
            return;

        EventsManager.Instance.GameOver(bLevelCompelete);

        Debug.LogError("GameOver");

        bIsGameover = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
