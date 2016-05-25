using System;
using System.IO;
using System.Text;

public class Files
{
    private string path;
    private string[] data;
	public Files(string nameFile)
	{
        this.path = nameFile;
        if (!File.Exists(nameFile))
        {
            FileInfo file = new FileInfo(nameFile);
            file.Create();
        }
	}

    public bool OpenFile()
    {
        if(File.Exists(path))
        {
            string line;
            int i = 0;
            FileInfo file = new FileInfo(path);
            StreamReader myStream = new StreamReader(path, Encoding.UTF8);
            while((line = myStream.ReadLine()) != null)
            {
                data[i] = line;
                i++;
            }
            myStream.Close();
            return true;
        }
        else
        {
            return false;
        }
    }

    public string[] GetData()
    {
        return data;
    }

    public void AddLink(string link)
    {
        if(data == null)
        {
            Array.Resize(ref data, 1);
        }
        else
        {
            Array.Resize(ref data, data.Length + 1);
        }
        int position = data.Length - 1;
        data[position] = link;
        addToFile(link);
    }

    private void addToFile(string link)
    {
        FileInfo myFile = new FileInfo(path);
        StreamWriter myStream = new StreamWriter(path, true, Encoding.UTF8);
        myStream.WriteLine(link + ";");
        myStream.Close();
    }
}
