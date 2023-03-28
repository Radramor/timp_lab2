using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace timp_lab2.Domain;

public static class DeserializeMenuData
{
    internal static List<List<object>> ParseData(string filePath)
    {
        // Считывание данных из файла
        var lines = File.ReadAllLines(filePath);
        
        // Разбирает каждую строку и добавляет её в List
        var dataList = (from line in lines
            select Regex.Match(line, @"(\d+)\s+(.+)\s+(\d+)\s*(.*)")
            into match
            where match.Success
            let num1 = int.Parse(match.Groups[1].Value)
            let str1 = match.Groups[2].Value
            let num2 = int.Parse(match.Groups[3].Value)
            let str2 = match.Groups[4].Value
            select new List<object> { num1, str1, num2, str2 }).ToList();
        
        if (dataList.Count != lines.Length)
            throw new DataException($"{lines.Length-dataList.Count} lines of data are corrupted");
        
        return dataList;
    }
}