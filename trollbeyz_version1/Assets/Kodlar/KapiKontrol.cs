using UnityEngine;
using UnityEngine.SceneManagement;

public class KapiKontrol : MonoBehaviour
{
    // Hangi sahneye geçeceğimizi artık Unity Inspector panelinden el yazısıyla seçeceğiz
    [Header("Gidilecek Sahnenin Adı")]
    public string sonrakiSahneAdi;

    private void OnTriggerEnter2D(Collider2D temasEden)
    {
        // Eğer temas eden obje "Player" etiketine sahipse
        if (temasEden.CompareTag("Player"))
        {
            // Yukarıdaki kutucuğa hangi sahne adını yazdıysak orayı açar
            SceneManager.LoadScene(sonrakiSahneAdi);
        }
    }
}