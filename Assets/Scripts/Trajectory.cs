using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    // Srip, collider, dan rigitbody bola
    public BallControl ball;
    CircleCollider2D ballCollider;
    Rigidbody2D ballRigitbody;

    // Bola "bayangan" yang akan ditampilkan di titik tumbukan
    public GameObject ballAtCollision;

    // Start is called before the first frame update
    void Start()
    {
        // Inisialisasi rigitbody dan collider
        ballRigitbody = ball.GetComponent<Rigidbody2D>();
        ballCollider = ball.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Inisialisasi status pantulan lintasan, yang hanya akan ditempatkan jika lintasan bertumbukan dengan objek tertentu
        bool drawBallAtCollision = false;

        // Titik tumbukan yang digeser, untuk menggambar ballAtCollision
        Vector2 offsetHitPoint = new Vector2();

        // Tentukan titik tumbukan dengan deteksi pergerakan lingkaran
        RaycastHit2D[] circleCastHit2DArray = Physics2D.CircleCastAll(ballRigitbody.position, ballCollider.radius, ballRigitbody.velocity.normalized);

        // Untuk setiap titik tumbukan
        foreach(RaycastHit2D circleCastHit2D in circleCastHit2DArray)
        {
            // Jika terjadi tubukan, dan tumbukan tersebut tidak dengan bola (karena garis lintasan digambar di titik tengah bola)
            if(circleCastHit2D.collider != null && circleCastHit2D.collider.GetComponent<BallControl>() == null)
            {
                // Garis lintasan akan digambar dari titik tengah bola saat ini ke titik tengah bola pada saat tumbukan
                // yaitu sebuah titik yang dioffset dari titik tumbukan berdasarkan vektor normal titik tersebut sebesar jari-jari bola

                // Tentukan titik tumbukan
                Vector2 hitPoint = circleCastHit2D.point;

                // Tentukan normal di titik tumbukan
                Vector2 hitNormal = circleCastHit2D.normal;

                // Tentukan offsetHitPoint, yaitu titik tengah bola pada saat bertumbukan
                offsetHitPoint = hitPoint + hitNormal * ballCollider.radius;

                // Gambar garis lintasan dari titik tengah bola saat ini ke titik tengah bola pada saat tumbukan
                DottedLine.DottedLine.Instance.DrawDottedLine(ball.transform.position, offsetHitPoint);

                // Kalau bukan sidewall, gambar pantlannya
                if (circleCastHit2D.collider.GetComponent<Sidewall>() == null)
                {
                    // Hitung vektor datang
                    Vector2 inVector = (offsetHitPoint - ball.TrajectoryOrigin).normalized;

                    // Hitung vektor keluar
                    Vector2 outVector = Vector2.Reflect(inVector, hitNormal);

                    // Hitung dot product dari outVector dan hitNormal. Digunakan supaya garis lintasan ketika terjadi tumbukan tidak digambar
                    float outDot = Vector2.Dot(outVector, hitNormal);
                    if(outDot > -1.0f && outDot < 1.0f)
                    {
                        // Gambar lintasan pantulannya
                        DottedLine.DottedLine.Instance.DrawDottedLine(offsetHitPoint, offsetHitPoint + outVector * 10.0f);

                        // Untuk menggambar bola "bayangan" di prediksi titik tumbukan
                        drawBallAtCollision = true;
                        
                        if (drawBallAtCollision)
                        {
                            // Gambar bola "Bayangan" di prediksi titik tumbukan
                            ballAtCollision.transform.position = offsetHitPoint;
                            ballAtCollision.SetActive(true);
                        }
                        else
                        {
                            // Sembunyikan bola "Bayangan"
                            ballAtCollision.SetActive(false);
                        }
                    }
                }
                // Hanay gambar lintasan untuk satu titik tumbukan, jadi keluar dari oop
                break;
            }
            // Jika true
        }
    }
}
