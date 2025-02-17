﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    // Rigidbody 2D bola
    private Rigidbody2D rigidbody2D;

    // Besarnya gaya awal yang diberikan untuk mendorong bola
    public float xInitialForce = 50;
    public float yInitialForce = 15;
    public float speed = 10;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        trajectoryOrigin = transform.position;
        time = Time.deltaTime;

        // Mulai game
        RestartGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetBall()
    {
        // Reset posisi menjadi (0,0)
        transform.position = Vector2.zero;

        // Reset kecepatan menjadi (0,0)
        rigidbody2D.velocity = Vector2.zero;
    }

    void PushBall()
    {
        // Tentukan nilai komponen y dari gaya dorong antara -yInitialForce dan yInitialForce
        float yRandomInitialForce = Random.Range(-yInitialForce, yInitialForce) * time;
        // Tentukan nilai acak antara 0 (inklusif) dan 2 (eksklusif)
        float randomDirection = Random.Range(0, 2);

        // Jika nilainya dibawah 1, bola bergerak ke kiri
        // Jika tidak, bola bergerak ke kanan
        if (randomDirection < 1.0f)
        {
            // Gunakan gaya untuk menggerakkan bola ini
            rigidbody2D.AddForce(new Vector2(-xInitialForce, yRandomInitialForce * speed * time));
        }
        else
        {
            rigidbody2D.AddForce(new Vector2(xInitialForce, yRandomInitialForce * speed * time));
        }
    }

    void RestartGame()
    {
        // Kembalikan bola ke posisi semula
        ResetBall();

        // Setelah 2 detik, berikan gaya ke bola
        Invoke("PushBall", 2);
    }

    // Debug
    // Titik asal lintasan bola saat ini
    private Vector2 trajectoryOrigin;

    // Ketika bola beranjak dari sebuah tumbuka, rekam titik tumbukan tersebut
    private void OnCollisionExit2D(Collision2D collision)
    {
        trajectoryOrigin = transform.position;
    }

    // Untuk mengakses informasi titik asal lintasan
    public Vector2 TrajectoryOrigin
    {
        get { return trajectoryOrigin; }
    }
}
