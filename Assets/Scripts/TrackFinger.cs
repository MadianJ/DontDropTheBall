using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
public class TrackFinger : SingletonPersistent<TrackFinger>
{

    
    [Header("Hit Box Values")]
    [SerializeField]
    private GameObject hitBox;
    [SerializeField]
    private float timeForHit;

    [Header("References")]
    [SerializeField]
    private Rigidbody2D rb;


    [Header("Score Values")]
    [SerializeField]
    private int currentScore;
    [SerializeField]
    private Text scoreText;

    [Header("VFX")]
    [SerializeField]
    private GameObject hitVFX;

    [Header("Magic Wand")]
    [SerializeField]
    private GameObject wand;
    [SerializeField]
    private float wandOffsetZ;

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown += SpawnHitBox;
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
        UnityEngine.InputSystem.EnhancedTouch.Touch.onFingerDown -= SpawnHitBox;
    }

   
    private void SpawnHitBox(Finger finger) 
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(finger.screenPosition);
        pos.z = -10;
        GameObject hB = Instantiate(hitBox, pos, Quaternion.identity);
        Destroy(hB, timeForHit);
    }





    public void UpdateScore(bool increment = false)
    {
        if (increment)
            currentScore++;
        scoreText.text = "" + currentScore;
    }

    /// <summary>
    /// Resets Score when player loses
    /// </summary>
    public void ResetScore()
    {
        //If current score is higer save it as new highscore
        if (currentScore > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", currentScore);
        //Set score to 0 than update UI
        currentScore = 0;
        UpdateScore();
    }


    public void FreezeBallToggle(bool freeze)
    {
        if(freeze)
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        else
            rb.constraints = RigidbodyConstraints2D.None;

    }

    public void PlayVFX(Transform trans)
    {
        //Get the length X 
        float xLength = trans.position.x - wand.transform.position.x;
        //Get the length Y
        float yLength = trans.position.y - wand.transform.position.y;
        //Get angle
        float angle = Mathf.Atan2(yLength, xLength) * Mathf.Rad2Deg;

        wand.transform.rotation = Quaternion.Euler(0f, 0f, angle + wandOffsetZ);


        Vector3 pos = new Vector3(trans.position.x, trans.position.y, 0f);
        Quaternion rot = Quaternion.Euler(0f, 0f, 90f);
        ParticleSystem ps = Instantiate(hitVFX, pos, rot).GetComponent<ParticleSystem>();

        
    }
}
