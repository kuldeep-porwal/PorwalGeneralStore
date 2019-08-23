using Microsoft.Extensions.Configuration;

namespace PorwalGeneralStore.Utility
{
    public static class ConfigurationMgr
    {
        private static volatile IConfiguration configuration = null;
        private static readonly object configurationLock = new object();

        public static IConfiguration Configuration
        {
            get
            {
                if (configuration != null)
                {
                    return configuration;
                }

                lock (configurationLock)
                {
                    // check to see if some other thread has built the configuration object already
                    if (configuration != null)
                    {
                        return configuration;
                    }

                    BuildConfiguration();

                    return configuration;
                }
            }
        }

        public static void BuildConfiguration(string[] args = null)
        {
            lock (configurationLock)
            {
                // keep the old configuration just in case we fail in building a new configuration object
                IConfiguration old = configuration;
                try
                {
                    // set the static to null, so any other thread accessing this object will be blocked
                    configuration = null;

                    var builder = new ConfigurationBuilder()
                        .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile("appsettings.env.json", optional: true);

                    if (args != null)
                    {
                        builder.AddCommandLine(args);
                    }

                    configuration = builder.Build();
                }
                catch
                {
                    // there was a problem, reverting back to old configuration
                    configuration = old;
                    throw;
                }
            }
        }
    }
}
