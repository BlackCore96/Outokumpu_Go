using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [System.Serializable]
    public struct SaveInfo
    {
        public List<SpawnWorldObjects.POIInfo> pOIs;
    }

    List<SpawnWorldObjects.POIInfo> saveFile;
    BinaryFormatter formatter = new BinaryFormatter();
    string dataPath;

    static public bool firstLaunch = false;
    static public bool loadSave = false;

    [Header("Save Game")]
    public bool saveGame;

    private void Awake()
    {
        if (!Application.isEditor)
        {
            dataPath = Application.persistentDataPath + "/Save";
        }
        else
        {
            dataPath = Application.persistentDataPath + "/Save";
        }
        if (loadSave)
        {
            ReadData();
        }

        Invoke("LateStart", .1f);
    }

    void LateStart()
    {
        Save();
    }

    public void Save()
    {
        try
        {
            Debug.Log("Save");
            saveFile = SpawnWorldObjects.pOIInfos;
            FileStream fileStream = File.OpenWrite(dataPath);
            formatter.Serialize(fileStream, saveFile);
        }
        catch
        {
            Debug.Log("Error saving the files!");
        }
    }

    private void Update()
    {
        if (saveGame)
        {
            saveGame = false;
            Save();
        }
    }

    public void Load()
    {
        ReadData();
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    void ReadData()
    {
        loadSave = false;
        try
        {
            if (!File.Exists(dataPath))//tarkistaa onko directory olemassa--> jos ei niin tekee
            {
                firstLaunch = true;
                File.Create(dataPath);
                saveFile = new List<SpawnWorldObjects.POIInfo>();
                Debug.Log("First time!");
            }
            else
            {
                try
                {
                    Debug.Log("Load");
                    FileStream filestream = File.OpenRead(dataPath);
                    saveFile = (List<SpawnWorldObjects.POIInfo>)formatter.Deserialize(filestream);
                    SpawnWorldObjects.pOIInfos = saveFile;
                }
                catch
                {
                    Debug.Log("Error");
                    saveFile = new List<SpawnWorldObjects.POIInfo>();
                    firstLaunch = true;
                }
            }
        }
        catch
        {
            Debug.Log("Error reading the files!");
        }
    }
}
