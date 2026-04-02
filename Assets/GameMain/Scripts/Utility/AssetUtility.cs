//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2020 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using GameFramework;
using UnityEngine.UI;
using UnityEngine;

namespace StarForce
{
    public static class AssetUtility
    {
     
        public static string GetDataTableAsset(string assetName, bool fromBytes)
        {
            return Utility.Text.Format("Assets/GameMain/DataTables/{0}.{1}", assetName, fromBytes ? "bytes" : "txt");
        }

        public static string GetUIFormAsset(string assetName)
        {
            return Utility.Text.Format("Assets/GameMain/UI/UIForms/{0}.prefab", assetName);
        }

        public static string getFloorBasePrefab(int id)
        {
            return Utility.Text.Format("Prefabs/baseHind/item{0}",id);
        }

        public static string getFloorBasePrefab(string name)
        {
            return Utility.Text.Format("Prefabs/baseHind/{0}",name);
        }

        public static string getShoesPrefab(int id)
        {
            return Utility.Text.Format("Prefabs/shoes/item{0}",id);
        }
        public static string getShoesPrefab(string name)
        {
            return Utility.Text.Format("Prefabs/shoes/{0}",name);
        }

        public static string getFloorHdPrefab(int id)
        {
            return Utility.Text.Format("Prefabs/hdHind/item{0}",id);
        }

        public static string getFloorHdSybPrefab(string name)
        {
            return Utility.Text.Format("Prefabs/hdHind/{0}",name);
        }

        public static string getRoomPrefab(string name)
        {
            return Utility.Text.Format("Prefabs/map/{0}",name);
        }
    
        public static void toChangePlayerLayer(GameObject obj,int layerValue)
        {
            Transform[] transArray = obj.GetComponentsInChildren<Transform>();
            foreach (Transform trans in transArray) 
            {
                trans.gameObject.layer = layerValue;
            }
        }
    }
}
