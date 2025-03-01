using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LV_BulletPatrol : MonoBehaviour
{
    public float speed = 3f;
    private GameObject player = null;
    private Transform target;

    private Color[] colors = { new Color32(162,52,25,255), new Color32(244,187,15,255), new Color32(47,55,91,255)};
    private Color currentColor;
    private Color playerColor;

    // Distance between bullet & player
    private float distance;
    public float detectRange = 4f;

    // Variables for patroling
    public Transform[] waypoints;   // Given patroling waypoints
    private int pointIndex = 0;
    private float waitTime = 1f;    // patroling the spot for 1 second
    public float startWaitTime = 3f;

    // Start is called before the first frame update
    void Start()
    {   
        // Initial setup
        currentColor =  colors[Random.Range(0, colors.Length)];
        gameObject.GetComponent<SpriteRenderer>().color = currentColor;

        // Get player's transform and color
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            target = player.transform;
            Debug.Log("playerColor = " + playerColor);
        }
    }

    // Update is called once per frame
    void Update()
    {

        distance = Vector3.Distance(target.position, transform.position);
        // Attack when player is within distance
        if (distance <= detectRange)
        {
            foundPlayer();
        }
        // Continue patrol
        else 
        {
            patroling();
        }
      
    }

    private void patroling()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[pointIndex].position, speed * Time.deltaTime);
        if (waitTime <= 0)
        {
            pointIndex++;
            if (pointIndex == waypoints.Length)
            {
                pointIndex = 0;
            }
            waitTime = startWaitTime;   // reset
        }
        else
        {
            waitTime -= Time.deltaTime;
        }

    }

    private void foundPlayer()
    {
        // detecting playerColor
        playerColor = player.GetComponent<SpriteRenderer>().color;

        // When playerColor is not the same as currentColor
        if (currentColor != playerColor)
        {
            // Face forward to player
            Vector2 direction = target.transform.position - gameObject.transform.position;
            direction = direction.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);

            // Move toward to player
            float towardStep = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, towardStep);
        }
        // When playerColor is the same as currentColor
        else 
        {
            // Face opposite to player's direction
            Vector2 direction = target.transform.position - gameObject.transform.position;
            direction = direction.normalized;
            transform.rotation = Quaternion.FromToRotation(Vector3.up, -direction);

            // Run away from player
            float towardStep = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, -target.position, towardStep);
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        playerColor = player.GetComponent<SpriteRenderer>().color;

        // Only destroy when being hitted by the same playerColor
        if (other.gameObject == player && currentColor == playerColor)
        {
            Destroy(gameObject);
        }
    }
}
