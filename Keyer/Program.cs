using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Spire.Pdf.Texts;
using Spire.Xls;

namespace Keyer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args) {
            var guardians = new List<string> { "Doc", "Jouki", "ILeonx", "Preston", "Thomas", "Masusder", "Grimes", "MakinDay", "ravendoom", "Extra #1", "Extra #2", "Extra #3", "Extra #4" }; //12
            var extraCodes = 4;
            var adminsCount = guardians.Count - extraCodes;
            var result = string.Empty;
            string clipboard = Clipboard.GetText();
            var codeList = new List<string>();

            int i = 0;
            Console.WriteLine("Who's the starting Admin getting the second code?");
            for(i  = 0; i < adminsCount; i++)
            {
                Console.WriteLine(String.Format("\t{0}. {1}", i+1, guardians[i]));
            }

            var startingAdminIndex = (int)char.GetNumericValue((char) Console.Read()) - 1;

            if(clipboard.Equals(string.Empty))
            {
                clipboard = ((string[]) Clipboard.GetDataObject().GetData(DataFormats.FileDrop))[0];
                var fi = new FileInfo((string) clipboard);
                switch(fi.Extension)
                {
                    case ".csv":
                        codeList = new List<string>(File.ReadAllText(clipboard).Split('\n').Where(x => !x.Equals(string.Empty)));
                        break;
                    case ".xlsx":
                        var excelFile = new Workbook();
                        excelFile.LoadFromFile(clipboard);
                        var sheet = excelFile.Worksheets[0];
                        codeList = sheet.Rows.Select(x => x.Text).ToList();
                        break;
                }
            }
            else
            { // plain text
                codeList = new List<string>(clipboard.Split(new string[] { "\r\n" }, StringSplitOptions.None).Where(x => !x.Equals(string.Empty)));
            }
            

            result += "```" + "\n";
            var writtenName = string.Empty;
            for (i = 0; i < codeList.Count && i < guardians.Count; i++) {
                if(i < adminsCount)
                {
                    writtenName = guardians[i];
                }
                else
                {
                    writtenName = guardians[(startingAdminIndex + i % adminsCount) % adminsCount];
                }
                result += codeList[i] + " - " + writtenName + "\n";
                if(i == adminsCount - 1)
                {
                    result += "\n";
                }
                
            }
            result += "```";

            Clipboard.SetText(result);
        }
    }
}
