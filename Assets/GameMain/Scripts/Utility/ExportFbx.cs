
using UnityEditor;
using UnityEngine;

public class ExportFbx : MonoBehaviour
{

    // [MenuItem("ExportFbx Editor/Create FBX")]
    static void CreateFBX()
    {
        // 项目视图Project 选择的物体对象
        // Object[] SelectedAsset = Selection.GetFiltered(typeof(Object), SelectionMode.DeepAssets);
        // GameObject[] selectObjs = new GameObject[SelectedAsset.Length];
        // for (int i = 0; i < SelectedAsset.Length; i++)
        // {
        //     selectObjs[i] = (GameObject)SelectedAsset[i];
        // }

        // foreach (Object obj in SelectedAsset)
        // {
        //     FBXExporter.ExportFBX("Assets/", obj.name, selectObjs, true);
        //     Debug.Log(obj.name);    
        // }
        // Debug.Log("xixi");
        // //刷新编辑器
        // AssetDatabase.Refresh();
    }
}
