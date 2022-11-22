using System;
using System.Collections.Generic;
using System.Text;

namespace PicturePickle
{
    // the global interface for saving the image
    public interface ISave
    {
        void saveFile(byte[] arrayToSave);
    }
}
