using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObject : MonoBehaviour
{
    public GameObject objectToDisappear; 

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.GetComponent<Ball>() || other.GetComponent<BallShadow>())
        {
            if (objectToDisappear != null)
            {
                objectToDisappear.SetActive(false);
            }
        }
    }
}
