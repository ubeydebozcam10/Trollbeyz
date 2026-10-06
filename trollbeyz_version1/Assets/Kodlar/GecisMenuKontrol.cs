using UnityEngine;
using UnityEngine.SceneManagement;

public class GecisMenuKontrol : MonoBehaviour
{
    // 1. BUTON İÇİN: 2. Bölüme geçiş yapar
    public void BolumIkiyeDevamEt()
    {
        // 2. bölümünün tam adını buraya yazıyoruz
        SceneManager.LoadScene("Bolum_2");
    }

    // 2. BUTON İÇİN: 1. Bölümü baştan başlatır
    public void BolumBiriTekrarla()
    {
        // 1. bölümünün tam adını buraya yazıyoruz (Ekranda Bolum_1 olarak görmüştüm)
        SceneManager.LoadScene("Bolum_1");
    }
}