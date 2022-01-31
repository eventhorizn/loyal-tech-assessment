using System.Text.Json;
using ICSharpCode.SharpZipLib.GZip;

public class ReviewFileReader : IReviewFileReader
{
    private string _filePath = "";

    public ReviewFileReader()
    {
        _filePath = Environment.CurrentDirectory + "\\Data\\reviews_Tools_and_Home_Improvement_5.json.gz";
    }

    public IList<ReviewData> ReadFile()
    {
        var items = new List<ReviewData>();

        using (Stream fs = 
            new FileStream(_filePath, FileMode.Open, FileAccess.Read))
        using (GZipInputStream gzis = new GZipInputStream(fs))
        using (StreamReader sr = new StreamReader(gzis))
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