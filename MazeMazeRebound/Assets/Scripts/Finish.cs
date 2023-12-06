using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public static event EventHandler OnFinishTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ball>())
        {
            OnFinishTrigger?.Invoke(this, EventArgs.Empty);
            GameManager.Instance.SetGameOver(true);
        }
    }

}
