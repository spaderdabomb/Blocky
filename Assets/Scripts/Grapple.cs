using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapple : MonoBehaviour
{
    [SerializeField] GameObject grappleChain;
    [SerializeField] GameObject grappleArrow;

    GameObject player;

    SpringJoint2D playerSpringJoint;

    Vector2 grappleEndPoint;
    Vector2 grappleArrowStartPoint;
    Vector3 playerPositionLastFrame;

    float rotation;
    float expansionRate = 12f;
    float grappleArrowX;
    float grappleArrowY;
    float totalExpansion = 0f;
    float startingDistanceFromEndPoint;

    bool reachedHook = false;
    bool grappling = false;

    void Start()
    {

    }

    public void InitializeGrapple(Vector2 _grappleEndPoint)
    {
        // Initialize values
        grappleEndPoint = _grappleEndPoint;
        player = GameObject.FindGameObjectWithTag("Player");
        playerPositionLastFrame = player.transform.position;
        startingDistanceFromEndPoint = Vector2.Distance(grappleEndPoint, (Vector2)player.transform.position);
        player.GetComponent<Player>().currentState = Player.PlayerState.Grappling;
        playerSpringJoint = player.GetComponent<SpringJoint2D>();
        grappleArrowStartPoint = grappleArrow.transform.localPosition;

        // Get rotation
        transform.position = player.transform.position;
        Vector2 diffVector = _grappleEndPoint - new Vector2(transform.position.x, transform.position.y);
        float rotation = Mathf.Atan2(diffVector.y, diffVector.x) * 180f / Mathf.PI - 90f;

        // Set some initial values
        grappleChain.transform.eulerAngles = Vector3.forward * rotation;
        grappleArrow.transform.eulerAngles = Vector3.forward * rotation;
        grappleArrowX = expansionRate * Time.fixedDeltaTime * Mathf.Cos(Mathf.Deg2Rad * (rotation + 90));
        grappleArrowY = expansionRate * Time.fixedDeltaTime * Mathf.Sin(Mathf.Deg2Rad * (rotation + 90));

        // Account for offset in grapple arrow position based on rotation
        float tempX = grappleArrowStartPoint.magnitude * Mathf.Cos(Mathf.Deg2Rad * (rotation + 90));
        float tempY = grappleArrowStartPoint.magnitude * Mathf.Sin(Mathf.Deg2Rad * (rotation + 90));
        grappleArrow.transform.localPosition = new Vector2(tempX, tempY);
    }

    void Update()
    {
        if (!reachedHook)
        {
            
        }
        else if (reachedHook && grappling)
        {
            //playerSpringJoint.anchor = player.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (!reachedHook)
        {
            // Get new values
            totalExpansion += expansionRate * Time.fixedDeltaTime;
            float playerDistanceFromTarget = Vector2.Distance(grappleEndPoint, (Vector2)player.transform.position);

            // Get rotation
            transform.localPosition = player.transform.localPosition;
            Vector2 diffVectorTemp = grappleEndPoint - new Vector2(player.transform.position.x, player.transform.position.y);
            float rotation = Mathf.Atan2(diffVectorTemp.y, diffVectorTemp.x) * 180f / Mathf.PI - 90f;
            grappleChain.transform.eulerAngles = Vector3.forward * rotation;
            grappleArrow.transform.eulerAngles = Vector3.forward * rotation;

            // Calculate new chain width and height
            float newHeight = (playerDistanceFromTarget - startingDistanceFromEndPoint) + totalExpansion;
            grappleChain.GetComponent<SpriteRenderer>().size = new Vector2(grappleChain.GetComponent<SpriteRenderer>().size.x, newHeight);

            // Calculate new position of grapple arrow
            grappleArrowX = newHeight * Mathf.Cos(Mathf.Deg2Rad * (rotation + 90));
            grappleArrowY = newHeight * Mathf.Sin(Mathf.Deg2Rad * (rotation + 90));
            float grappleArrowPosX = grappleArrowStartPoint.x + grappleArrowX;
            float grappleArrowPosY = grappleArrowStartPoint.y + grappleArrowY;
            grappleArrow.transform.localPosition = new Vector2(grappleArrowPosX, grappleArrowPosY);

            playerPositionLastFrame = player.transform.position;
        }
        else if (reachedHook && grappling)
        {
            float playerDistanceFromTarget = Vector2.Distance(grappleEndPoint, (Vector2)player.transform.position);

            // Get rotation
            transform.localPosition = player.transform.localPosition;
            Vector2 diffVectorTemp = grappleEndPoint - new Vector2(player.transform.position.x, player.transform.position.y);
            float rotation = Mathf.Atan2(diffVectorTemp.y, diffVectorTemp.x) * 180f / Mathf.PI - 90f;
            grappleChain.transform.eulerAngles = Vector3.forward * rotation;
            grappleArrow.transform.eulerAngles = Vector3.forward * rotation;

            // Calculate new chain width and height
            float newHeight = playerDistanceFromTarget;
            grappleChain.GetComponent<SpriteRenderer>().size = new Vector2(grappleChain.GetComponent<SpriteRenderer>().size.x, newHeight);

            // Calculate new position of grapple arrow
            grappleArrowX = newHeight * Mathf.Cos(Mathf.Deg2Rad * (rotation + 90));
            grappleArrowY = newHeight * Mathf.Sin(Mathf.Deg2Rad * (rotation + 90));
            float grappleArrowPosX = grappleArrowStartPoint.x + grappleArrowX;
            float grappleArrowPosY = grappleArrowStartPoint.y + grappleArrowY;
            grappleArrow.transform.localPosition = new Vector2(grappleArrowPosX, grappleArrowPosY);

            playerPositionLastFrame = player.transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ceiling"))
        {
            reachedHook = true;
            grappling = true;
            BeginGrapple();
            grappleEndPoint = grappleArrow.transform.position;
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Damage(player.GetComponent<Player>().grappleDamage);
        }
    }

    void BeginGrapple()
    {
        playerSpringJoint = player.GetComponent<SpringJoint2D>();
        playerSpringJoint.connectedBody = grappleArrow.GetComponent<Rigidbody2D>();
        playerSpringJoint.connectedAnchor = grappleArrow.transform.position;
        playerSpringJoint.enabled = true;
    }

    public void EndGrapple()
    {
        playerSpringJoint = player.GetComponent<SpringJoint2D>();
        playerSpringJoint.connectedBody = null;
        playerSpringJoint.enabled = false;
        Destroy(gameObject);
        player.GetComponent<Player>().currentState = Player.PlayerState.Walking;
    }
}
