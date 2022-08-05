using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;
using System.IO;

/// <summary>
/// DictionaryデータとJsonデータ(string)の変換を行う
/// 暗号化と複合化も同時に行う
/// </summary>
public static class JsonSerializer
{

    //保存するディレクトリ名
    private const string DIRECTORY_NAME = "Data";

    //ファイルのパスを取得
    private static string GetFilePath(string fileName)
    {

        string directoryPath = Application.persistentDataPath + "/" + DIRECTORY_NAME;

        //ディレクトリが無ければ作成
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        //ファイル名は暗号化する
        string encryptedFlieName = Encryption.EncryptString(fileName);
        string filePath = directoryPath + "/" + encryptedFlieName;

        return filePath;
    }

    /// <summary>
    /// Dictionaryデータをjson形式に変換して保存する
    /// </summary>
    /// <param name="dic">保存するDictionary<string, object>データ</param>
    /// <param name="fileName">保存ファイル名</param>
    public static void Save(Dictionary<string, object> dic, string fileName)
    {

        string jsonStr = Json.Serialize(dic);
        Debug.Log("serialized text = " + jsonStr);

        //jsonを暗号化する
        jsonStr = Encryption.EncryptString(jsonStr);

        string filePath = GetFilePath(fileName);
        File.WriteAllText(filePath, jsonStr);


        Debug.Log("saveFilePath = " + filePath);
    }

    /// <summary>
    /// jsonデータを読み込みDictionaryデータに変換して返す
    /// </summary>
    /// <param name="fileName">取得するファイルの名前</param>
    public static Dictionary<string, object> Load(string fileName)
    {

        string filePath = GetFilePath(fileName);
        if (!File.Exists(filePath))
        {
            Debug.Log(fileName + "はありません！");
            return null;
        }

        string jsonStr = File.ReadAllText(filePath);

        //取得したファイルを複合化
        jsonStr = Encryption.DecryptString(jsonStr);

        Dictionary<string, object> dic = Json.Deserialize(jsonStr) as Dictionary<string, object>;
        return dic;
    }

}