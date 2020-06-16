using UnityEngine;

public static class ExtPersistence
{
    public static Vector3 ToVector3(this string str)
    {
        if (str == null)
            return Vector3.zero;

        var values = str.Split(';');
        return new Vector3(float.Parse(values[0]), float.Parse(values[1]), float.Parse(values[2]));
    }

    public static string ToJSON(this Vector3 vector)
    {
        return $"{vector.x};{vector.y};{vector.z};";
    }

    //public static string ToJSON(this Rectangle rect)
    //{
    //    return $"{rect.X};{rect.Y};{rect.Width};{rect.Height}";
    //}

    //public static Rectangle ToRectangle(this string str)
    //{
    //    var values = str.Split(';');
    //    return new Rectangle(int.Parse(values[0]), int.Parse(values[1]), int.Parse(values[2]), int.Parse(values[3]));
    //}

    //public static string ToJSON(this Vector2 vector)
    //{
    //    return $"{vector.X};{vector.Y}";
    //}

    //public static Vector2 ToVector2(this string str)
    //{
    //    if (str == null)
    //        return Vector2.Zero;

    //    var values = str.Split(';');
    //    return new Vector2(float.Parse(values[0]), float.Parse(values[1]));
    //}

    //public static string ToJSON(this Point point)
    //{
    //    return $"{point.X};{point.Y}";
    //}

    //public static Point ToPoint(this string str)
    //{
    //    var values = str.Split(';');
    //    return new Point(int.Parse(values[0]), int.Parse(values[1]));
    //}

    //public static string ToJSON(this Pos point)
    //{
    //    return $"{point.WorldPosition.X};{point.WorldPosition.Y}";
    //}

    //public static Pos ToPos(this string str)
    //{
    //    if (str == null)
    //        return Pos.Empty;

    //    var values = str.Split(';');
    //    return Pos.At(float.Parse(values[0]), float.Parse(values[1]));
    //}
}