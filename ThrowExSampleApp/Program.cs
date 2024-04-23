// CA2200: Rethrow to preserve stack details
// https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2200

var justThrowEx = (int time) =>
{
    try
    {
        RecursiveFunction(time);
    }
    catch (Exception)
    {
        throw;
    }
};
var reThrowEx = (int time) =>
{
    try
    {
        RecursiveFunction(time);
    }
    catch (Exception ex)
    {
        throw ex;
    }
};
Console.WriteLine("Output:\n------------------------------");
ShowExMessageAndStackTrace(justThrowEx, 3);
/* 
Throw the exception without any modification and keep the original stack trace

Output:
------------------------------
This is exception in RecursiveFunction (0)
   at Program.<<Main>$>g__RecursiveFunction|0_3(Int32 count) in C:\ThrowExSampleApp\Program.cs:line 47
   at Program.<<Main>$>g__RecursiveFunction|0_3(Int32 count) in C:\ThrowExSampleApp\Program.cs:line 49
   at Program.<<Main>$>g__RecursiveFunction|0_3(Int32 count) in C:\ThrowExSampleApp\Program.cs:line 49
   at Program.<<Main>$>g__RecursiveFunction|0_3(Int32 count) in C:\ThrowExSampleApp\Program.cs:line 49
   at Program.<>c.<<Main>$>b__0_0(Int32 time) in C:\ThrowExSampleApp\Program.cs:line 8
   at Program.<<Main>$>g__ShowExMessageAndStackTrace|0_2(Action`1 action, Int32 time) in C:\ThrowExSampleApp\Program.cs:line 36
 */

Console.WriteLine();
Console.WriteLine("Output:\n------------------------------");
ShowExMessageAndStackTrace(reThrowEx, 3);
/* 
Re-throw the exception and loss the original stack trace

Output:
------------------------------
This is exception in RecursiveFunction (0)
   at Program.<>c.<<Main>$>b__0_1(Int32 time) in C:\ThrowExSampleApp\Program.cs:line 25
   at Program.<<Main>$>g__ShowExMessageAndStackTrace|0_2(Action`1 action, Int32 time) in C:\ThrowExSampleApp\Program.cs:line 36
 */

void ShowExMessageAndStackTrace(Action<int> action, int time)
{
    try
    {
        action(time);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.WriteLine(ex.StackTrace);
    }
}

void RecursiveFunction(int count)
{
    if (count == 0) throw new MyException($"This is exception in {nameof(RecursiveFunction)} ({count})");
    count--;
    RecursiveFunction(count);
}

public class MyException(string message) : Exception(message) { }
