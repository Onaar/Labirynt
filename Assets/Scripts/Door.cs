using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform door, closePos, openPos;
    public bool open = false;
    float speed = 5f;

    private void Start()
    {
        door.position = closePos.position;
    }
    public void Open()
    {
        open = true;
    }
    private void Update()
    {
        if (open && Vector3.Distance(door.position, openPos.position) > 0.001f )
        {
            door.position = Vector3.MoveTowards(door.position, openPos.position, speed * Time.deltaTime);
        }
        else if (!open && Vector3.Distance(door.position, closePos.position) > 0.001f )
        {
            door.position = Vector3.MoveTowards(door.position, closePos.position, speed * Time.deltaTime);
        }
    }
}
