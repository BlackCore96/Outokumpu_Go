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

    SaveInfo saveFile;
    BinaryFormatter formatter = new BinaryFormatter();
    string dataPath;

    static public bool firstLaunch = false;

    [Header("Save Game")]
    public bool saveGame;

    private void Awake()
    {
        dataPath = Application.persistentDataPath + "/save";
        ReadData();
    }

    public void Save()
    {
        if (!saveFile.pOIs.Equals(SpawnWorldObjects.pOIInfos))
        {
            Debug.Log("Save");
            saveFile.pOIs = SpawnWorldObjects.pOIInfos;
            using (Stream fileStream = File.OpenWrite(dataPath))
            {
                formatter.Serialize(fileStream, saveFile);
            }
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
        if (!Directory.Exists(dataPath))//tarkistaa onko directory olemassa--> jos ei niin tekee
        {
            firstLaunch = true;
            Directory.CreateDirectory(dataPath);
            saveFile = new SaveInfo();
            saveFile.pOIs = new List<SpawnWorldObjects.POIInfo>();
            Debug.Log("First time!");
        }
        else
        {
            Debug.Log("Load");
            using (Stream filestream = File.OpenRead(dataPath))//unauthorized access!!
            {
                saveFile = (SaveInfo)formatter.Deserialize(filestream);
            }
        }
    }
}
