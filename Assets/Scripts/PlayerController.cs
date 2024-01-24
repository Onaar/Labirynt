using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float speed = 12f;
    CharacterController characterController;
    [SerializeField]
    LayerMask groundMask;
    [SerializeField]
    Transform groundChecker;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        CheckTheGround();
        PlayerMove();
    }
    private void CheckTheGround()
    {
        RaycastHit hit;
        if(Physics.Raycast(groundChecker.position, Vector3.down, out hit, 0.4f, groundMask))
        {
            string terrainType = hit.collider.tag;
            switch (terrainType)
            {
                case "Low":
                    speed = 3f;
                    break;
                case "High":
                    speed = 20f;
                    break;
                default:
                    speed = 12f;
                    break;
            }
        }
    }
    private void PlayerMove()
    {
        float x = Input.GetAxis("Horizontal"),
            z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
    }
}
