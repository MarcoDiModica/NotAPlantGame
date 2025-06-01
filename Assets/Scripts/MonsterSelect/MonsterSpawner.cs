using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{

    public Monster prefab;
    public static MonsterSpawner Instance;

    private void Awake()
    {
        if (Instance != null) { Destroy(this.gameObject); }
        else { Instance = this; }
        DontDestroyOnLoad(this);
    }

    public void SelectMonster(Monster monster_prefab)
    {
        prefab = monster_prefab;
        LoadSceneAsync("CombatScene");
    }

    void SpawnEnemy()
    {

        CombatSpawner spawn = GameObject.FindAnyObjectByType<CombatSpawner>();
        if (spawn && prefab) spawn.SpawnEnemy(prefab);
    }

    public void LoadSceneAsync(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if(sceneName == "ShopScene")
        {
            Destroy(gameObject);
        }
        else 
        {
            SpawnEnemy();
        }

    }

}
