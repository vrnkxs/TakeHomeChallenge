using System;

namespace PlaywrightGoogleTest.Utilities;

/*
 * A simple logging utility used to print consistent, readable messages during test execution. 
 * Keeps Page Objects clean and makes it easier to track what actions the test performs at each step.
 */
public class Logger
{
    // Logs general test actions (navigation, typing, clicking).
    public static void Info(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"[INFO] {DateTime.Now:HH:mm:ss} - {message}");
        Console.ResetColor();
    }

    // Logs successful validations or completed actions.
    public static void Success(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[OK]   {DateTime.Now:HH:mm:ss} - {message}");
        Console.ResetColor();
    }

    // Logs non-critical issues or fallback behavior.
    public static void Warning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"[WARN] {DateTime.Now:HH:mm:ss} - {message}");
        Console.ResetColor();
    }

    // Logs errors or unexpected behavior to help troubleshooting.
    public static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ERROR] {DateTime.Now:HH:mm:ss} - {message}");
        Console.ResetColor();
    }
}