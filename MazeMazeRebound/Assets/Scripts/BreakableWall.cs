using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    private Animator animator;
    private bool hasCollided = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            StartCoroutine(PlayAnimationAndDisappear());
            hasCollided = true;
        }
    }

    private IEnumerator PlayAnimationAndDisappear()
    {
        if (!hasCollided)
            animator.SetTrigger("Disappear");

        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);

        gameObject.SetActive(false);

    }
}
