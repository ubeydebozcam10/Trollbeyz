using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuKontrol : MonoBehaviour
{
    public void OyunaBasla()
    {
        // Sol alttaki Console penceresinde bu yazıyı görürsek buton çalışıyor demektir
        Debug.Log("BUTONA BASILDI! Oyun sahnesi yükleniyor...");

        // Sahneyi ismiyle çağırmak index karmaşasını önler. 
        // Eğer asıl oyun sahnendeki isim "SampleScene" ise aynen böyle bırak:
        SceneManager.LoadScene("Bolum_1");
    }
}