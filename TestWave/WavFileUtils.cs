using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace TestWave
{
    public static class WavFileUtils
    {
        public static void TrimWavFile(string inPath, string outPath, TimeSpan cutFromStart, TimeSpan cutFromEnd)
        {
            using (WaveFileReader reader = new WaveFileReader(inPath))
            {
                using (WaveFileWriter writer = new WaveFileWriter(outPath, reader.WaveFormat))
                {
                    
                    double bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000d; // Use "d" suffix, for no-int calculation

                    //long values, more precise and prevents overflow, 64bit int (vs 32bit int)
                    long startPos = (long)(cutFromStart.TotalMilliseconds * bytesPerMillisecond);
                    startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                    //
                    long endBytes = (long)(cutFromEnd.TotalMilliseconds * bytesPerMillisecond);
                    endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                    //
                    long endPos = endBytes;

                    TrimWavFile(reader, writer, startPos, endPos);
                }
            }
        }

        private static void TrimWavFile(WaveFileReader reader, WaveFileWriter writer, long startPos, long endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[reader.WaveFormat.BlockAlign * 100];
            while (reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}