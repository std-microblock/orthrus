// Copyright (C) 2020 grappigegovert <grappigegovert@hotmail.com>
// Licensed under the zlib license. See LICENSE for more info

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HitmanPatcher
{

    public class Settings
    {
        public MemoryPatcher.Options patchOptions;
        public bool startInTray;
        public bool minimizeToTray;
        public List<string> domains;

        private static string localpath = "patcher.conf";
        private static string appdatapath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "LocalGhost", "patcher.conf");

        public Settings()
        {
            // Default settings
            this.patchOptions = new MemoryPatcher.Options
            {
                DisableCertPinning = true,
                AlwaysSendAuthHeader = true,
                SetCustomConfigDomain = true,
                CustomConfigDomain = "localhost:5866",
                UseHttp = true,
                DisableForceOfflineOnFailedDynamicResources = true,
                ForcedVersion = ""
            };
            this.startInTray = false;
            this.minimizeToTray = false;
            this.domains = new List<string>();
        }

        public void saveToFile(string path)
        {

        }

        public static Settings getFromFile(string path)
        {
            Settings result = new Settings();
            return result;
        }

        public static Settings Load()
        {
            Settings result = new Settings();
            return result;
        }

        public void Save()
        {
            string dir = Path.GetDirectoryName(appdatapath);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            this.saveToFile(appdatapath);
        }
    }
}
