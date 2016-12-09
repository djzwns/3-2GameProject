using UnityEngine;

/*

*/
public class EnumFlagAttribute : PropertyAttribute {

    public EnumFlagAttribute() { }


    // a타입에 b타입이 있는지 비교
    public static bool HasFlag(Object.EEnvironmentAction _typeA, Object.EEnvironmentAction _typeB)
    {
        return (_typeA & _typeB) == _typeB;
    }

    // a타입에 b타입이 있는지 비교
    public static bool HasFlag(Object.EMoveAction _typeA, Object.EMoveAction _typeB)
    {
        return (_typeA & _typeB) == _typeB;
    }

    // a타입에 b타입이 있는지 비교
    public static bool HasFlag(Object.EQuakeAction _typeA, Object.EQuakeAction _typeB)
    {
        return (_typeA & _typeB) == _typeB;
    }

    // 비교 함수 필요한 거 있으면 추가

}
