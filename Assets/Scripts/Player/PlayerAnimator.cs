using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerAC;

    void Start()
    {

    }

    void Update()
    {

    }
    public void ManageAnimations(Vector3 move)
    {
        if (move.magnitude > 0)
        {
            PlayRunAnimation();

            playerAC.transform.forward = move.normalized;
        }
        else
        {
            PlayIdleAnimation();    
        }
    }
    private void PlayRunAnimation()
    {
        playerAC.Play("RUN");    
    }
    private void PlayIdleAnimation()
    {
        playerAC.Play("IDLE");
    }
}
