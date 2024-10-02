using System.Collections.Generic;

public static class MockInput
{
    private static Dictionary<string, float> axes = new Dictionary<string, float>();

    public static float GetAxis(string axisName)
    {
        if (axes.TryGetValue(axisName, out float value))
        {
            return value;
        }
        return 0;
    }

    public static void SetAxis(string axisName, float value)
    {
        axes[axisName] = value;
    }

    public static void Reset()
    {
        axes.Clear();
    }
}
