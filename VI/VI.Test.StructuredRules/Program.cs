using System.Collections.Generic;
using VI.NumSharp;
using VI.ParallelComputing;
using VI.Test.StructuredRules;
using VI.Test.StructuredRules.DataTools;
using VI.Test.StructuredRules.Maggie;
using VI.Test.StructuredRules.Tools;

namespace Test
{
    class Program
    {
#if DEBUG
        static string urlPallets = @"C:\Projects\GitHub\Virtual-Intelligence\VI\VI.Test.StructuredRules\Data\Database\mapsSalvador.txt";
        static string urlJsonItems = @"C:\Projects\GitHub\Virtual-Intelligence\VI\VI.Test.StructuredRules\Data\Database\itemsDetails_v2.json";
        static string urlWeights = @"C:\Projects\GitHub\Virtual-Intelligence\VI\VI.Test.StructuredRules\Data\Knowledge";
#else
        static string urlPallets = @"C:\Projects\GitHub\Virtual-Intelligence\VI\VI.Test.StructuredRules\Data\Database\mapsSalvador.txt";
        static string urlJsonItems = @"C:\Projects\GitHub\Virtual-Intelligence\VI\VI.Test.StructuredRules\Data\Database\itemsDetails_v2.json";
        static string urlWeights = @"C:\Projects\GitHub\Virtual-Intelligence\VI\VI.Test.StructuredRules\Data\Knowledge";
#endif

        static void Main(string[] args)
        {
            ProcessingDevice.Device = DeviceType.CPU;

            var reader = new FilesReaderTxt();
            var pallets = reader.ReadFile(urlPallets);
            var jsonItemsDetails = reader.ReadFile(urlJsonItems);

            var converter = new CustomJsonSerializer();
            var itemsDetails = converter.Deserialize<IList<ItemDetails>>(jsonItemsDetails);

            var classRoom = new ClassRoomMaggie(itemsDetails);
            
            classRoom.Train(pallets.Split("\r\n"));
        }
    }
}
