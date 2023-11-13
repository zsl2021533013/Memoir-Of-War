using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class ChangeModelMaterils : Editor
{
    public static GameObject targetModel;
    private static Material[] modelMaterials;
    public static Material srcTargett;
    //创建多级菜单并声明顺序
    [MenuItem("编辑器拓展/批量替换", false, 100)]
    public static void Menu1()
    {
        // 在此处填写场景中目标无提名
        targetModel = GameObject.Find("Janissaries");
        // 遍历处理的静态方法，当遍历到一个子物体后就会触发相应的处理事件，此处的事件是替换指定的材质
        GetGOAllChilren<Renderer>(targetModel, (Renderer r) =>
        {
            Material mat = Resources.Load<Material>("Art/Material/Enemy/Dissolve Fantasy Hero Mat");
            r.sharedMaterial = mat;
        });
    }
    // 遍历获取所有子物体
    public static void GetGOAllChilren<W>(GameObject go, Action<W> a)
    {
        if (go.transform.childCount > 0)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                GameObject g = go.transform.GetChild(i).gameObject;
                GetGOAllChilren<W>(g, a);
            }
        }

        if (go.TryGetComponent<W>(out W w))
        {
            a?.Invoke(w);
        };
    }
}