using Fusion;
using UnityEngine;

public class PlayerColor : NetworkBehaviour
{
    public MeshRenderer MeshRenderer;

    // Hem ağda senkronize olsun hem de değiştiğinde ColorChanged fonksiyonunu tetiklesin
    [Networked, OnChangedRender(nameof(ColorChanged))]
    public Color NetworkedColor { get; set; }

    private void ColorChanged()
    {
        // Ağdan yeni bir renk geldiğinde görseli güncelle
        MeshRenderer.material.color = NetworkedColor;
    }

    private void Update()
    {
        // Sadece bu karakterin sahibiysen (StateAuthority) veri girişi yapabilirsin
        if (HasStateAuthority == false)
            return;

        // 'E' tuşuna basıldığında rastgele bir renk ata
        if (Input.GetKeyDown(KeyCode.E))
        {
            NetworkedColor = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 1f);
        }
    }
}