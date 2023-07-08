using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float touchSpeed = 5f;
    [SerializeField] private float maxX = 5;
    [SerializeField] private float minX = -5;

    [SerializeField] private ElementsHandler elementHandler;

    private bool bIsGameover = false;
    private bool bLevelCompelete = false;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TMP_Text gemsText;


    private int gemsCollected = 0;
    [SerializeField] private AudioClip gemsAudio;
    [SerializeField] private AudioSource gemsAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        bIsGameover = false;
        bLevelCompelete = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bIsGameover)
            return;

#if UNITY_EDITOR

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
        transform.Translate(transform.forward * speed * Time.deltaTime);
    }

    void PlayerMovement()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Moved:

                    float _newPosX = transform.position.x + touch.deltaPosition.x * touchSpeed * Time.deltaTime;

                    if (_newPosX > 3.25f)
                    {
                        _newPosX = 3.25f;
                    }

                    if (_newPosX < -3.25f)
                    {
                        _newPosX = -3.25f;
                    }

                    transform.position = new Vector3(_newPosX, transform.position.y, transform.position.z);

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
        }

        if (other.CompareTag("Collectible"))
        {
            gemsCollected += 1;
            gemsAudioSource.PlayOneShot(gemsAudio);
            Destroy(other.gameObject);
        }

        if (other.CompareTag("Finish"))
        {
            bLevelCompelete = true;
        }

        other.TryGetComponent(out Obstacles obstacle);


        if (!obstacle || obstacle.HasDisabledObstacles)
            return;

        if (bLevelCompelete)
        {
            gemsText.text = gemsCollected.ToString();
            gameOverUI.SetActive(true);
        }

        Debug.LogError("GameOver");

        bIsGameover = true;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
