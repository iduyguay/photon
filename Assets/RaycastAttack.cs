using Fusion;
using UnityEngine;

public partial class RaycastAttack : NetworkBehaviour
{
    public float Damage = 10;
    public PlayerMovement PlayerMovement;

    private void Update()
    {
        // Sadece kendi karakterimizden ateş edebiliriz
        if (HasStateAuthority == false)
            return;

        // Kamera henüz atanmadıysa hata vermemesi için kontrol ekleyelim
        if (PlayerMovement.Camera == null)
            return;

        // Ekranın tam ortasından bir ışın (ray) çıkarıyoruz
        // (Mouse olmadığı için Input.mousePosition yerine ekran merkezini kullanmak daha garanti olabilir)
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = PlayerMovement.Camera.ScreenPointToRay(screenCenter);
        
        // Işının karakterin içinden değil, biraz önünden başlamasını sağlıyoruz
        ray.origin += PlayerMovement.Camera.transform.forward;

        // MOUSE YOKSA: 'F' tuşu ile ateş etme (Test için geçici)
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Editörde ışını görmek için (Kırmızı çizgi)
            Debug.DrawRay(ray.origin, ray.direction * 50, Color.red, 1f);

            // Eğer bir şeye çarparsa (Mesafe 50 metre olarak ayarlandı)
            if (Physics.Raycast(ray.origin, ray.direction, out var hit, 50f))
            {
                // Çarptığımız şeyde Health scripti var mı?
                if (hit.transform.TryGetComponent<Health>(out var health))
                {
                    // VARSA: RPC'yi çağırarak ona hasar gönder!
                    Debug.Log("Hedef vuruldu! Hasar gönderiliyor...");
                    health.DealDamageRpc(Damage);
                }
            }
        }
    }
}