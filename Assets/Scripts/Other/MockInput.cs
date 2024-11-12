using System.Collections.Generic;

/*
    This class is a mock implementation of the Input class for testing purposes.
    It allows setting the values of the axes for testing the movement of the follower character.
*/

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
