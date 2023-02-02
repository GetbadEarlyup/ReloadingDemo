using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PictureTexMo : MonoBehaviour
{
    //更多请访问：CSDN作孽就得先起床
    public GameObject[] prefab;
    // Start is called before the first frame update
    void Start()
    {
        //导出模型预览图代码
        for (int i = 0; i < prefab.Length; i++)
        {
            EditorUtility.SetDirty(prefab[i]);
            Texture2D image = AssetPreview.GetAssetPreview(prefab[i]);
            System.IO.File.WriteAllBytes(Application.dataPath + "/Resolution/image_"+i+".png", image.EncodeToPNG());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
