using Fusion;
using UnityEngine;

public class PlayerSpawner : SimulationBehaviour, IPlayerJoined
{
    public GameObject PlayerPrefab;

    void IPlayerJoined.PlayerJoined(PlayerRef player)
    {
        if (player == Runner.LocalPlayer)
        {
            // İç içe doğmasınlar diye X ve Z eksenlerinde hafif rastgele bir konum belirliyoruz
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-2f, 2f), 1, Random.Range(-2f, 2f));

            // DİKKAT: En sona ', player' ekleyerek bu karakterin sahibini sisteme tanıtıyoruz!
            Runner.Spawn(PlayerPrefab, randomSpawnPosition, Quaternion.identity, player);
        }
    }
}