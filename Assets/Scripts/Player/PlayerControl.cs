using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float speed = 10.0f;
    //[SerializeField] Tilemap destructableTileMap;

    Rigidbody2D rb;

    private void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        speed = speed * 10;
    }

    private void Update()
    {
        float vertical = Input.GetAxisRaw("Vertical") * speed;
        float horizontal = Input.GetAxisRaw("Horizontal") * speed;

        Vector2 movement = new Vector2(horizontal * speed, vertical * speed);
        movement *= Time.deltaTime;
        rb.velocity = movement;

        //TODO: Implement a cooldown, Should holding down increase attack animations?
        if (Input.GetButtonDown("Fire1"))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider != null)
            {
                Debug.Log("CLICKED " + hit.collider.name);
            }
            Attack(hit.collider.gameObject);
        }
    }

    private void Attack(GameObject go) 
    {
        //TODO: Finish: https://github.com/Oragoss/lich-keeper/blob/main/Assets/Scripts/Player/HighlightWall.cs
        if(Vector3.Distance(transform.position, go.transform.position) <= 2)
        {
            Destroy(go);
        }
    }
}
