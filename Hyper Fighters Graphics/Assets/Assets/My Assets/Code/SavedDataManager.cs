using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedDataManager : MonoBehaviour
{
    private string filePath = "";

    void Start()
    {
        Debug.Log("Save data manager initializing.");

        filePath = Application.dataPath + "/SaveData/"; //"/MyAssets/SavedData/";

        try
        {
            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

        }
        catch (IOException ex)
        {
            Debug.Log("Save data manager exception: " + ex.Message);
        }

        Debug.Log(LoadData("Data.json"));
    }

    public string LoadData(string fileName)
    {
        string totalFilePath = filePath + fileName;

        if (File.Exists(totalFilePath))
        {
            Debug.Log("File exists");
            return File.ReadAllText(totalFilePath);
        }
        else
        {
            Debug.Log("file dosen't exist");
            FileStream fs = File.Create(totalFilePath);
            File.WriteAllText(totalFilePath, "Hello");
            fs.Close();
        }

        return "NULL";
    }
}
