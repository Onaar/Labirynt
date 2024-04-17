using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public virtual void Picked()
    {
        Debug.Log("Picked");
        GameManager.instance.PlayClip(GameManager.instance.pickedClip);
        Destroy(gameObject);
    }
}
