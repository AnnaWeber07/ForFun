using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace EAndTree
{
    class RootSynchro
    {
        private object _syncRoot; //объект локировки
                                  //с его помощью один поток не сможет зайти в lock,
                                  //пока другой из него не выйдет

        string path = @"c:\users\анна\source\repos\eandtree\EAndTree\ConstE.txt";

        string ReadFile()
        {
            lock (_syncRoot)
            {
                //OpenFile, Read, CloseFile, Return

                var myFile = File.OpenRead(path);
                string copiedInfo = File.ReadAllText(path);
                myFile.Close();

                return copiedInfo;
            }
        }

        string WriteFile(string[] lines)
        {
            lock (_syncRoot)
            {
                //OpenFile, Write, CloseFile, Return

                var myFile = File.OpenWrite(path);
                using (StreamWriter outFile = new StreamWriter(path))
                {
                    foreach (string line in lines)
                        outFile.Write(line);
                }
                    myFile.Close();

                //return ;
            }
        }
    }
}
