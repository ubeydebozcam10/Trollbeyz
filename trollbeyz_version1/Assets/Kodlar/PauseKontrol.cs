using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseKontrol : MonoBehaviour
{
    public GameObject pauseMenuPaneli; // Gizleyip açacağımız panel
    private bool oyunDurduMu = false; // Oyunun durma durumunu takip eder

    void Update()
    {
        // Eğer oyuncu klavyeden ESC tuşuna basarsa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (oyunDurduMu)
            {
                DevamEt(); // Zaten durmuşsa devam et
            }
            else
            {
                Durdur();  // Akıyorsa durdur
            }
        }
    }

    // Oyunu durduran fonksiyon
    public void Durdur()
    {
        pauseMenuPaneli.SetActive(true); // Paneli görünür yap
        Time.timeScale = 0f;             // Zamanı tamamen dondur (0 çarpandır)
        oyunDurduMu = true;
    }

    // Oyunu devam ettiren fonksiyon
    public void DevamEt()
    {
        pauseMenuPaneli.SetActive(false); // Paneli gizle
        Time.timeScale = 1f;              // Zamanı normal hızına (1) döndür
        oyunDurduMu = false;
    }

    // Bölümü baştan başlatan fonksiyon
    public void BastanBasla()
    {
        Time.timeScale = 1f; // ÇOK ÖNEMLİ: Yeniden başlarken zamanın donuk kalmaması için önce 1 yapıyoruz
        int suAnkiSahne = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(suAnkiSahne); // Aynı sahneyi tekrar yükle
    }
}