using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalDataManager : MonoBehaviour
{
    public static GlobalDataManager Instance;
    public string playerName { private set; get; }
    public void SetName(string name)=> playerName = name;
    
    public int HighScore = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        Load();
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void LoadScene(string index)
    {
        SceneManager.LoadScene(index);
    }

    [System.Serializable]
    class GlobalData
    {
        public int HighScore;
    }

    public void Save()
    {
        GlobalData data = new GlobalData();
        data.HighScore = HighScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/globalData.json", json);
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/globalData.json"))
        {
            string json = File.ReadAllText(Application.persistentDataPath + "/globalData.json");
            GlobalData data = JsonUtility.FromJson<GlobalData>(json);
            HighScore = data.HighScore;
        }
    }
    
    
}
