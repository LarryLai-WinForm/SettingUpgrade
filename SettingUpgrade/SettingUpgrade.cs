using System.Configuration;

namespace SettingUpgrade
{
    public class SettingUpgradeClass
    {

        /// <summary>
        /// <para>檢查更新參數名稱</para>
        /// <para>更新版本時所有設定值會變為預設值</para>
        /// <para>須設定一個檢查更新參數</para>
        /// <para>並在更新時載入舊版本設定參數值</para>
        /// </summary>
        const string SettingUpgradeKey = "_SettingUpgrade";

        /// <summary>
        /// 存取檢查更新值
        /// </summary>
        static bool Upgraded
        {
            get
            {
                SettingUpgrade.Default.Reload();
                return (bool)SettingUpgrade.Default[SettingUpgradeKey];
            }
            set
            {
                SettingUpgrade.Default[SettingUpgradeKey] = value;
                SettingUpgrade.Default.Save();
            }
        }
        /// <summary>
        /// 檢查更新時載入舊版本設定參數,請先建立參數 bool _SettingUpgrade = false
        /// </summary>
        /// <param name="settings"></param>
        static public void Run(ApplicationSettingsBase settings)
        {

            //檢查是否為新版本未更新參數值
            if (Upgraded == false)
            {
                //從舊版本載入設定
                settings.Upgrade();
                //存檔
                settings.Save();

                //設定為已載入舊版本參數
                Upgraded = true;
            }

            //重載入設定參數值
            settings.Reload();

        }

    }
}
