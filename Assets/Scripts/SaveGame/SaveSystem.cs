using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    //vvv SAVE vvv

    public static void SaveWinPoints(WinPointCounter winPointCounter)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/winPoints.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        WinPointData data = new WinPointData(winPointCounter);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveDefenseStore(DefensesStore defensesStore)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/defensesStore.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        DefensesStoreData data = new DefensesStoreData(defensesStore);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void SaveLoadManager(LoadManager loadManager)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/loadManager.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        LoadManagerData data = new LoadManagerData(loadManager);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    //vvv LOAD vvv

    public static WinPointData LoadWinPoints()
    {
        string path = Application.persistentDataPath + "/winPoints.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            WinPointData data = formatter.Deserialize(stream) as WinPointData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No Save File in: " + path);
            return null;
        }
    }

    public static DefensesStoreData LoadDefensesStore()
    {
        string path = Application.persistentDataPath + "/defensesStore.save";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            DefensesStoreData data = formatter.Deserialize(stream) as DefensesStoreData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No Save File in: " + path);
            return null;
        }
    }

    public static LoadManagerData LoadLoadManager()
    {
        string path = Application.persistentDataPath + "/loadManager.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            LoadManagerData data = formatter.Deserialize(stream) as LoadManagerData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No Save File in: " + path);
            return null;
        }
    }
}

