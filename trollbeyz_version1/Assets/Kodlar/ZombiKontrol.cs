using UnityEngine;

public class ZombiKontrol : MonoBehaviour
{
    [Header("Görsel Ayarlarý")]
    public Sprite ezilmeResmi;   // Kafasýna basýnca geçeceði o pestil resmi

    [Header("Ses Ayarlarý")]
    public AudioClip zombiOlumSesi; // Öldüðünde çýkacak o "Çat" sesi
    private AudioSource audioSource;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider; // Kanka burayý Capsule yaptým jilet gibi kaymasý için!
    private bool olduMu = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>(); // Burayý da Capsule olarak çektik
        audioSource = GetComponent<AudioSource>();

        // Sabit duracaðý için kýpýrdamasýn kanka
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    // --- ÇARPIÞMA KONTROLÜ (KAFASINA BASMA MANTIÐI) ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (olduMu) return;

        // Eðer çarpan þey bizim ana karakterse
        if (collision.gameObject.CompareTag("Player"))
        {
            // Oyuncunun zombiye/gobline nereden çarptýðýný hesaplýyoruz
            Vector2 temasNoktasi = collision.GetContact(0).normal;

            if (temasNoktasi.y < -0.5f) // Üstten tam kafasýna basýldýysa kanka
            {
                EzilerekOl();

                // Trollbey kafaya basýnca havaya doðru hafifçe geri zýplasýn (Vuruþ hissi)
                Rigidbody2D oyuncuRb = collision.gameObject.GetComponent<Rigidbody2D>();
                if (oyuncuRb != null)
                {
                    oyuncuRb.linearVelocity = new Vector2(oyuncuRb.linearVelocity.x, 8f);
                }
            }
            else
            {
                // Saðdan soldan düz çarptýysa oyuncunun canýný azalt kanka
                PlayerMovement oyuncu = collision.gameObject.GetComponent<PlayerMovement>();
                if (oyuncu != null)
                {
                    oyuncu.CanAzalt(1);
                }
            }
        }
    }

    void EzilerekOl()
    {
        olduMu = true;

        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = RigidbodyType2D.Kinematic; // Fizikten etkilenmesin artýk
        }

        // Arkadaþýnýn çizdiði o ezilmiþ pestil resmini buraya çakýyoruz kanka
        if (spriteRenderer != null && ezilmeResmi != null)
        {
            spriteRenderer.sprite = ezilmeResmi;
        }

        if (capsuleCollider != null) capsuleCollider.enabled = false; // Ýçinden geçebilelim diye fizik sýnýrýný kapat kanka!

        // --- SESÝ KÖKLEME VE KESÝLMESÝNÝ ENGELLEME MANTIÐI ---
        if (zombiOlumSesi != null)
        {
            if (audioSource != null)
            {
                // Kanka buraya , 1.0f ekleyerek sesi %100 seviyesine çektim!
                audioSource.PlayOneShot(zombiOlumSesi, 1.0f);
            }
            else
            {
                // Eðer objede AudioSource bileþeni yoksa veya bozulduysa ses havada kalmasýn, kameranýn olduðu yerde gürlesin
                AudioSource.PlayClipAtPoint(zombiOlumSesi, Camera.main.transform.position, 1.0f);
            }
        }

        // Ezilmiþ resmi ekranda 0.5 saniye dursun, sonra dünyadan tamamen silinsin
        Destroy(gameObject, 0.5f);
    }
}
