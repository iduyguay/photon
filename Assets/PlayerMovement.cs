using Fusion;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    private Vector3 _velocity;
    private bool _jumpPressed;

    private CharacterController _controller;

    public float PlayerSpeed = 2f;
    public float JumpForce = 5f;
    public float GravityValue = -9.81f;

    // Kamerayı tutacak olan değişken
    public Camera Camera;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    // NetworkObject doğduğunda (spawn olduğunda) çalışır
    public override void Spawned()
    {
        // Eğer bu karakter bana ait değilse (başka bir oyuncuysa) kamerayı ona bağlama
        if (HasStateAuthority == false)
            return;

        // Sahnedeki ana kamerayı bul
        Camera = Camera.main;
        
        // Kameradaki FirstPersonCamera scriptine hedef olarak bu karakteri ata
        if (Camera != null)
        {
            var fpCamera = Camera.GetComponent<FirstPersonCamera>();
            if (fpCamera != null)
            {
                fpCamera.Target = transform;
            }
        }
    }

    private void Update()
    {
        // Tuş girdilerini her zaman Update içinde yakalıyoruz
        if (Input.GetButtonDown("Jump"))
        {
            _jumpPressed = true;
        }
    }

    public override void FixedUpdateNetwork()
    {
        // Sadece karakterin sahibi hareket kodunu çalıştırsın
        if (HasStateAuthority == false) return;

        if (_controller.isGrounded)
        {
            _velocity = new Vector3(0, -1, 0);
        }

        // --- KAMERA YÖNÜNE GÖRE HAREKET ---
        // Kameranın Y eksenindeki (sağ-sol) dönüşünü alıyoruz
        Quaternion cameraRotationY = Quaternion.Euler(0, Camera.transform.rotation.eulerAngles.y, 0);
        
        // Karakterin ileri-geri gitme komutunu kameranın baktığı yöne göre hesaplıyoruz
        Vector3 move = cameraRotationY * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * Runner.DeltaTime * PlayerSpeed;
        // ----------------------------------

        _velocity.y += GravityValue * Runner.DeltaTime;

        if (_jumpPressed && _controller.isGrounded)
        {
            _velocity.y += JumpForce;
        }

        _controller.Move(move + _velocity * Runner.DeltaTime);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        _jumpPressed = false;
    }
}