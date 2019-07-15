using System;
using System.Collections.Generic;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EventHorizonRPG.Services
{
	public class ClockService
	{
		#region Inherited
		private readonly IConfiguration _config;
		private readonly LogService _logService;
		#endregion

		private Timer Clock;
		public delegate Task ClockMetadata(DateTime TriggerTime);
		public event ClockMetadata OnClockTick;

		public ClockService(LogService logService,IConfiguration configuration)
		{
			_config = configuration;
			_logService = logService;

			Clock = new Timer(Convert.ToInt32(_config["clockinterval"]));
			Clock.AutoReset = true;
			Clock.Elapsed += OnTick;
			Clock.Start();
		}

		private async void OnTick(object sender, ElapsedEventArgs e)
		{
			await OnClockTick?.Invoke(e.SignalTime);
		}

		
	}
}
