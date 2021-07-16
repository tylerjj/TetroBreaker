using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 10f;
    [SerializeField] Boolean randomizeSounds = false;
    [SerializeField] AudioClip[] ballSounds;
    
    // state
    Vector2 paddleToBallVector;
    Boolean hasLaunched = false;

    // Cached component references
    AudioSource myAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
        paddleToBallVector = transform.position - paddle1.transform.position;
        GetComponent<Rigidbody2D>().simulated = false;
        
        myAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasLaunched)
        {
            // Keep Ball Glued to Paddle
            LockToPaddle();
            // On MouseClick, Launch Ball. 
            LaunchOnMouseClick();
        }
    }

    private void LaunchOnMouseClick()
    {
        // On left-click
        if (Input.GetMouseButtonDown(0))
        {
            hasLaunched = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush, yPush);
            GetComponent<Rigidbody2D>().simulated = true;
        }
    }

    private void LockToPaddle()
    {
        // Get Paddle Position. 
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        // Update Ball Position relative to Paddle Position. 
        transform.position = paddlePos + paddleToBallVector;
    }
    private void PlaySFX()
    {
        if (randomizeSounds)
        {
            // Get a random audio clip from our ball sounds. 
            AudioClip clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];

            // Play SFX when ball bounces into things. 
            // (PlayOneShot makes sure that our sfx play to
            // completion, as opposed to being replaced by whatever
            // clip is played next.)
            myAudioSource.PlayOneShot(clip);
        }
        else
        {
            myAudioSource.pitch = UnityEngine.Random.Range(.9f, 1.1f);
            myAudioSource.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasLaunched)
        {
            PlaySFX();
        }
        
    }
}
