using System;
using System.Drawing;
using System.Runtime.InteropServices;

public static class Mouse
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

    private const uint MOUSEEVENTF_MOVE = 0x0001;
    private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    private const uint MOUSEEVENTF_LEFTUP = 0x0004;
    private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
    private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
    private const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
    private const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
    private const uint MOUSEEVENTF_ABSOLUTE = 0x8000;

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int X, int Y);

    public static void SetCursorPosition(Point position)
    {
        SetCursorPos(position.X, position.Y);
    }

    public static void LeftClick(Point position)
    {
        SetCursorPosition(position);
        mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, (uint)position.X, (uint)position.Y, 0, 0);
    }

    public static void RightClick(Point position)
    {
        SetCursorPosition(position);
        mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, (uint)position.X, (uint)position.Y, 0, 0);
    }

    public static void LeftDown(Point position)
    {
        SetCursorPosition(position);
        mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)position.X, (uint)position.Y, 0, 0);
    }

    public static void LeftUp(Point position)
    {
        SetCursorPosition(position);
        mouse_event(MOUSEEVENTF_LEFTUP, (uint)position.X, (uint)position.Y, 0, 0);
    }

    public static void Move(Point position)
    {
        SetCursorPosition(position);
        mouse_event(MOUSEEVENTF_MOVE, (uint)position.X, (uint)position.Y, 0, 0);
    }
}
