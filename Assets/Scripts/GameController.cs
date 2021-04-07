﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    //constant variable to contain the save file path for easy access inside of this script
    private const String SAVEFILEPATH = "/playerData.txt";

    //this is the MAIN STATIC control object to be called by other scripts
    public static GameController control;

    //these are potential saved values
    //public AudioMixer mixer;

    //on awake, check for controller obj, if none exist stay, otherwise Destroy()
    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
    }

    //this initializes the player's music volume and other things 
/*    void Start()
    {
        mixer.SetFloat("musicVol", PlayerPrefs.GetFloat("musicVol", 0));
        mixer.SetFloat("fxVol", PlayerPrefs.GetFloat("fxVol", 0));
    }*/

    //saves player data to file
    public void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + SAVEFILEPATH;
        FileStream stream = new FileStream(path, FileMode.Create);
        GameData data = new GameData();

        formatter.Serialize(stream, data);

        //example of saving data
        //data.maxUses = maxUses;
        
        stream.Close();
    }

    //loads player data from SAVEFILEPATH and inputs data into GameData class
    public void Load()
    {
        string path = Application.persistentDataPath + SAVEFILEPATH;
        if (File.Exists(path))//if file is found input data into the local DDOL "control" object
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            GameData data = formatter.Deserialize(stream) as GameData;

            stream.Close();

            //example of loading data
            //maxUses = data.maxUses;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }

/*    public void musicVolume(float volume)
    {
        mixer.SetFloat("musicVol", volume);
        PlayerPrefs.SetFloat("musicVol", volume);
    }

    public void effectsVolume(float volume)
    {
        mixer.SetFloat("fxVol", volume);
        PlayerPrefs.SetFloat("fxVol", volume);
    }*/

    public void goToNewScene(String sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}

[Serializable]
class GameData //THIS HOLDS RAW PLAYER DATA
{

}