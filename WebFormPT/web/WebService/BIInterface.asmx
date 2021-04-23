<%@ WebService Language="C#" Class="BIInterface" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class BIInterface  : System.Web.Services.WebService {
    /*範例
    [WebMethod]
    public string getLogonToken(string userID, string password)
    {
        try
        {
            
            CrystalDecisions.Enterprise.SessionMgr sm = new CrystalDecisions.Enterprise.SessionMgr();
            CrystalDecisions.Enterprise.EnterpriseSession es = sm.Logon(userID, password, "", "Enterprise");
            string token = es.LogonTokenMgr.DefaultToken;
            
            //string token = "";
            return token;
        }
        catch (Exception te)
        {
            return "";
        }
    }
    [WebMethod]
    public string getLogonTokenTrusted(string userID)
    {
        try
        {
            string token = "";
            
            CrystalDecisions.Enterprise.SessionMgr sessionMgr = new CrystalDecisions.Enterprise.SessionMgr();
            CrystalDecisions.Enterprise.TrustedPrincipal trustedPrincipal = sessionMgr.CreateTrustedPrincipal(userID, "");
            CrystalDecisions.Enterprise.EnterpriseSession enterpriseSession = sessionMgr.LogonTrustedPrincipal(trustedPrincipal);
            token = enterpriseSession.LogonTokenMgr.DefaultToken;
            
            return token;
        }
        catch (Exception te)
        {
            return "";
        }
    }



    /// <summary>
    /// 檢查BO系統是否有此使用者
    /// </summary>
    /// <param name="UserID">UserID</param>
    /// <returns></returns>
    [WebMethod]
    public string CheckBOUser(string UserID)
    {
        string ret = "none";
        try
        {
            CrystalDecisions.Enterprise.SessionMgr sessionMgr = new CrystalDecisions.Enterprise.SessionMgr();
            CrystalDecisions.Enterprise.EnterpriseSession enterpriseSession = sessionMgr.Logon("administrator", "", "dsc-8kn4za61b97:6400", "Enterprise");
            CrystalDecisions.Enterprise.InfoStore infoStore = (CrystalDecisions.Enterprise.InfoStore)enterpriseSession.GetService("InfoStore");
            //先進行查詢
            string query = "Select * From CI_SYSTEMOBJECTS Where SI_PROGID='CrystalEnterprise.User'";
            CrystalDecisions.Enterprise.InfoObjects IOS = infoStore.Query(query);
            if (IOS.Count > 0) //取到某一筆CrystalDecisions.Enterprise.InfoObject IO = IOS[1];
            {
                CrystalDecisions.Enterprise.InfoObject IO = null;
                for (int i = 1; i < IOS.Count + 1; i++)
                {
                    IO = IOS[i];
                    if (IO.Title == UserID)
                    {
                        ret = "repeat";
                    }
                }


            }

            if (ret == "none")
            {
                CrystalDecisions.Enterprise.PluginManager pluginMgr = infoStore.PluginManager;
                CrystalDecisions.Enterprise.PluginInfo userPlugin = pluginMgr.GetPluginInfo("CrystalEnterprise.User");
                CrystalDecisions.Enterprise.InfoObjects newInfoObjects = infoStore.NewInfoObjectCollection();
                newInfoObjects.Add(userPlugin);

                CrystalDecisions.Enterprise.InfoObject Object = newInfoObjects[1];

                CrystalDecisions.Enterprise.Desktop.User user = (CrystalDecisions.Enterprise.Desktop.User)Object;

                user.Title = UserID;
                user.FullName = UserID;
                user.Description = "經由WebService新增的帳戶";
                user.Connection = (CrystalDecisions.Enterprise.Desktop.CeConnectionType)(2);
                user.PasswordExpires = false;
                user.ChangePasswordAtNextLogon = false;
                user.AllowChangePassword = false;
                user.NewPassword = "";
                infoStore.Commit(newInfoObjects);
                ret = "add";

                pluginMgr.Dispose();
                userPlugin.Dispose();
                newInfoObjects.Dispose();
                Object.Dispose();
                user.Dispose();
                pluginMgr = null;
                userPlugin = null;
                newInfoObjects = null;
                Object = null;
                user = null;
            }
            sessionMgr.Dispose();
            sessionMgr = null;
            enterpriseSession.Dispose();
            enterpriseSession = null;
            infoStore.Dispose();
            infoStore = null;
            return ret;
        }
        catch (Exception EX)
        {
            return EX.ToString();
        }
    }

    [WebMethod]
    public string AddUserToGroup(string UserID, string UserGroupID)
    {
        string ret = "none";
        int intUserID = -1;
        try
        {
            CrystalDecisions.Enterprise.SessionMgr sessionMgr = new CrystalDecisions.Enterprise.SessionMgr();
            CrystalDecisions.Enterprise.EnterpriseSession enterpriseSession = sessionMgr.Logon("administrator", "", "dsc-8kn4za61b97:6400", "Enterprise");
            CrystalDecisions.Enterprise.InfoStore infoStore = (CrystalDecisions.Enterprise.InfoStore)enterpriseSession.GetService("InfoStore");
            CrystalDecisions.Enterprise.PluginManager pluginMgr = infoStore.PluginManager;
            CrystalDecisions.Enterprise.PluginInfo userPlugin = pluginMgr.GetPluginInfo("CrystalEnterprise.UserGroup");

            //先進行查詢
            //string query = "Select * From CI_SYSTEMOBJECTS Where SI_KIND='UserGroup' And SI_ID='" + UserGroupID.ToString() + "'";
            string query = "Select * From CI_SYSTEMOBJECTS Where SI_KIND='UserGroup'";
            string queryUser = "Select * From CI_SYSTEMOBJECTS Where SI_PROGID='CrystalEnterprise.User'";
            CrystalDecisions.Enterprise.InfoObjects IOS = infoStore.Query(query);
            CrystalDecisions.Enterprise.InfoObjects IOSUser = infoStore.Query(queryUser);
            //CrystalDecisions.Enterprise.InfoObjects newInfoObjects = infoStore.Query(query);
            int intIndex = 0;
            //return IOSUser[1].Title;
            if (IOS.Count > 0) //取到某一筆CrystalDecisions.Enterprise.InfoObject IO = IOS[1];
            {
                //全部的群組id
                CrystalDecisions.Enterprise.InfoObject IO = null;
                for (int i = 1; i < IOS.Count + 1; i++)
                {
                    IO = IOS[i];
                    if (IO.Title == UserGroupID)
                    {
                        intIndex = i;
                        break;
                    }
                }
            }

            if (IOSUser.Count > 0)
            {
                //全部的userid
                CrystalDecisions.Enterprise.InfoObject IO = null;
                for (int i = 1; i < IOSUser.Count + 1; i++)
                {
                    IO = IOSUser[i];
                    if (IO.Title == UserID)
                    {
                        intUserID = IO.ID;
                    }
                }
            }

            if (intIndex != 0 && intUserID != -1)
            {
                CrystalDecisions.Enterprise.Desktop.UserGroup myGroup = (CrystalDecisions.Enterprise.Desktop.UserGroup)IOS[intIndex];
                myGroup.Users.Add(intUserID);

                infoStore.Commit(IOS);
                ret = "success";
            }
            return ret;
        }
        catch (Exception EX)
        {
            return ret + ": " + EX.ToString();
        }
    }

    [WebMethod]
    public string DeleteUserFromGroup(string UserID, string UserGroupID)
    {
        string ret = "none";
        int intUserID = -1;
        try
        {
            CrystalDecisions.Enterprise.SessionMgr sessionMgr = new CrystalDecisions.Enterprise.SessionMgr();
            CrystalDecisions.Enterprise.EnterpriseSession enterpriseSession = sessionMgr.Logon("administrator", "", "dsc-8kn4za61b97:6400", "Enterprise");
            CrystalDecisions.Enterprise.InfoStore infoStore = (CrystalDecisions.Enterprise.InfoStore)enterpriseSession.GetService("InfoStore");
            CrystalDecisions.Enterprise.PluginManager pluginMgr = infoStore.PluginManager;
            CrystalDecisions.Enterprise.PluginInfo userPlugin = pluginMgr.GetPluginInfo("CrystalEnterprise.UserGroup");

            //先進行查詢
            //string query = "Select * From CI_SYSTEMOBJECTS Where SI_KIND='UserGroup' And SI_ID='" + UserGroupID.ToString() + "'";
            string query = "Select * From CI_SYSTEMOBJECTS Where SI_KIND='UserGroup'";
            string queryUser = "Select * From CI_SYSTEMOBJECTS Where SI_PROGID='CrystalEnterprise.User'";
            CrystalDecisions.Enterprise.InfoObjects IOS = infoStore.Query(query);
            CrystalDecisions.Enterprise.InfoObjects IOSUser = infoStore.Query(queryUser);
            //CrystalDecisions.Enterprise.InfoObjects newInfoObjects = infoStore.Query(query);
            int intIndex = 0;
            if (IOS.Count > 0) //取到某一筆CrystalDecisions.Enterprise.InfoObject IO = IOS[1];
            {
                CrystalDecisions.Enterprise.InfoObject IO = null;
                for (int i = 1; i < IOS.Count + 1; i++)
                {
                    IO = IOS[i];
                    if (IO.Title == UserGroupID)
                    {
                        intIndex = i;
                        break;
                    }
                }
            }

            if (IOSUser.Count > 0)
            {
                CrystalDecisions.Enterprise.InfoObject IO = null;
                for (int i = 1; i < IOSUser.Count + 1; i++)
                {
                    IO = IOSUser[i];
                    if (IO.Title == UserID)
                    {
                        intUserID = IO.ID;
                    }
                }
            }

            if (intIndex != 0 && intUserID != -1)
            {
                CrystalDecisions.Enterprise.Desktop.UserGroup myGroup = (CrystalDecisions.Enterprise.Desktop.UserGroup)IOS[intIndex];
                myGroup.Users.Delete("#" + intUserID);

                infoStore.Commit(IOS);
                ret = "success";
            }
            //return UserID.PadLeft(6, '0') + "==ws==" + UserGroupID.PadLeft(8, '0');
            return ret;
        }
        catch (Exception EX)
        {
            return ret;
        }
    }

    private string getRandomPassword()
    {
        string passWord = "";
        string passWordIndex = "";
        string[] numericIndex = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        string[] wordIndex = new string[]{"A", "a", "B", "b", "C", "c", "D", "d", "E", "e", "F", "f", "G", "g", "H", "h", "I", "i", 
                                    "J", "j", "K", "k", "L", "l", "M", "m", "N", "n", "O", "o", "P", "p", "Q", "q", "R", "r",
                                    "S", "s", "T", "t", "U", "u", "V", "v", "W", "w", "X", "x", "Y", "y", "Z", "z"};

        Random rdm = new Random();

        passWordIndex = getIndex();

        while (!isLegalPasswordIndex(passWordIndex))
        {
            passWordIndex = getIndex();
        }

        char[] index = passWordIndex.ToCharArray();

        for (int i = 0; i < index.Length; i++)
        {
            int keyIndex;
            if (index[i] == '0')
            {
                keyIndex = rdm.Next(0, 10);
                passWord += numericIndex[keyIndex].ToString();
            }
            else
            {
                keyIndex = rdm.Next(0, 52);
                passWord += wordIndex[keyIndex].ToString();
            }
        }

        return passWord;
    }

    private string getIndex()
    {
        string index = "";
        Random rdm = new Random();

        for (int i = 0; i < 8; i++)
        {
            index += rdm.Next(0, 2);

        }

        return index;
    }

    private bool isLegalPasswordIndex(string passWordIndex)
    {
        char[] index = passWordIndex.ToCharArray();
        int numeric = 0;
        int word = 0;


        for (int i = 0; i < 8; i++)
        {
            if (index[i] == '0')
            {
                numeric += 1;
            }
            else
            {
                word += 1;
            }
        }

        if (numeric > 2 && word > 2)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    
    
    
    */
}

