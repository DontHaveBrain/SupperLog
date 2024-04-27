using SupperLog;

iLogable iLogable = new LogHelper();
iLogable.InitialLog();
 


Task.Factory.StartNew(async () =>
{
    for (int i = 0; i < 1000; i++)
    {
        await Task.Delay(300);
        iLogable.Debug($"你告诉我什么叫做面子{i}\r\n");
    }
});
Task.Factory.StartNew(async () =>
{
    for (int i = 0; i < 1000; i++)
    {
        await Task.Delay(300);
        iLogable.Debug($"皮皮{i}\r\n");
    }
});
Console.ReadKey();