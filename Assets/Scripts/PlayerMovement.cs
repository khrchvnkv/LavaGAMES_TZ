using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Transform startTransform;  //Точка телепорта
    [SerializeField] private Transform finishTransform; //Точка Respawn'a

    private Animator playerAnimator;
    [SerializeField] private float speed = 5.0f;
    public bool isJumping = false;

    private bool isTeleport = false;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!isJumping)
            Moving();
    }

    private void Moving()
    {
        transform.position = Vector3.MoveTowards(transform.position, finishTransform.position, speed * Time.deltaTime);

        if(Vector3.Distance(transform.position, finishTransform.position) < 2.5f && !isTeleport)
        {
            isTeleport = true;
            if (playerAnimator == null)
                Debug.LogError("The Player doesn't have an Animator component");
            else
                StartCoroutine(Teleport());
        }

    }

    private IEnumerator Teleport()
    {
        playerAnimator.SetTrigger("startTeleport");
        yield return new WaitForSeconds(0.5f);
        transform.position = startTransform.position;
        playerAnimator.SetTrigger("endTeleport");
        isTeleport = false;
    }
}
