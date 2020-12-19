using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace DesignIntentDesktop
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
        public App()
        {
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MzY5MjI2QDMxMzgyZTM0MmUzMFNDSzF3MXpvR3o3Nmloc2lUYVFqVEFaekVoL0h0dmg2bDVlcDZvcGorRTA9");
		}
	}
}
