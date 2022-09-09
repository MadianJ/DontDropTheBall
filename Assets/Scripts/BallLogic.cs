using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Rigidbody2D rb;

    [Header("Speed Values")]
    [SerializeField]
    private float speedY;
    [SerializeField]
    private float speedX;

    [Header("Rotate Values")]
    [SerializeField]
    private float rotateBySpeed = 1;
    private float rotateBy;

    #region Unity Methods

    private void FixedUpdate()
    {
        if (rb.velocity.x > 0)
        {
            rotateBy -= Time.deltaTime * rotateBySpeed;
            rb.MoveRotation(rotateBy);
        }
        else if (rb.velocity.x < 0)
        {
            rotateBy += Time.deltaTime * rotateBySpeed;
            rb.MoveRotation(rotateBy);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!MainMenu.Instance.inGame)
            MainMenu.Instance.ToGamePlay();
        TrackFinger.Instance.PlayVFX(collision.transform);
        Vector2 dir = new Vector2(0, speedY);
        if (collision.gameObject.transform.position.x > gameObject.transform.position.x)
        {
            //TODO: Move slightly in this direction
            dir.x = -speedX;
        }
        else
        {
            dir.x = speedX;
        }
        rb.AddForce(dir);
        Destroy(collision.gameObject);
        
        TrackFinger.Instance.UpdateScore(true);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Bottom")
        {
            TrackFinger.Instance.ResetScore();
            gameObject.transform.position = Vector2.zero;
            MainMenu.Instance.ToMainMenu();
            
        }
    }

    #endregion
}
