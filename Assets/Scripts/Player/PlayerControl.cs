using Assets.Scripts.Resources;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Assets.Scripts.Managers;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] LayerMask ignoreLayerMask;

    Rigidbody2D rb;
    InventoryManager inventoryManager = new InventoryManager();

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ClickOnObject();
        TogglePlayerInventory();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void OnCollisionStay2D(Collision2D collision)
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
        if (go.CompareTag("Collectable") && go.GetComponent<ResourcePickupTimer>().canBePickedUp)
        {
            Debug.Log("Pulling " + go.name);
            Vector3 pos = transform.position - go.transform.position;
            go.GetComponent<Rigidbody2D>().velocity = pos.normalized * 5;
        }
    }

    private void PickUpCollectable(Collision2D collision)
    {
        try
        {
            var go = collision.gameObject;
            if (go.CompareTag("Collectable"))
            {
                if (go.GetComponent<ResourcePickupTimer>().canBePickedUp)
                {
                    Inventory newItem = new Inventory
                    {
                        Name = go.name,
                        Amount = 1, //Change this somewhere? Is it always 1?
                        Type = go.GetComponent<CollectableType>().type,
                        Sprite = go.GetComponent<CollectableType>().sprite
                    };

                    inventoryManager.AddItemToPlayerInventory(newItem);
                    UiManager.ui.UpdatePlayerInventory(inventoryManager.GetPlayerInventory());
                    Destroy(go);
                }
            }
        } 
        catch(Exception ex)
        {
            Debug.LogError($"Error occured trying to pick up a collectable: \n{ex.Message}");
        }
    }

    private void TogglePlayerInventory()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            UiManager.ui.TogglePlayerInventory();

            Debug.Log("TODO: open the UI screen and populate it with the items from the player's inventory");
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
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, ~ignoreLayerMask);
            //Physics2D.queriesHitTriggers = false;

            if (hit.collider != null)
            {
                //if (hit.collider.isTrigger)
                //{
                //    HarvestResource(hit.collider.gameObject);
                //}
                HarvestResource(hit.collider.gameObject);
            }

        }
    }

    private void HarvestResource(GameObject go)
    {
        if (Vector3.Distance(transform.position, go.transform.position) <= 3 && !go.CompareTag("Player"))
        {
            if (go.CompareTag("Wood") || go.CompareTag("Stone") || go.CompareTag("Crop"))
            {
                go.GetComponent<GenerateCollectable>().Generate();
                Destroy(go);
            }
        }
    }
}
