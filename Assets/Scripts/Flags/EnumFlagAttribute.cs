using UnityEngine;

/*

*/
public class EnumFlagAttribute : PropertyAttribute {

    public EnumFlagAttribute() { }


    // a타입에 b타입이 있는지 비교
    public static bool HasFlag(EEnvironmentAction _typeA, EEnvironmentAction _typeB)
    {
        return (_typeA & _typeB) == _typeB;
    }

    // a타입에 b타입이 있는지 비교
    public static bool HasFlag(EMoveAction _typeA, EMoveAction _typeB)
    {
        return (_typeA & _typeB) == _typeB;
    }

    // a타입에 b타입이 있는지 비교
    public static bool HasFlag(EQuakeAction _typeA, EQuakeAction _typeB)
    {
        return (_typeA & _typeB) == _typeB;
    }

    // 비교 함수 필요한 거 있으면 추가

}
