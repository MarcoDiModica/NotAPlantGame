using UnityEngine;

public class CombatSpawner : MonoBehaviour
{
    public Sword sword;
    public VictoryPannel pannel;

    public void SpawnEnemy(Monster moster_prefab)
    {
        Monster monster = Instantiate(moster_prefab, transform);
        monster.atkEvent.AddListener(sword.OnMonsterAttack);

        monster.transform.position = transform.position;
        pannel.ConnectPannelToDefeat();
    }

}
