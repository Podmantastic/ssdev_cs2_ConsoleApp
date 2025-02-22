using System;
using System.Reflection;
using ConsoleApp.Boot;
using log4net;

ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(Program));

Bootstrapper.ResolveGenerator().Do();

Log.Info("Press a key to quit.");
Console.ReadKey();
