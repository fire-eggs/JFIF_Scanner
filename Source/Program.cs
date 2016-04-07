using System;
using System.IO;

namespace JFIF_Scanner
{
    class Program
    {
        static void checkFiles(string[] files)
        {
            const int READ_SIZE = 32;
            byte[] bom = new byte[READ_SIZE];

            foreach (var afile in files)
            {
                try
                {
                    using (FileStream fileStream = File.OpenRead(afile))
                    {
                        fileStream.Read(bom, 0, READ_SIZE);
                        for (int i = 0; i < READ_SIZE - 4; i++) // take the signature look-ahead into account
                        {
                            // search for 'JFIF' signature
                            if (bom[i] != 'J' || bom[i + 1] != 'F' || bom[i + 2] != 'I' || bom[i + 3] != 'F') 
                                continue;

                            // found it.
                            int units = bom[i + 7];
                            int xd = bom[i + 8] * 256 + bom[i + 9];
                            int yd = bom[i + 10] * 256 + bom[i + 11];
                            if (units == 0) // zero for units appears not to be a problem
                                break; // don't search any further
                            if (xd != yd)
                            {
                                if (xd == 1 || yd == 1)
                                    Console.WriteLine("Thin image: {0} ({3}:{1}x{2})", afile, xd, yd, units);
                                else if (yd != 0) // zero for ydensity appears not to be a problem
                                    Console.WriteLine("Stretch image: {0} ({3}:{1}x{2})", afile, xd, yd, units);
                            }

                            break; // don't look any further [at least one image with two JFIF signatures...]
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("***Error with image {0}:{1}", afile, ex.Message);
                }
            }
        }

        static void checkFolder(string path)
        {
            var files = Directory.GetFiles(path, "*.jpg");
            checkFiles(files);
            files = Directory.GetFiles(path, "*.jpeg");
            checkFiles(files);
            var folders = Directory.GetDirectories(path);
            foreach (var folder in folders)
            {
                checkFolder(folder);
            }
        }

        static void Main(string[] args)
        {
            string apath = args[0];
            checkFolder(apath);
        }
    }
}
