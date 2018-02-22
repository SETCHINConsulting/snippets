if (System.Diagnostics.Debugger.IsAttached || (args.Length > 0 && args[0].Equals("debug", System.StringComparison.CurrentCultureIgnoreCase)))
{
	RtsWinService service = new RtsWinService();
	service.DebugRun(); ;
}
else
{
	ServiceBase[] ServicesToRun = new ServiceBase[] { new RtsWinService() };

	ServiceBase.Run(ServicesToRun);
}
