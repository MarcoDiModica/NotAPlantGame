using UnityEngine;
using UnityEngine.UI;

public class monsterbutton : MonoBehaviour
{
    private Button button;

    public Monster monster_prefab;
    public int monsterID = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Spawn);
    }

    void Spawn()
    {
        MonsterSpawner.Instance.SelectMonster(monster_prefab);
        PlayerPrefs.SetInt("Monster", monsterID);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
