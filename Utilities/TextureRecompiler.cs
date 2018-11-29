using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace FlatEarth.Utilities
{
    class TextureRecompiler
    {
        private static string textureDirectory;
        private static string mgcbPath;

        private static string defaultTextureParams = @"
/importer:TextureImporter
/processor:TextureProcessor
/processorParam:ColorKeyColor=255,0,255,255
/processorParam:ColorKeyEnabled=True
/processorParam:GenerateMipmaps=False
/processorParam:PremultiplyAlpha=True
/processorParam:ResizeToPowerOfTwo=False
/processorParam:MakeSquare=False
/processorParam:TextureFormat=Color";

        private static string buildTextureParam = @"
/build:{0}";

        private static string baseParams = @"
/outputDir:bin/$(Platform)
/intermediateDir:obj/$(Platform)
/platform:Windows
/config:
/profile:Reach
/compress:False
";

        private static DateTime LastUpdated;
        static TextureRecompiler()
        {
            LastUpdated = DateTime.Now;
        }

        public static void InitializeWatcher(string mgcbPath, string textureDirectory)
        {
            TextureRecompiler.textureDirectory = textureDirectory;
            TextureRecompiler.mgcbPath = mgcbPath;
        }

        static void CheckForChanges(string rootDirectory)
        {
            var files = Directory.GetFiles(rootDirectory, "*.png", SearchOption.AllDirectories);
            var reloadParameters = baseParams;
            bool doReload = false;

            foreach(var file in files)
            {
                var updatedTime = File.GetLastWriteTime(file);
                if(updatedTime > LastUpdated)
                {
                    reloadParameters += GetTextureReloadParams(file);
                    doReload = true;
                }
            }

            if (doReload)
                ReloadTextures(reloadParameters);
        }

        static string GetTextureReloadParams(string filePath)
        {
            return defaultTextureParams + string.Format(buildTextureParam, filePath);
        }

        static void ReloadTextures(string parameters)
        {
            LastUpdated = DateTime.Now;
            Process contentBuilderProcess = new Process
            {
                StartInfo =
                {
                    FileName = mgcbPath,
                    Arguments = parameters,
                    CreateNoWindow = true,
                    WorkingDirectory = textureDirectory,
                    UseShellExecute = false
                }
            };

            try
            {
                contentBuilderProcess.Start();
                contentBuilderProcess.EnableRaisingEvents = true;
                contentBuilderProcess.Exited += ContentBuilderProcess_Exited;
            }
            catch(Exception ex)
            {

            }
        }

        private static void ContentBuilderProcess_Exited(object sender, EventArgs e)
        {
            ContentManager reloadedContentManager = new ContentManager(Engine.Instance.Content.ServiceProvider, Engine.Instance.Content.RootDirectory);
            Engine.Instance.Content.Unload();
            Engine.Instance.Content.Dispose();
            Engine.Instance.Content = reloadedContentManager;
        }
    }
}
