//The project is licensed under GPL-3.0, which requires all modifications and distributions to adhere to the same license.

using System;

[Serializable]
public class CloseAppClass
{
    public bool generateCsvReport;
    public string action;
}

[Serializable]
public class StartAppClass
{
    public int[] settings;
    public string sessionId;
}