using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SerializeScript
{
    
    public static void SaveData(SerializableData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/SurvivalQuestData.bin";
        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, data);
        fileStream.Close();

    }

    public static SerializableData LoadData()
    {
        string path = Application.persistentDataPath + "/SurvivalQuestData.bin";

        if (!File.Exists(path))
        {
            SerializableData data = new SerializableData(0, 0);
            SaveData(data);
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(path, FileMode.Open);

        SerializableData returnedData = formatter.Deserialize(fileStream) as SerializableData;
        fileStream.Close();

        return returnedData;
    }

}
