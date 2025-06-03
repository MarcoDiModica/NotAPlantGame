using UnityEngine;


public class CombatSpawner : MonoBehaviour
{
    public Sword sword;
    public VictoryPannel pannel;

    public Monster monster_to_spawn;

    public Monster Shroom;
    public Monster Artichoke;

    public void SpawnEnemy(Monster moster_prefab)
    {
        //if (!moster_prefab) { 
        //    moster_prefab = MonsterSpawner.Instance.prefab;  
        //}
        //Monster monster = Instantiate(moster_prefab, transform);
        //monster.atkEvent.AddListener(sword.OnMonsterAttack);

        //monster.transform.position = transform.position;
        //pannel.ConnectPannelToDefeat();
        int munster = PlayerPrefs.GetInt("Monster");
        if (munster == 0)
        {
            monster_to_spawn = Shroom;
            //monster.atkEvent.AddListener(sword.OnMonsterAttack);

            //monster.transform.position = transform.position;
            //pannel.ConnectPannelToDefeat();
        }
        else
        {
            monster_to_spawn = Artichoke;
        }

        Monster monster = Instantiate(monster_to_spawn, transform);
        monster.atkEvent.AddListener(sword.OnMonsterAttack);
        monster.transform.position = transform.position;
        pannel.ConnectPannelToDefeat();

    }

    private void Awake()
    {
        //if (monster_to_spawn)
        //{
        //    SpawnEnemy(monster_to_spawn);
        //}
        SpawnEnemy(null);
    }


}
