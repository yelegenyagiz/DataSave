using System;
using System.IO;
using UnityEngine;

public static class SaveLoad
{
    const string fileName = @"C:\Users\yagiz\Desktop\Database.bin";
    static char sep = ':';
    /// <summary>
    /// İstenilen türdeki veriyi bir key ile dosyaya yazdırır.Eğer varolan bir key ile kayıt yapılmaya çalışılırsa varolan key'e ait olan 
    /// değerin üzerine yazılır.
    /// </summary>
    /// <typeparam name="TValue">Yazdırılacak olan verinin türü.</typeparam>
    /// <param name="key">Yazdırılacak olan verinin anahtarı.</param>
    /// <param name="value">Yazdırılacak olan veri.</param>
    /// <param name="seperator">Eğer kaydedilen veri içinde : karakteri var ise yazdırma işlemi sırasında bu karakteri kullanmayın.Kullanmak zorunlu 
    /// ise default olarak ayarlanmış bu karakteri değiştirin.</param>

    public static void Write<TValue>(string key, TValue value, char seperator = ':')
    {
        sep = seperator;
        if (!File.Exists(fileName))
        {
            Debug.LogWarning("The file is not found!A new file has been created.");
            using (FileStream stream = File.Create(fileName))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine($"{key}{sep}{value}");
                    writer.Close();
                }
            }
            return;
        }

        using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                int lineIndex = 0;
                while (!reader.EndOfStream)
                {
                    lineIndex++;
                    var str = reader.ReadLine();
                    string _v = "";
                    for (int i = 0; i < str.Length; i++)
                    {
                        char c = str[i];
                        if (c == sep)
                        {
                            if (_v == key)
                            {
                                reader.Close();
                                string[] lines = File.ReadAllLines(fileName);
                                lines[lineIndex - 1] = $"{key}{sep}{value}";
                                File.WriteAllLines(fileName, lines);
                                reader.Close();
                                return;
                            }
                        }
                        _v += c;
                    }
                }
            }
        }

        using (FileStream stream = File.Open(fileName, FileMode.Append, FileAccess.Write))
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine($"{key}{sep}{value}");
                writer.Close();
            }
        }
    }

    /// <summary>
    /// Kaydedilen değerin anahtarı ile istenen değeri döndürür(string hariç).
    /// </summary>
    /// <typeparam name="TValue">Geri dönecek olan veri türü(Bu veri türü string olamaz).</typeparam>
    /// <param name="key">İstenen değerin anahtar değeri.</param>
    /// <returns>Geriye TValue türünde bir değer dönderir.</returns>
    public static TValue? Read<TValue>(string key) where TValue : struct
    {
        if (File.Exists(fileName))
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string v = "";
                        for (int i = 0; i < line.Length; i++)
                        {
                            char c = line[i];

                            if (c == sep)
                            {
                                if (v == key)
                                {
                                    Type type = typeof(TValue);
                                    if (type == typeof(Vector3) || type == typeof(Vector2))
                                    {
                                        var newS = line.Substring(i + 2, line.Length - (i + 3));
                                        string[] array = newS.Split(',');

                                        if (array.Length == 3)
                                        {
                                            Vector3 vect = new Vector3(
                                            float.Parse(array[0]),
                                            float.Parse(array[1]),
                                            float.Parse(array[2])

                                            );
                                            reader.Close();

                                            return (TValue)Convert.ChangeType(vect, type);
                                        }
                                        else if (array.Length == 2)
                                        {
                                            Vector2 vect = new Vector2(
                                            float.Parse(array[0]),
                                            float.Parse(array[1])

                                            );
                                            reader.Close();
                                            return (TValue)Convert.ChangeType(vect, type);
                                        }
                                    }
                                    else if (type == typeof(float))
                                    {
                                        var newS = line.Substring(i + 1, line.Length - (i + 1));
                                        reader.Close();

                                        return (TValue)Convert.ChangeType(newS, type);
                                    }
                                    else if (type == typeof(int))
                                    {
                                        var newS = line.Substring(i + 1, line.Length - (i + 1));
                                        reader.Close();

                                        return (TValue)Convert.ChangeType(newS, type);
                                    }
                                    else if (type == typeof(long))
                                    {
                                        var newS = line.Substring(i + 1, line.Length - (i + 1));
                                        reader.Close();

                                        return (TValue)Convert.ChangeType(newS, type);
                                    }
                                    else if (type == typeof(double))
                                    {
                                        var newS = line.Substring(i + 1, line.Length - (i + 1));
                                        reader.Close();

                                        return (TValue)Convert.ChangeType(newS, type);
                                    }
                                    else if (type == typeof(bool))
                                    {
                                        var newS = line.Substring(i + 1, line.Length - (i + 1));
                                        reader.Close();

                                        return (TValue)Convert.ChangeType(newS, type);
                                    }
                                    break;
                                }
                            }
                            v += c;
                        }
                    }
                }
            }

        }
        return null;
    }

    /// <summary>
    /// Geri dönecek olan değer string tipinde olduğu zaman kullanın.
    /// </summary>
    /// <param name="key">Dönecek olan verinin key'i.</param>
    /// <returns>System.String türünde veri dönderir.</returns>
    public static string Read(string key)
    {

        if (File.Exists(fileName))
        {
            using (FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string v = "";
                        for (int i = 0; i < line.Length; i++)
                        {
                            char c = line[i];

                            if (c == sep)
                            {
                                if (v == key)
                                {
                                    var newS = line.Substring(i + 1, line.Length - (i + 1));
                                    reader.Close();
                                    return newS;
                                }
                            }
                            v += c;
                        }
                    }
                }
            }

        }
        return null;
    }



    /// <summary>
    /// Silinmesinin istenen değerin key'ini parametre olarak veriğinizde değeri siler.
    /// </summary>
    /// <param name="key">Silinecek olan değerin key'i.</param>
    public static void DeleteValue(string key)
    {
        using (FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite))
        {
            using (StreamReader reader = new StreamReader(fs))
            {
                int lineIndex = 0;
                while (!reader.EndOfStream)
                {
                    lineIndex++;
                    var str = reader.ReadLine();
                    string _v = "";
                    for (int i = 0; i < str.Length; i++)
                    {
                        char c = str[i];
                        if (c == sep)
                        {
                            if (_v == key)
                            {
                                reader.Close();
                                string[] lines = File.ReadAllLines(fileName);
                                lines[lineIndex - 1] = "";
                                File.WriteAllLines(fileName, lines);
                                return;
                            }
                        }
                        _v += c;
                    }
                }
            }
        }

    }
    /// <summary>
    /// Veri dosyasını(eğer varsa) siler. Yoksa bir hata mesajı bastırır.
    /// </summary>
    public static void DeleteData()
    {
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        else
        {
            Debug.LogError("The file is not found!");
        }
    }

}
