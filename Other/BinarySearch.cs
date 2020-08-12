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
    public static int UpperBound<T>(IList<T> array, T value, Comparison<T> cmp = null)
    {
        cmp = cmp ?? Comparer<T>.Default.Compare;
        var low = -1;
        var high = array.Count;
        while (high - low > 1)
        {
            var mid = (high + low) / 2;
            if (cmp(array[mid], value) == 1) high = mid;
            else low = mid;
        }
        return high;
    }
    /// <summary>
    /// ある配列でvalue以上の値の内、最小の値を格納しているindexを求める
    /// </summary>
    /// <typeparam name="T">Comparableな型</typeparam>
    /// <param name="array">ソート済みのリスト</param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int LowerBound<T>(IList<T> array, T value, Comparison<T> cmp = null)
    {
        cmp = cmp ?? Comparer<T>.Default.Compare;
        var low = -1;
        var high = array.Count;
        while (high - low > 1)
        {
            var mid = (high + low) / 2;
            if (cmp(array[mid], value) != -1) high = mid;
            else low = mid;
        }
        return high;
    }
}
