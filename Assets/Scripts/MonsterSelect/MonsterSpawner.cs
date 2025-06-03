using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

public class MonsterSpawner : MonoBehaviour
{

    public Monster prefab;
    string aaaaaa;
    public static MonsterSpawner Instance;

    private void Awake()
    {
       if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SelectMonster(Monster monster_prefab, string bbbb ="a")
    {
        aaaaaa = bbbb;
        prefab = monster_prefab;
        LoadSceneAsync("CombatScene");
    }

    void SpawnEnemy()
    {

        CombatSpawner spawn = GameObject.FindAnyObjectByType<CombatSpawner>();
        if (spawn && prefab) spawn.SpawnEnemy(prefab);
        print(aaaaaa);
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
