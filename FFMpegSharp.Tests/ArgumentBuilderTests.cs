﻿using FFMpegSharp.FFMPEG.Arguments;
using FFMpegSharp.FFMPEG.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFMpegSharp.Tests
{
    [TestClass]
    public class ArgumentBuilderTests : BaseTest
    {
        List<string> concatFiles = new List<string>
        { "1.mp4", "2.mp4", "3.mp4", "4.mp4"};

        FFArgumentBuilder builder;

        public ArgumentBuilderTests() : base()
        {
            builder = new FFArgumentBuilder();
        }

        private string GetArgumentsString(params Argument[] args)
        {
            var container = new ArgumentsContainer();
            container.Add(new OutputArgument("output.mp4"));
            container.Add(new InputArgument("input.mp4"));

            foreach (var a in args)
            {
                container.Add(a);
            }

            return builder.BuildArguments(container);
        }


        [TestMethod]
        public void Builder_BuildString_IO_1()
        {
            var str = GetArgumentsString();

            Assert.IsTrue(str == "-i \"input.mp4\" \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Scale()
        {
            var str = GetArgumentsString(new ScaleArgument(VideoSize.Hd));

            Assert.IsTrue(str == "-i \"input.mp4\" -vf scale=-1:720 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_AudioCodec()
        {
            var str = GetArgumentsString(new AudioCodecArgument(AudioCodec.Aac, AudioQuality.Normal));

            Assert.IsTrue(str == "-i \"input.mp4\" -codec:a aac -b:a 128k -strict experimental \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_BitStream()
        {
            var str = GetArgumentsString(new BitStreamFilterArgument(Channel.Audio, Filter.H264_Mp4ToAnnexB));

            Assert.IsTrue(str == "-i \"input.mp4\" -bsf:a h264_mp4toannexb \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Concat()
        {
            var container = new ArgumentsContainer();
            container.Add(new OutputArgument("output.mp4"));

            container.Add(new ConcatArgument(concatFiles));

            var str = builder.BuildArguments(container);

            Assert.IsTrue(str == "-i \"concat:1.mp4|2.mp4|3.mp4|4.mp4\" \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Copy_Audio()
        {
            var str = GetArgumentsString(new CopyArgument(Channel.Audio));

            Assert.IsTrue(str == "-i \"input.mp4\" -c:a copy \"output.mp4\"");
        }


        [TestMethod]
        public void Builder_BuildString_Copy_Video()
        {
            var str = GetArgumentsString(new CopyArgument(Channel.Video));

            Assert.IsTrue(str == "-i \"input.mp4\" -c:v copy \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Copy_Both()
        {
            var str = GetArgumentsString(new CopyArgument(Channel.Both));

            Assert.IsTrue(str == "-i \"input.mp4\" -c copy \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_CpuSpeed()
        {
            var str = GetArgumentsString(new CpuSpeedArgument(10));

            Assert.IsTrue(str == "-i \"input.mp4\" -quality good -cpu-used 10 -deadline realtime \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_ForceFormat()
        {
            var str = GetArgumentsString(new ForceFormatArgument(VideoCodec.LibX264));

            Assert.IsTrue(str == "-i \"input.mp4\" -f libx264 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_FrameOutputCount()
        {
            var str = GetArgumentsString(new FrameOutputCountArgument(50));

            Assert.IsTrue(str == "-i \"input.mp4\" -vframes 50 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_FrameRate()
        {
            var str = GetArgumentsString(new FrameRateArgument(50));

            Assert.IsTrue(str == "-i \"input.mp4\" -r 50 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Loop()
        {
            var str = GetArgumentsString(new LoopArgument(50));

            Assert.IsTrue(str == "-i \"input.mp4\" -loop 50 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Seek()
        {
            var str = GetArgumentsString(new SeekArgument(TimeSpan.FromSeconds(10)));

            Assert.IsTrue(str == "-i \"input.mp4\" -ss 00:00:10 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Shortest()
        {
            var str = GetArgumentsString(new ShortestArgument(true));

            Assert.IsTrue(str == "-i \"input.mp4\" -shortest \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Size()
        {
            var str = GetArgumentsString(new SizeArgument(1920, 1080));

            Assert.IsTrue(str == "-i \"input.mp4\" -s 1920x1080 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Speed()
        {
            var str = GetArgumentsString(new SpeedArgument(Speed.Fast));

            Assert.IsTrue(str == "-i \"input.mp4\" -preset fast \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_StartNumber()
        {
            var str = GetArgumentsString(new StartNumberArgument(50));

            Assert.IsTrue(str == "-i \"input.mp4\" -start_number 50 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Threads_1()
        {
            var str = GetArgumentsString(new ThreadsArgument(50));

            Assert.IsTrue(str == "-i \"input.mp4\" -threads 50 \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Threads_2()
        {
            var str = GetArgumentsString(new ThreadsArgument(true));

            Assert.IsTrue(str == $"-i \"input.mp4\" -threads {Environment.ProcessorCount} \"output.mp4\"");
        }


        [TestMethod]
        public void Builder_BuildString_Codec()
        {
            var str = GetArgumentsString(new VideoCodecArgument(VideoCodec.LibX264));

            Assert.IsTrue(str == "-i \"input.mp4\" -codec:v libx264 -pix_fmt yuv420p \"output.mp4\"");
        }

        [TestMethod]
        public void Builder_BuildString_Codec_Override()
        {
            var str = GetArgumentsString(new VideoCodecArgument(VideoCodec.LibX264), new OverrideArgument());

            Assert.IsTrue(str == "-i \"input.mp4\" -codec:v libx264 -pix_fmt yuv420p \"output.mp4\" -y");
        }
    }
}
