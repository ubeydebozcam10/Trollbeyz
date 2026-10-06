// Elmas için kodlar

using UnityEngine;

public class Elmas : MonoBehaviour
{
    [Header("Ses Ayarý")]
    public AudioClip toplamaSesi; // Elmas toplanýnca çalacak o tatlý coin/altýn sesi kanka

    private bool toplandiMi = false; // Sesin üst üste binip çift tetiklenmesini engellemek için emniyet kilidi

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Elmasa çarpan objenin adý "Oyuncu" ise ve elmas daha önce toplanmadýysa kanka
        if (collision.gameObject.name == "Oyuncu" && !toplandiMi)
        {
            toplandiMi = true;

            // --- KESÝLMEYEN SES MANEVRASI ---
            if (toplamaSesi != null)
            {
                // Elmas milisaniyeler içinde yok olsa bile bu ses kameranýn olduðu yerde sonuna kadar gürül gürül çalar kanka!
                AudioSource.PlayClipAtPoint(toplamaSesi, Camera.main.transform.position, 1.0f);
            }

            // Merkezi beyne (GameManager) ulaþýp skoru 1 arttýr diyoruz kanka
            if (GameManager.instance != null)
            {
                GameManager.instance.ElmasKazandir(1);
            }

            // Elmasý sahneden güvenle yok et, sesimiz artýk havada asýlý kalýp çalmaya devam edecek!
            Destroy(gameObject);
        }
    }
}
