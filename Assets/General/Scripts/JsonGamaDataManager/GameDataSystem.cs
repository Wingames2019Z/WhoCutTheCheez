using System;
using System.Collections.Generic;

public class UserDataModel
{
    public int BestScore;
}
public class ShopDataModel
{
    public bool NoAd;
}
public class SettingDataModel
{
    public bool Sounds;
    public bool Vibration;
}
public static class GameDataSystem
{
    public static UserDataModel UserDataLoad()
    {
        UserDataModel userDataModel = new UserDataModel();
        Dictionary<string, object> user_dic = JsonSerializer.Load("UserSaveData");
        if(user_dic == null)
        {
            //Initialize Game Data
            userDataModel.BestScore = 0;
            UserDataSave(userDataModel);
        }
        else
        {
            userDataModel.BestScore = int.Parse(user_dic["best_score"] as string);
        }
        return userDataModel;
    }
    public static void UserDataSave(UserDataModel userDataModel)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("best_score", userDataModel.BestScore.ToString());
        JsonSerializer.Save(dic, "UserSaveData");
    } 
    public static ShopDataModel ShopDataLoad()
    {
        ShopDataModel shopDataModel = new ShopDataModel();
        Dictionary<string, object> shop_dic = JsonSerializer.Load("Shop");
        if (shop_dic == null)
        {
            //Initialize Shop Data
            shopDataModel.NoAd = false;
            ShopDataSave(shopDataModel);
        }
        else
        {
            shopDataModel.NoAd = bool.Parse(shop_dic["no_ad"] as string);
        }        
        return shopDataModel;
    }
    public static void ShopDataSave(ShopDataModel shopDataModel)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("no_ad", shopDataModel.NoAd.ToString());
        JsonSerializer.Save(dic, "Shop");
    }
    public static SettingDataModel SettingDataLoad()
    {
        SettingDataModel settingDataModel = new SettingDataModel();
        Dictionary<string, object> setting_dic = JsonSerializer.Load("SettingData");

        if (setting_dic == null)
        {
            //Initialize Level Data
            settingDataModel.Sounds = true;
            settingDataModel.Vibration = true;
            SettingDataSave(settingDataModel);
        }
        else
        {
            settingDataModel.Sounds = bool.Parse(setting_dic["sounds"] as string);
            settingDataModel.Vibration = bool.Parse(setting_dic["vibration"] as string);
        }
        return settingDataModel;
    }
    public static void SettingDataSave(SettingDataModel settingDataModel)
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();
        dic.Add("sounds", settingDataModel.Sounds.ToString());
        dic.Add("vibration", settingDataModel.Vibration.ToString());
        JsonSerializer.Save(dic, "SettingData");
    }
}
