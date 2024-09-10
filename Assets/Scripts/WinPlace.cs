using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinPlace : MonoBehaviour
{
    float scaleX, scaleY, alfa = 0f;

    private void Start()
    {
        scaleX = transform.localScale.x;
        scaleY = transform.localScale.y;
    }
    private void FixedUpdate()
    {
        float scale = Resizer();
        transform.localScale = new Vector3(scale, scaleY, scale);
    }
    private float Resizer()
    {
        float value = Mathf.Sin(alfa);
        alfa += (1.5f * Time.deltaTime);
        return value + scaleX;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameManager.instance.WinGame();
        }
    }
}
