using Fusion;
using UnityEngine;

public class Health : NetworkBehaviour
{
    // Can değeri 100'den başlar ve değiştiğinde HealthChanged çalışır
    [Networked, OnChangedRender(nameof(HealthChanged))]
    public float NetworkedHealth { get; set; } = 100;

    private void HealthChanged()
    {
        // Can her değiştiğinde konsola yazdırır
        Debug.Log($"Health changed to: {NetworkedHealth}");
    }

    // --- KRİTİK NOKTA: RPC EKLEME ---
    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void DealDamageRpc(float damage)
    {
        // Bu fonksiyonun içindeki kod, SADECE bu nesnenin sahibi olan bilgisayarda çalışır.
        Debug.Log("Sahip bilgisayar RPC'yi aldı, canı düşürüyor...");
        NetworkedHealth -= damage;
    }
}