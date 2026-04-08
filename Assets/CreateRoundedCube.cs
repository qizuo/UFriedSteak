using UnityEngine;

public class CreateRoundedCube : MonoBehaviour
{
    [ContextMenu("生成不规则圆润低模方块")]
    void GenerateCube()
    {
        // 创建新网格
        Mesh mesh = new Mesh();
        mesh.name = "LowPolyIrregularCube";

        // 不规则圆角方块顶点
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-1,-1,-1), new Vector3(-1,-1,1), new Vector3(-1,1,-1), new Vector3(-1,1,1),
            new Vector3(1,-1,-1), new Vector3(1,-1,1), new Vector3(1,1,-1), new Vector3(1,1,1),
            
            new Vector3(-0.8f,-1.2f,-0.8f), new Vector3(-0.8f,-1.2f,0.8f),
            new Vector3(-0.8f,1.2f,-0.8f), new Vector3(-0.8f,1.2f,0.8f),
            new Vector3(0.8f,-1.2f,-0.8f), new Vector3(0.8f,-1.2f,0.8f),
            new Vector3(0.8f,1.2f,-0.8f), new Vector3(0.8f,1.2f,0.8f),
            
            new Vector3(-1.2f,-0.8f,-0.8f), new Vector3(-1.2f,-0.8f,0.8f),
            new Vector3(-1.2f,0.8f,-0.8f), new Vector3(-1.2f,0.8f,0.8f),
            new Vector3(1.2f,-0.8f,-0.8f), new Vector3(1.2f,-0.8f,0.8f),
            new Vector3(1.2f,0.8f,-0.8f), new Vector3(1.2f,0.8f,0.8f),
        };

        // 三角形面
        int[] triangles = new int[]
        {
            0,1,3,0,3,2,
            2,3,7,2,7,6,
            6,7,5,6,5,4,
            4,5,1,4,1,0,
            
            8,9,11,8,11,10,
            10,11,15,10,15,14,
            14,15,13,14,13,12,
            12,13,9,12,9,8,
            
            16,17,19,16,19,18,
            18,19,23,18,23,22,
            22,23,21,22,21,20,
            20,21,17,20,17,16,
            
            0,8,16,
            1,9,17,
            3,11,19,
            2,10,18,
            4,12,20,
            5,13,21,
            7,15,23,
            6,14,22,
        };

        // 赋值网格
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
        mesh.RecalculateBounds();

        // 创建游戏对象
        GameObject cube = new GameObject("IrregularLowPolyCube");
        MeshFilter filter = cube.AddComponent<MeshFilter>();
        MeshRenderer renderer = cube.AddComponent<MeshRenderer>();
        
        filter.mesh = mesh;
        renderer.material = new Material(Shader.Find("Standard"));
        
        // 保存模型到本地（可选）
        SaveMesh(mesh, "LowPolyIrregularCube", true, true);
    }

    // 保存网格为.asset文件
    void SaveMesh(Mesh mesh, string name, bool makeNewInstance, bool optimize)
    {
        string path = "Assets/" + name + ".asset";
        Mesh instance = mesh;
        
        if (makeNewInstance)
        {
            instance = Object.Instantiate(mesh);
            instance.name = name;
        }
        
        if (optimize)
            instance.Optimize();
        
        UnityEditor.AssetDatabase.CreateAsset(instance, path);
        UnityEditor.AssetDatabase.SaveAssets();
        UnityEditor.AssetDatabase.Refresh();
        
        Debug.Log("模型已保存到：" + path);
    }
}