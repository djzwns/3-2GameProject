using UnityEngine;
using UnityEditor;
using System.IO;
/*
    CreateAsset 함수를 통해 애셋을 생성시킨다.
    ScriptableObject 의 속성을 가진 스크립트만을 생성할 수 있다.
    만들어진 애셋을 생성하고 저장시키기 까지하면 마무리됨.

    - Selection.activeObject : 현재 프로젝트 창에서 활성화 된 폴더의 오브젝트를 받아옴. 혹은 set으로 활성화 시킬 수 있음
    - AssetDatabase
        .GetAssetPath : 애셋의 경로를 받아오는데 여기서 사용된 건 활성화된 폴더의 위치를 받아와 경로를 저장함.
        .CreateAsset : 만들어진 애셋을 경로에 생성함
        .SaveAssets : 생성되고 아직 저장되지 않은 애셋들을 디스크에 저장
        .Refresh : 갱신된 애셋들 임포트
    - EditorUtility.FocusProjectWindow : 프로젝트를 맨 앞으로, 윈도우창을 맨위로 올려주는 듯,
*/
public class CustomAssetUtility {

    public static void CreateAsset<T>() where T : ScriptableObject
    {
        T asset = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (path == "")
        {
            path = "Assets";
        }
        else if (Path.GetExtension(path) != "")
        {
            path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
        }

        string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

        AssetDatabase.CreateAsset(asset, assetPathAndName);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
