using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SavedDataManager : MonoBehaviour
{
    private string filePath = "";
    public static SavedDataManager m_instance = null;

    void Start()
    {
        if (m_instance != null)
        {
            Destroy(this);
            Debug.LogError("Trying to create multiple save data managers is not allowed!!!!");
        }
        else
        {
            m_instance = this;

            Debug.Log("Save data manager initializing.");

            filePath = Application.dataPath + "/SaveData/";

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

    public int[] LoadIntArray(string fileName)
    {
        byte[] bytes = File.ReadAllBytes(filePath + fileName);
        int[] data = new int[bytes.Length * sizeof(byte)];

        Buffer.BlockCopy(bytes, 0, data, 0, data.Length);
        return data;
    }

    public void SaveData(string data, string fileName)
    {

    }

    public void SaveData(int[] data, string fileName)
    {
        byte[] bytes = new byte[data.Length * sizeof(int)];
        Buffer.BlockCopy(data, 0, bytes, 0, bytes.Length);

        File.WriteAllBytes(filePath + fileName, bytes);
    }
}
