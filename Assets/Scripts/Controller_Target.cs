using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Target : MonoBehaviour
{
    public int targetNumber;

    public bool playerOnTarget;

    private Animator animator;

    private void Start()
    {
        playerOnTarget = false;
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Controller_Player>().playerNumber == targetNumber)
            {
                animator.SetBool("Activa", true);
                playerOnTarget = true;
                //Debug.Log("P on T");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.GetComponent<Controller_Player>().playerNumber == targetNumber)
            {
                animator.SetBool("Activa", false);
                playerOnTarget = false;
                //Debug.Log("P off T");
            }
        }
    }
}
