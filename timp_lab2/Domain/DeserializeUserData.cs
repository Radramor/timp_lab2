using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace timp_lab2.Domain;

public class DeserializeUsersData
{
    internal static List<List<object>> ParseData(string filePath)
    {
        // Считывание данных из файла
        var lines = File.ReadAllLines(filePath);
        
        // Разбирает каждую строку и добавляет её в List
        var dataList = (from line in lines
            select Regex.Match(line, @"^(\S+)\s+(\S+)\s+(\S+)$")
            into match
            where match.Success
            let login = match.Groups[1].Value
            let password = match.Groups[2].Value
            let fileMenuName = match.Groups[3].Value
            select new List<object> { login, password, fileMenuName }).ToList();
        
        if (dataList.Count != lines.Length)
            throw new DataException($"{lines.Length-dataList.Count} lines of data are corrupted");
        
        return dataList;
    }
}