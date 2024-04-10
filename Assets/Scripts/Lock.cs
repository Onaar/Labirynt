using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    bool iCanOpen = false;
    public Door door;
    public KeyColor myColor;
    bool locked = false;
    Animator animator;
    [SerializeField]
    KeyCode interactionKey = KeyCode.E;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = true;
            Debug.Log("You can use the lock!");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = false;
            Debug.Log("You cannot use the lock!");
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(interactionKey) && iCanOpen && !locked)
        {
            animator.SetBool("useKey", CheckTheKey());
        }
    }
    public void UseKey()
    {
        door.Open();
    }
    public bool CheckTheKey()
    {
        if(GameManager.instance.redKey > 0 && myColor == KeyColor.Red)
        {
            GameManager.instance.redKey--;
            locked = true;
            return true;
        }
        else if(GameManager.instance.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.instance.greenKey--;
            locked = true;
            return true;
        }
        else if(GameManager.instance.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.instance.goldKey--;
            locked = true;
            return true;
        }
        
        Debug.Log("You dont have proper key!");
        return false;
    }
}
