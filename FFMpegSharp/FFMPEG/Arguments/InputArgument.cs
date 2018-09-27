﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FFMpegSharp.FFMPEG.Arguments
{
    public class InputArgument : Argument<string>
    {
        public InputArgument()
        {
        }

        public InputArgument(string value) : base(value)
        {
        }

        public InputArgument(VideoInfo value) : base(value.FullName)
        {
        }

        public InputArgument(FileInfo value) : base(value.FullName)
        {
        }

        public InputArgument(Uri value) : base(value.AbsolutePath)
        {
        }

        public override string GetStringValue()
        {
            return ArgumentsStringifier.Input(Value);
        }
    }
}
