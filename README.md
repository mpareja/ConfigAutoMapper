# ConfigAutoMapper
ConfigAutoMapper performs a basic mapping of configuration settings to plain-old CLR objects (POCOs). The emphasis is on simplicity.

# Sample
This is how you can load a configuration object:

	var mapper = new ConfigAutoMapper(new AppSettingsConfigSource());
	var config = mapper.Load<AppConfig>();

Here is a sample configuration object:

	public class AppConfig {
		public AppConfig() {
			# default values
			DataDir = "data";
			RetryCount = 5;
		}

		public string DataDir { get; set; }
		public int RetryCount { get; set; }	
		protected string RawSiteId { get; set; } // field is mapped

		# not mapped, but depends on mapped field
		public string SiteId { get { return RawSiteId.Substring(0,5); } }
	}

Below is a sample configuration file using the AppSettingsConfigSource. Note the "Config" suffix is automatically stripped out:

	<?xml version="1.0" encoding="utf-8" ?>
	<configuration>
		<appSettings>
			<add key="App.DataDir" value="mydata" />
			<add key="App.RetryCount" value="5" />
			<add key="App.RawSiteId" value="MySite_5312312" />
		</appSettings>
	</configuration>

# Future
This currently fulfills my needs, but I could imagine adding more settings:
* Allow configuring what ConfigSource to use for a configuration object.
* Support nested configuration objects.
* Collection support.
* See if it is feasible to lean on [AutoMapper][http://automapper.org].

