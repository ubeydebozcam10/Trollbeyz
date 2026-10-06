using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Hareket Ayarları")]
    public float hiz = 8f;
    public float ziplamaGucu = 12f;

    [Header("Can Ayarları")]
    public int maksimumCan = 3;
    private int mevcutCan;
    public TextMeshProUGUI canYazisi;

    [Header("Zemin Kontrolü")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask zeminKatmani;

    [Header("Yürüme İnce Ayarı (Gömülme Engeli)")]
    public float yuruoOffsetKaymasi = -0.04f;

    [Header("Ses Klipleri")]
    public AudioClip ziplamaSesi;
    public AudioClip hasarSesi;
    public AudioClip elmasSesi;
    public AudioClip arkaPlanMuzigi;

    [Header("Mobil Kontroller")]
    private bool solaBasili;
    private bool sagaBasili;

    private AudioSource audioSource;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D boxCollider;

    private bool yerdeMi;
    private float yatayHareket;
    private float orijinalOffsetMerdiyeni;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

        if (boxCollider != null)
        {
            orijinalOffsetMerdiyeni = boxCollider.offset.y;
        }

        mevcutCan = maksimumCan;
        CanArayuzunuGuncelle();

        if (audioSource != null && arkaPlanMuzigi != null)
        {
            audioSource.clip = arkaPlanMuzigi;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    void Update()
    {
        // Yön Kontrolü: Hem PC (Klavye) Hem Mobil Butonlar Bir Arada
        if (solaBasili)
        {
            yatayHareket = -1f;
        }
        else if (sagaBasili)
        {
            yatayHareket = 1f;
        }
        else
        {
            yatayHareket = Input.GetAxisRaw("Horizontal");
        }

        if (yatayHareket > 0) sprite.flipX = false;
        else if (yatayHareket < 0) sprite.flipX = true;

        yerdeMi = Physics2D.OverlapCircle(groundCheck.position, checkRadius, zeminKatmani);

        if (boxCollider != null)
        {
            if (yatayHareket != 0 && yerdeMi)
            {
                boxCollider.offset = new Vector2(boxCollider.offset.x, orijinalOffsetMerdiyeni + yuruoOffsetKaymasi);
            }
            else
            {
                boxCollider.offset = new Vector2(boxCollider.offset.x, orijinalOffsetMerdiyeni);
            }
        }

        if (anim != null)
        {
            anim.SetBool("yuruyor mu", yatayHareket != 0);
            anim.SetBool("yerde mi", yerdeMi);
        }

        // PC İçin Zıplama Tuşu (Space vb.)
        if (Input.GetButtonDown("Jump"))
        {
            ZiplamaTetikle();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(yatayHareket * hiz, rb.linearVelocity.y);
    }

    public void CanAzalt(int miktar)
    {
        mevcutCan -= miktar;
        CanArayuzunuGuncelle();

        if (audioSource != null && hasarSesi != null)
        {
            audioSource.PlayOneShot(hasarSesi);
        }

        if (mevcutCan <= 0)
        {
            Debug.Log("CAN BİTTİ! Oyun yeniden yükleniyor...");
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
        else
        {
            transform.position = new Vector2(0f, 2f);
            rb.linearVelocity = Vector2.zero;
        }
    }

    void CanArayuzunuGuncelle()
    {
        if (canYazisi != null)
        {
            canYazisi.text = "x " + mevcutCan;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Tuzak"))
        {
            CanAzalt(1);
        }

        if (collision.CompareTag("Elmas"))
        {
            if (audioSource != null && elmasSesi != null)
            {
                audioSource.PlayOneShot(elmasSesi);
            }
        }
    }

    // --- MOBİL BUTON FONKSİYONLARI ---

    public void SolaBas() { solaBasili = true; }
    public void SolaBirak() { solaBasili = false; }

    public void SagaBas() { sagaBasili = true; }
    public void SagaBirak() { sagaBasili = false; }

    public void ZiplamaTetikle()
    {
        if (yerdeMi)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, ziplamaGucu);

            if (audioSource != null && ziplamaSesi != null)
            {
                audioSource.PlayOneShot(ziplamaSesi);
            }
        }
    }
}