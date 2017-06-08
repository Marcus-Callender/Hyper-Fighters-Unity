using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavedDataManager : MonoBehaviour
{
    private string filePath = "";

    void Start()
    {
        filePath = Application.dataPath + "/MyAssets/SavedData/";

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
            File.Create(totalFilePath);
            File.WriteAllText(totalFilePath, "Hello");
        }

        return "NULL";
    }
}
