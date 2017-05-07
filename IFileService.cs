using System;
using System.Collections.Generic;

namespace TZ
{
    interface IFileService
    {
        List<StudetModel> Open(string filename);
        void Save(string filename, List<StudetModel> studentList);
    }
}
