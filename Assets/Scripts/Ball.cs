using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters

    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 10f;
    [SerializeField] Boolean randomizeSounds = false;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float randomMovementFactor = 0.2f;

    // state
    Vector2 paddleToBallVector;
    Boolean hasLaunched = false;

    // Cached component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody2D;
    Level level;
    Paddle paddle;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myRigidBody2D.simulated = false;

        paddle = FindObjectOfType<Paddle>();

        PlaceBallOnPaddle();

        paddleToBallVector = transform.position - paddle.transform.position;
 
        myAudioSource = GetComponent<AudioSource>();

        level = FindObjectOfType<Level>();
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
            level.BallLaunched();
            myRigidBody2D.velocity = new Vector2(xPush, yPush);
            myRigidBody2D.simulated = true;
        }
    }
    private void PlaceBallOnPaddle()
    {
        //https://stackoverflow.com/questions/25752429/how-to-get-height-of-2d-gamobject-with-sprite-renderer-component-in-unity3d-4-5
        float ballHeight = transform.localScale.y * GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        float paddleHeight = paddle.transform.localScale.y * paddle.GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        transform.position = new Vector3(paddle.transform.position.x, paddleHeight + ballHeight / 2);
    }
    private void LockToPaddle()
    {
        // Get Paddle Position. 
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
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
        Vector2 velocityTweak = new Vector2
            (UnityEngine.Random.Range(0, randomMovementFactor),
            UnityEngine.Random.Range(0, randomMovementFactor));

        if (hasLaunched)
        {
            myRigidBody2D.velocity += velocityTweak;

            PlaySFX();
        }
        
    }
}
