using System;
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
    [SerializeField]
    Material redMat, greenMat, goldMat;
    [SerializeField]
    Renderer myLock;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = true;
            if (!locked)
            {
                GameManager.instance.SetGamePanelActiveness(true);
                Debug.Log("You can use the lock!");
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            iCanOpen = false;
            GameManager.instance.SetGamePanelActiveness(false);
            Debug.Log("You cannot use the lock!");
        }
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        SetMyColor();
    }
    private void SetMyColor()
    {
        switch (myColor)
        {
            case KeyColor.Red:
                myLock.material = redMat;
                SetKeyMaterial(redMat);
                break;
            case KeyColor.Green:
                myLock.material = greenMat;
                SetKeyMaterial(greenMat);
                break;
            case KeyColor.Gold:
                myLock.material = goldMat;
                SetKeyMaterial(goldMat);
                break;
        }
    }
    private void SetKeyMaterial(Material targetMat)
    {
        Renderer keyRend = GetComponent<Renderer>();
        Material[] materials = keyRend.materials;
        materials[1] = targetMat;
        keyRend.materials = materials;
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
            GameManager.instance.UpdateKeysText(myColor);
            locked = true;
            return true;
        }
        else if(GameManager.instance.greenKey > 0 && myColor == KeyColor.Green)
        {
            GameManager.instance.greenKey--;
            GameManager.instance.UpdateKeysText(myColor);
            locked = true;
            return true;
        }
        else if(GameManager.instance.goldKey > 0 && myColor == KeyColor.Gold)
        {
            GameManager.instance.goldKey--;
            GameManager.instance.UpdateKeysText(myColor);
            locked = true;
            return true;
        }
        
        Debug.Log("You dont have proper key!");
        return false;
    }
}
