﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFMpegSharp.FFMPEG.Enums;

namespace FFMpegSharp.FFMPEG.Arguments
{
    public class SizeArgument : ScaleArgument
    {
        public SizeArgument()
        {
        }

        public SizeArgument(Size value) : base(value)
        {
        }

        public SizeArgument(VideoSize videosize) : base(videosize)
        {
        }

        public SizeArgument(int width, int heignt) : base(width, heignt)
        {
        }

        public override string GetStringValue()
        {
            return ArgumentsStringifier.Size(Value);
        }
    }
}