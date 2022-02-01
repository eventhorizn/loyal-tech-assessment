using System.Reflection;
using System.Text.Json;
using ICSharpCode.SharpZipLib.GZip;
using review_api.Models;

namespace review_api.Business;

public class ReviewFileReader : IReviewFileReader
{
    private readonly string _filePath = "";

    public ReviewFileReader()
    {
        _filePath = System.IO.Path.GetDirectoryName(
            Assembly.GetEntryAssembly()?.Location) + "\\Data\\reviews_Tools_and_Home_Improvement_5.json.gz";
    }

    public IList<ReviewData> ReadFile()
    {
        var items = new List<ReviewData>();

        using (Stream fs = 
            new FileStream(_filePath, FileMode.Open, FileAccess.Read))
        using (GZipInputStream gzis = new(fs))
        using (StreamReader sr = new(gzis))
        {
            while (sr.Peek() > 0)
            {
                var line = sr.ReadLine();
                if (line is null)
                    continue;
                
                var t = JsonSerializer.Deserialize<ReviewData>(line);
                if (t is null)
                    continue;

                items.Add(t);
            }
        }

        return items;
    }
}