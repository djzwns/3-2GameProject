//using System;
using UnityEditor;  // 에디터 사용을 위해 추가
using UnityEngine;

// EnumFlagAttribute 를 위한 드로워, inspector 창에 나타남
[CustomPropertyDrawer(typeof(EnumFlagAttribute))]
public class EnumFlagDrawer : PropertyDrawer {

    public override void OnGUI(Rect _position, SerializedProperty _property, GUIContent _label)
    {
        // enum 변수에 저장 될 최종 값
        int nButtonsIntValue = 0;

        // enum안의 프로퍼티의 개수. 생성될 버튼의 수와 같음.
        int nEnumLength = _property.enumNames.Length;

        // 버튼이 눌렸는지를 판단하기 위한 변수. 버튼의 수와 같음.
        bool[] bButtonPressed = new bool[nEnumLength];

        // 버튼 너비
        float fButtonWidth = (_position.width - EditorGUIUtility.labelWidth) / nEnumLength;
        
        EditorGUI.LabelField(new Rect(_position.x, _position.y, EditorGUIUtility.labelWidth, _position.height), _label);

        EditorGUI.BeginChangeCheck();

        for (int i = 0; i < nEnumLength; i++)
        {
            // 누른 버튼과 같은 번호에 눌림 표시
            if ((_property.intValue & (1 << i)) == 1 << i)
            {
                bButtonPressed[i] = true;
            }

            Rect buttonPos = new Rect(_position.x + EditorGUIUtility.labelWidth + fButtonWidth * i, _position.y, fButtonWidth, _position.height);

            bButtonPressed[i] = GUI.Toggle(buttonPos, bButtonPressed[i], _property.enumNames[i], "Button");

            // 눌린 버튼이면 값을 추가
            if (bButtonPressed[i])
                nButtonsIntValue += 1 << i;
        }

        // 최종 값을 저장
        if (EditorGUI.EndChangeCheck())
        {
            _property.intValue = nButtonsIntValue;
        }
    }
}
