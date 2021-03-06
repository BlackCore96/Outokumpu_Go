﻿using System.Collections;
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

    bool quitQithoutSaving = false;

    static public bool firstLaunch = false;
    static public bool loadSave = false;

    [Header("Save Game")]
    public bool saveGame;

    private void Awake()
    {
        dataPath = Application.persistentDataPath + "/Save";
        if (loadSave)
        {
            ReadData();
        }
        quitQithoutSaving = false;
        Invoke("LateStart", .5f);
    }

    void LateStart()
    {
        Save();
    }

    public void Save()
    {
        try
        {
            saveFile = SpawnWorldObjects.pOIInfos;
            FileStream fileStream = File.OpenWrite(dataPath);
            formatter.Serialize(fileStream, saveFile);
            fileStream.Close();
            Debug.Log("Saved successfully");
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
        if (!quitQithoutSaving)
        {
            Save();
        }
    }

    public void ResetSave()
    {
        File.Delete(dataPath);
        quitQithoutSaving = true;
        Application.Quit();
    }

    void ReadData()
    {
        loadSave = false;
        try
        {
            if (!File.Exists(dataPath))//tarkistaa onko file olemassa--> jos ei niin tekee
            {
                firstLaunch = true;
                MapManager.mapTutorialDone = false;
                File.Create(dataPath);
                saveFile = new List<SpawnWorldObjects.POIInfo>();
                Debug.Log("First time!");
            }
            else
            {
                MapManager.mapTutorialDone = true;
                try
                {
                    Debug.Log("Load");
                    FileStream filestream = File.OpenRead(dataPath);
                    saveFile = (List<SpawnWorldObjects.POIInfo>)formatter.Deserialize(filestream);
                    filestream.Close();
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
