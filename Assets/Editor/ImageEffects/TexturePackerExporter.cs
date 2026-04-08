using UnityEngine;
using UnityEditor;
using System.IO;

public class TexturePackerExporter
{
    [MenuItem("Tools/导出TexturePacker图集中的Sprite")]
    static void ExportSpritesFromAtlas()
    {
        Texture2D atlasTexture = Selection.activeObject as Texture2D;
        if (atlasTexture == null)
        {
            EditorUtility.DisplayDialog("提示", "请先选中一个TexturePacker图集!", "确定");
            return;
        }

        string assetPath = AssetDatabase.GetAssetPath(atlasTexture);
        Object[] sprites = AssetDatabase.LoadAllAssetRepresentationsAtPath(assetPath);

        if (sprites == null || sprites.Length == 0)
        {
            EditorUtility.DisplayDialog("提示", "该图集内没有找到Sprite!", "确定");
            return;
        }

        // ==============================================
        // 【关键修复】自动开启纹理读写权限（解决报错核心）
        // ==============================================
        TextureImporter importer = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        bool oldReadWrite = importer.isReadable;
        importer.isReadable = true;
        importer.SaveAndReimport(); // 强制重新导入

        // 创建保存目录
        string directory = Path.GetDirectoryName(assetPath) + "/" + atlasTexture.name + "_拆分图";
        Directory.CreateDirectory(directory);

        int count = 0;
        foreach (Object spriteObj in sprites)
        {
            Sprite sprite = spriteObj as Sprite;
            if (sprite == null) continue;

            // 读取Sprite像素
            Texture2D newTex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGBA32, false);
            Color[] pixels = sprite.texture.GetPixels(
                (int)sprite.rect.x,
                (int)sprite.rect.y,
                (int)sprite.rect.width,
                (int)sprite.rect.height
            );
            newTex.SetPixels(pixels);
            newTex.Apply();

            // 保存PNG
            byte[] bytes = newTex.EncodeToPNG();
            string savePath = Path.Combine(directory, sprite.name + ".png");
            File.WriteAllBytes(savePath, bytes);
            count++;
        }

        // ==============================================
        // 【可选】导出后自动还原读写权限（保持项目优化）
        // ==============================================
        importer.isReadable = oldReadWrite;
        importer.SaveAndReimport();

        AssetDatabase.Refresh();
        EditorUtility.DisplayDialog("完成", $"成功导出 {count} 张图片！", "确定");
    }
}