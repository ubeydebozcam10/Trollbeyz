// ELMAS TOPLAMI ICIN

using UnityEngine;
using TMPro; // TextMeshPro kullanabilmek için bu kütüphane þart kanka!

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Diðer kodlardan bu koda kolayca ulaþmak için köprü

    [Header("UI Elementleri")]
    public TextMeshProUGUI elmasYazisi; // Ekrandaki yazý bileþeni

    private int toplamElmas = 0;

    void Awake()
    {
        // Singleton tasarýmý: Sahnede sadece bir tane GameManager olmasýný saðlýyoruz
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SkoruGuncelle();
    }

    // Elmas toplandýðýnda bu fonksiyon çaðrýlacak
    public void ElmasKazandir(int miktar)
    {
        toplamElmas += miktar;
        SkoruGuncelle();
        Debug.Log("Skor Güncellendi! Toplam Elmas: " + toplamElmas);
    }

    // Ekrandaki yazýyý güncelleyen fonksiyon
    void SkoruGuncelle()
    {
        if (elmasYazisi != null)
        {

            elmasYazisi.text = "x " + toplamElmas;
        }
    }
}