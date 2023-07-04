using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float touchSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR

        float _newPosX = transform.position.x + Input.GetAxis("Horizontal") * 5 * Time.deltaTime;

        //Mathf.Clamp(_newPosX, -2f, 2f);

        //if (_newPosX > 3.25f)
        //{
        //    _newPosX = 3.25f;
        //}

        //if (_newPosX < -3.25f)
        //{
        //    _newPosX = -3.25f;
        //}

        transform.position = new Vector3(_newPosX,
            transform.position.y, transform.position.z);


#endif

        transform.Translate(transform.forward * speed * Time.deltaTime);
    }
}
