﻿using SaintDenis_Launcher.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintDenis_Launcher.Tools.Handlers
{
    internal class PlaceAzerty
    {
        private static readonly String sourceFile = @"./Resources/default.meta";
        private static readonly String destinationFile = Settings.Default.RedmFolder + @"\RedM.app\citizen\platform\data\control\default.meta";

        /// <summary>
        /// Move File Asynchronously
        /// </summary>
        /// <exception cref="IOException"></exception>
        public static async Task MoveFile()
        {
            using FileStream sourceStream = new(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
            using FileStream destinationStream = new(destinationFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None, 4096, FileOptions.Asynchronous | FileOptions.SequentialScan);
            if (destinationStream.CanWrite)
            {
                await sourceStream.CopyToAsync(destinationStream);
            }
        }
    }
}
