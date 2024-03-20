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
