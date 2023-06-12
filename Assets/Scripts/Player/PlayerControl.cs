using Assets.Scripts.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] LayerMask ignoreLayerMask;
    [SerializeField] float howLongToWaitBeforePullingItems = 1.0f;

    Rigidbody2D rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ClickOnObject();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(collision);
        PickUpCollectable(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        PullItemsToPlayer(collision);
    }

    //TODO: Put in some kind of delay
    private void PullItemsToPlayer(Collider2D collision)
    {
        var go = collision.gameObject;
        if (go.CompareTag("Collectable"))
        {
            Debug.Log("Pulling " + go.name);
            Vector3 pos = transform.position - go.transform.position;
            go.GetComponent<Rigidbody2D>().velocity = pos.normalized * 5;
        }
    }

    //private IEnumerator WaitToPullItemsToPlayer(GameObject go)
    //{
    //    //Print the time of when the function is first called.
    //    Debug.Log("Started Coroutine at timestamp : " + Time.time);

    //    //yield on a new YieldInstruction that waits for 5 seconds.
    //    yield return new WaitForSeconds(howLongToWaitBeforePullingItems);
    //    Vector3 pos = transform.position - go.transform.position;
    //    go.GetComponent<Rigidbody2D>().velocity = pos.normalized * 5;

    //    //After we have waited 5 seconds print the time again.
    //    Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    //}

    private void PickUpCollectable(Collision2D collision)
    {
        var go = collision.gameObject;
        if (go.CompareTag("Collectable"))
        {
            Debug.Log("Picked up " + go.name + " it's a " + go.GetComponent<CollectableType>().type);
            Destroy(go);
        }
    }

    private void TakeDamage(Collision2D collision)
    {
        
    }

    private void Movement()
    {
        float vertical = Input.GetAxisRaw("Vertical") * speed;
        float horizontal = Input.GetAxisRaw("Horizontal") * speed;

        Vector2 movement = new Vector2(horizontal * speed, vertical * speed);
        movement *= Time.deltaTime;
        rb.velocity = movement;
    }

    private void ClickOnObject()
    {
        //TODO: Implement a cooldown, Should holding down increase attack animations?
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, ~ignoreLayerMask);
            Physics2D.queriesHitTriggers = false;

            if (hit.collider != null)
            {
                HarvestResource(hit.collider.gameObject);
            }
        }
    }

    private void HarvestResource(GameObject go)
    {
        if (Vector3.Distance(transform.position, go.transform.position) <= 2 && !go.CompareTag("Player"))
        {
            if (go.CompareTag("Wood") || go.CompareTag("Stone"))
            {
                go.GetComponent<GenerateCollectable>().Generate();
                Destroy(go);
            }
        }
    }
}
