using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovingPlatform : MonoBehaviour
{
    [Header("Game Objects")]
    public Transform[] wayPoints;
    public GameManager gameManager;

    [Header("Movement Stats")]
    public float speed;
    public int startingPoint;
    private int i;

    [Header("Checks")]
    public bool isRiserPlatform;
    private bool isRiserActivated = false;

    private void Start()
    {
        transform.position = wayPoints[startingPoint].position;
    }

    private void Update()
    {
        if (!isRiserPlatform || (isRiserPlatform && isRiserActivated))
        {
            if (Vector2.Distance(transform.position, wayPoints[i].position) < 0.02f)
            {
                i++;
                if (i == wayPoints.Length)
                {
                    i = 0;
                }
            }
            if (!isRiserPlatform)
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[i].position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, wayPoints[1].position, speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
        if (isRiserPlatform)
        {
            isRiserActivated = true;
            gameManager.LevelTwo();
        }           
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!isRiserPlatform)
        {
            collision.transform.SetParent(null);
        }
        else
        {
            collision.transform.SetParent(null);
            Destroy(gameObject);
        }
    }
}
