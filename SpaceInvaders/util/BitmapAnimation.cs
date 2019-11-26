using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders.util
{
    class BitmapAnimation
    {
        private Bitmap[] frames;
        private int counter = 0;

        public BitmapAnimation(Bitmap[] frames)
        {
            this.frames = frames;
        }

        public BitmapAnimation()
        {
            frames = null;
        }

        public Bitmap GetNextFrame()
        {
            if (counter < frames.Length)
            {
                return frames[counter++];
            } 
            else
            {
                return frames[frames.Length - 1];
            }
        }



    }
}
