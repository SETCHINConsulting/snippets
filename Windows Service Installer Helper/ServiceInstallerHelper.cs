// **************************************************************
// <copyright file="ServiceInstallerHelper.cs" company="Open Wave">
// Copyright (c) Open Wave. All rights reserved.
// </copyright>
// <author>
// Vlad Setchin
// </author>
// **************************************************************

namespace OpenWave.RTS.Services
    {
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.ServiceProcess;
    using System.Configuration.Install;
    using System.IO;
    using System.Windows.Forms;

    /// <summary>
    /// Helper Class for Service Installer
    /// </summary>
    public class ServiceInstallerHelper
        {
        #region Constants
        /// <summary>
        /// Name of the configuration file
        /// </summary>
        private const string ConfigFileName = "Rts.exe.config";

        /// <summary>
        /// Template of the Log Directory Path to be replaced during installation
        /// </summary>
        private const string LogDirPathTemplate = @"C:\Rts\logs";

        /// <summary>
        /// Template of the archive directory path to be replaced during installation
        /// </summary>
        private const string ArchiveDirPathTemplate = @"C:\Rts\archives";

        /// <summary>
        /// Name of the Logs Directory
        /// </summary>
        private const string LogDirName = "logs";

        /// <summary>
        /// Name of the archive directory
        /// </summary>
        private const string ArchiveDirName = "archives";

        /// <summary>
        /// Name of the Service
        /// </summary>
        public const string ServiceName = "rts";

        /// <summary>
        /// Description of the Service
        /// </summary>
        private const string ServiceDescription = "Noble Systems RT Scheduler Service runs scheduled scripts";

        private const string InstallDirParam = "INSTALLDIR";
        #endregion Constants

        #region Variables
        private static ServiceInstaller serviceInstaller = null;
        #endregion Variables

        #region Public Functions
        /// <summary>
        /// Initialises the windows service installer.
        /// </summary>
        /// <param name="serviceInstaller">The service installer.</param>
        public static void InitWindowsServiceInstaller(ServiceInstaller serviceInstaller)
            {
            #region Service Installer
            serviceInstaller.DisplayName = ServiceName;
            serviceInstaller.ServiceName = ServiceName;
            serviceInstaller.Description = ServiceDescription;

            ServiceInstallerHelper.serviceInstaller = serviceInstaller;
            #endregion Service Installer
            }

        /// <summary>
        /// Runs the install custom actions.
        /// </summary>
        /// <param name="installContext">The install context.</param>
        public static void RunInstallCustomActions(InstallContext installContext)
            {
            if (installContext.Parameters.ContainsKey(ServiceInstallerHelper.InstallDirParam))
                {
                ServiceInstallerHelper.EditConfigFile(installContext.Parameters[ServiceInstallerHelper.InstallDirParam]);
                }
            }

        /// <summary>
        /// Runs the uninstall custom actions.
        /// </summary>
        public static void RunUninstallCustomActions()
            {

            }
        #endregion Public Functions

        #region Private Functions
#if DEBUG
        /// <summary>
        /// Edits the configuration file.
        /// </summary>
        /// <param name="installDirectory">The install directory.</param>
        public static void EditConfigFile(string installDirectory)
#else
        private static void EditConfigFile(string installDirectory)
#endif
            {
            string fileContents = string.Empty;
            string configFilePath = Path.Combine(installDirectory, ConfigFileName);

            try
                {
                // read configuration file and replace strings
                StreamReader sr = new StreamReader(configFilePath);
                fileContents = sr.ReadToEnd();

                // LogDirPath
                if (fileContents.Contains(ServiceInstallerHelper.LogDirPathTemplate))
                    {
                    // replace from e-mail address
                    fileContents
                        = fileContents.Replace(
                        ServiceInstallerHelper.LogDirPathTemplate,
                        Path.Combine(installDirectory,
                        ServiceInstallerHelper.LogDirName));
                    }

                if (fileContents.Contains(ServiceInstallerHelper.ArchiveDirPathTemplate))
                    {
                    // replace from address name 
                    fileContents
                        = fileContents.Replace(ServiceInstallerHelper.ArchiveDirPathTemplate,
                        Path.Combine(installDirectory,
                        ServiceInstallerHelper.ArchiveDirName));
                    }

                sr.Close();
                }
            catch (Exception ex)
                {
                throw new Exception("Unable to read configuration file: " + ConfigFileName, ex);
                }

            // replace configuration file contents
            try
                {
                StreamWriter sw = new StreamWriter(configFilePath);

                sw.Write(fileContents);

                sw.Close();
                }
            catch (Exception ex)
                {
                throw new Exception("Unable to write configuration file: " + ConfigFileName, ex);
                }
            }
        #endregion Private Functions
        }
    }
