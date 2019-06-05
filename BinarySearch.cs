using System;
using System.Collections.Generic;

public class BinarySearch
{
    /// <summary>
    /// ある配列でvalueよりも大きい値の内、最小の値を格納しているindexを求める
    /// </summary>
    /// <typeparam name="T">Comparableな型</typeparam>
    /// <param name="array">ソート済みのリスト</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int UpperBound<T>(IList<T> array, T value)
        where T : IComparable<T>
    {
        var low = 0;
        var high = array.Count;
        while (high > low)
        {
            var mid = (high + low) / 2;
            if (array[mid].CompareTo(value) == 1) high = mid;
            else low = mid + 1;
        }
        return low;
    }
    /// <summary>
    /// ある配列でvalue以上の値の内、最小の値を格納しているindexを求める
    /// </summary>
    /// <typeparam name="T">Comparableな型</typeparam>
    /// <param name="array">ソート済みのリスト</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int LowerBound<T>(IList<T> array, T value)
        where T : IComparable<T>
    {
        var low = 0;
        var high = array.Count;
        while (high > low)
        {
            var mid = (high + low) / 2;
            if (array[mid].CompareTo(value) != -1) high = mid;
            else low = mid + 1;
        }
        return low;
    }
}
