// KUTU ÝÇÝN

using UnityEngine;

public class GizemliKutu : MonoBehaviour
{
    public GameObject elmasPrefab;
    public Sprite acikKutuGorseli;

    private SpriteRenderer spriteRenderer;
    private bool kutuAcildi = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Karakterin adý "Oyuncu" ise ve kutu daha önce HÝÇ açýlmadýysa
        if (collision.gameObject.name == "Oyuncu" && !kutuAcildi)
        {
            //  YENÝ ÞART: Oyuncunun kutuya üstten mi çarptýðýný kontrol ediyoruz
            // Eðer oyuncunun bastýðý yer (temas noktasý), kutunun merkezinden daha yukarýdaysa:
            foreach (ContactPoint2D temasNoktasi in collision.contacts)
            {
                if (temasNoktasi.normal.y < -0.5f) // Yukardan aþaðýya doðru bir basma varsa
                {
                    KutuyuAc();
                    break;
                }
            }
        }
    }

    void KutuyuAc()
    {
        kutuAcildi = true; // Kutuyu kilitliyoruz, bir daha bu kod çalýþamaz!

        // 1. Elmasý kutunun biraz yukarýsýnda oluþtur
        if (elmasPrefab != null)
        {
            Vector3 elmasPozisyonu = transform.position + new Vector3(0, 0.8f, 0);
            Instantiate(elmasPrefab, elmasPozisyonu, Quaternion.identity);
        }

        // 2. Görseli zorla ve kesin olarak deðiþtir
        if (acikKutuGorseli != null)
        {
            spriteRenderer.sprite = acikKutuGorseli;

            //  UNITY'NÝN ÝNATÇILIÐINI KIRACAK EN YÜKSEK EMÝRLER:
            spriteRenderer.sortingLayerName = "OnPlan"; // Katman adýný zorla OnPlan yap
            spriteRenderer.sortingOrder = 100;         // Sýrayý zorla 100 yap (En öne gelsin)

            // Ne olur ne olmaz, sprite renderer'ý bir kez kapatýp açarak ekraný yeniliyoruz
            spriteRenderer.enabled = false;
            spriteRenderer.enabled = true;

            Debug.Log("Kutu görseli baþarýyla 'Açýk Kutu' yapýldý ve katmaný 100'e çekildi!");
        }
        else
        {
            Debug.LogError("HATA: Inspector panelinde 'Acik Kutu Gorseli' alaný boþ kanka!");
        }
    }
}
