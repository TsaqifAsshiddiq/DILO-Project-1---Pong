using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sidewall : MonoBehaviour
{
    // Pemain yang akan ditambah skornya jika bola menyentuh dinding
    public PlayerControl player;
    [SerializeField]
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Akan dipanggil ketika objek lain bercollider (bola) bersentuhan dengan dinding
    void OnTriggerEnter2D(Collider2D anotherCollider)
    {
        // Jika objek tersebut bernama "Ball"
        if(anotherCollider.name == "Ball")
        {
            // Tambahkan skor ke pemain
            player.IncrementScore();

            // Jika skor pemain belummencapai skor maksimal
            if(player.Score < gameManager.maxScore)
            {
                // restart game setelah bola mengenai dinding
                anotherCollider.gameObject.SendMessage("RestartGame", 2.0f, SendMessageOptions.RequireReceiver);
            }
        }
    }
}
