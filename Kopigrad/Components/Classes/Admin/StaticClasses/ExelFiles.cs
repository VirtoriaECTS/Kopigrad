
using System;
using System.Globalization;
using System.Linq;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Hosting;

namespace Kopigrad.Components.Classes.Admin.StaticClasses
{
    public class ExelFiles
    {

        public List<Data.DataStaticDohod> getListDohod()
        {
            var dataStaticDohods = new List<Data.DataStaticDohod>();

            // 1) Текущий рабочий каталог — это корень проекта, где лежит .csproj и папка wwwroot
            string projectRoot = Directory.GetCurrentDirectory();

            // 2) Строим абсолютный путь к папке wwwroot\Dohod
            string dohoddFolder = Path.Combine(projectRoot, "wwwroot", "Dohod");
            if (!Directory.Exists(dohoddFolder))
                throw new DirectoryNotFoundException($"Папка {dohoddFolder} не найдена.");

            // 3) Берём все .xlsx-файлы из wwwroot\Dohod
            var excelFiles = Directory.GetFiles(dohoddFolder, "*.xlsx");

            // Словарь для перевода названия месяца по-русски в его порядковый номер (1-12)
            var monthMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["Январь"] = 1,
                ["Февраль"] = 2,
                ["Март"] = 3,
                ["Апрель"] = 4,
                ["Май"] = 5,
                ["Июнь"] = 6,
                ["Июль"] = 7,
                ["Август"] = 8,
                ["Сентябрь"] = 9,
                ["Октябрь"] = 10,
                ["Ноябрь"] = 11,
                ["Декабрь"] = 12
            };

            // Временный список для сортировки по (год, месяц)
            var tempList = new List<(int year, int monthIndex, Data.DataStaticDohod data)>();

            foreach (var filePath in excelFiles)
            {
                // 4) Имя файла без расширения, например "Выручка Август 2024"
                var fileName = Path.GetFileNameWithoutExtension(filePath);

                // 5) Разбиваем по пробелам: ["Выручка", "<Месяц>", "<Год>"]
                var parts = fileName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3)
                {
                    // Если имя файла не соответствует шаблону "Выручка <Месяц> <Год>", пропускаем
                    Console.WriteLine($"Файл \"{fileName}\" имеет неожиданный формат. Ожидалось \"Выручка <Месяц> <Год>\".");
                    continue;
                }

                string monthRus = parts[1];
                if (!monthMap.TryGetValue(monthRus, out int monthIndex))
                {
                    // Если месяц не в словаре, пропускаем
                    Console.WriteLine($"Не удалось определить номер месяца \"{monthRus}\" в файле \"{fileName}\". Пропускаем.");
                    continue;
                }

                // Парсим год (parts[2]) в int
                if (!int.TryParse(parts[2], NumberStyles.None, CultureInfo.InvariantCulture, out int year))
                {
                    Console.WriteLine($"Не удалось распознать год \"{parts[2]}\" в файле \"{fileName}\". Пропускаем.");
                    continue;
                }

                // Собираем полное название месяца + год, чтобы передать в DataStaticDohod
                string monthNameForData = $"{monthRus} {year}";

                // 6) Открываем книгу Excel
                using var workbook = new XLWorkbook(filePath);

                // 7) Считываем все листы с именами "01","02",… и складываем sum = B12 + B13
                var dailyValues = new List<double>();
                foreach (var ws in workbook.Worksheets)
                {
                    var sheetName = ws.Name.Trim();
                    if (sheetName.Length == 2 && int.TryParse(sheetName, out _))
                    {
                        double valB12 = TryGetDoubleCellValue(ws.Cell("B12"));
                        double valB13 = TryGetDoubleCellValue(ws.Cell("B13"));
                        dailyValues.Add(valB12 + valB13);
                    }
                }

                // Создаём объект DataStaticDohod и кладём в временный список вместе с ключами сортировки
                var dataStatic = new Data.DataStaticDohod(monthNameForData, dailyValues);
                tempList.Add((year, monthIndex, dataStatic));
            }

            // 8) Сортируем tempList сначала по году (возрастание), затем по месяцу (от 1 до 12)
            var ordered = tempList
                .OrderBy(t => t.year)
                .ThenBy(t => t.monthIndex)
                .ToList();

            // 9) Переносим уже отсортированные DataStaticDohod в результирующий список
            foreach (var item in ordered)
            {
                dataStaticDohods.Add(item.data);
            }

            return dataStaticDohods;
        }

        public List<Data.DataCountPrint> getListCountAll ()
        {
            var dataStaticDohods = new List<Data.DataCountPrint>();

            // 1) Текущий рабочий каталог — это корень проекта, где лежит .csproj и папка wwwroot
            string projectRoot = Directory.GetCurrentDirectory();

            // 2) Строим абсолютный путь к папке wwwroot\Dohod
            string dohoddFolder = Path.Combine(projectRoot, "wwwroot", "Dohod");
            if (!Directory.Exists(dohoddFolder))
                throw new DirectoryNotFoundException($"Папка {dohoddFolder} не найдена.");

            // 3) Берём все .xlsx-файлы из wwwroot\Dohod
            var excelFiles = Directory.GetFiles(dohoddFolder, "*.xlsx");

            // Словарь для перевода названия месяца по-русски в его порядковый номер (1-12)
            var monthMap = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
            {
                ["Январь"] = 1,
                ["Февраль"] = 2,
                ["Март"] = 3,
                ["Апрель"] = 4,
                ["Май"] = 5,
                ["Июнь"] = 6,
                ["Июль"] = 7,
                ["Август"] = 8,
                ["Сентябрь"] = 9,
                ["Октябрь"] = 10,
                ["Ноябрь"] = 11,
                ["Декабрь"] = 12
            };

            // Временный список для сортировки по (год, месяц)
            var tempList = new List<(int year, int monthIndex, Data.DataCountPrint data)>();

            foreach (var filePath in excelFiles)
            {
                // 4) Имя файла без расширения, например "Выручка Август 2024"
                var fileName = Path.GetFileNameWithoutExtension(filePath);

                // 5) Разбиваем по пробелам: ["Выручка", "<Месяц>", "<Год>"]
                var parts = fileName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length < 3)
                {
                    // Если имя файла не соответствует шаблону "Выручка <Месяц> <Год>", пропускаем
                    Console.WriteLine($"Файл \"{fileName}\" имеет неожиданный формат. Ожидалось \"Выручка <Месяц> <Год>\".");
                    continue;
                }

                string monthRus = parts[1];
                if (!monthMap.TryGetValue(monthRus, out int monthIndex))
                {
                    // Если месяц не в словаре, пропускаем
                    Console.WriteLine($"Не удалось определить номер месяца \"{monthRus}\" в файле \"{fileName}\". Пропускаем.");
                    continue;
                }

                // Парсим год (parts[2]) в int
                if (!int.TryParse(parts[2], NumberStyles.None, CultureInfo.InvariantCulture, out int year))
                {
                    Console.WriteLine($"Не удалось распознать год \"{parts[2]}\" в файле \"{fileName}\". Пропускаем.");
                    continue;
                }

                // Собираем полное название месяца + год, чтобы передать в DataStaticDohod
                string monthNameForData = $"{monthRus} {year}";

                // 6) Открываем книгу Excel
                using var workbook = new XLWorkbook(filePath);

                // 7) Считываем все листы с именами "01","02",… и складываем sum = B12 + B13
                var kyocera = new List<double>();
                var xerox = new List<double>();
                var canon = new List<double>();
                var canontm = new List<double>();
                var riso = new List<double>();
                foreach (var ws in workbook.Worksheets)
                {
                    var sheetName = ws.Name.Trim();
                    if (sheetName.Length == 2 && int.TryParse(sheetName, out _))
                    {
                        double valD7 = TryGetDoubleCellValue(ws.Cell("D7"));
                        double valE7 = TryGetDoubleCellValue(ws.Cell("E7"));
                        if(valD7 + valE7 < 0) kyocera.Add(0);
                        else
                        {
                            kyocera.Add(valD7 + valE7);
                        }
                        

                        double valF = TryGetDoubleCellValue(ws.Cell("F7"));
                        double valG = TryGetDoubleCellValue(ws.Cell("G7"));
                        if (valF + valG < 0) xerox.Add(0);
                        else
                        {
                            xerox.Add(valF + valG);
                        }

                        double valH = TryGetDoubleCellValue(ws.Cell("H7"));
                        double valI = TryGetDoubleCellValue(ws.Cell("I7"));


                        if (valH + valI < 0) canon.Add(0);
                        else
                        {
                            canon.Add(valH + valI);
                        }

                        double valJ = TryGetDoubleCellValue(ws.Cell("J7"));
                        double valK = TryGetDoubleCellValue(ws.Cell("K7"));


                        if (valJ + valK < 0) canontm.Add(0);
                        else
                        {
                            canontm.Add(valJ + valK);
                        }

                        double valL = TryGetDoubleCellValue(ws.Cell("L7"));
                        double valM = TryGetDoubleCellValue(ws.Cell("M7"));

                        if (valL + valM < 0) riso.Add(0);
                        else
                        {
                            riso.Add(valL + valM);
                        }

                    }
                }

                // Создаём объект DataStaticDohod и кладём в временный список вместе с ключами сортировки
                var dataStatic = new Data.DataCountPrint(monthNameForData, kyocera.ToArray(), xerox.ToArray(), canon.ToArray(), canontm.ToArray(), riso.ToArray());
                tempList.Add((year, monthIndex, dataStatic));
            }

            // 8) Сортируем tempList сначала по году (возрастание), затем по месяцу (от 1 до 12)
            var ordered = tempList
                .OrderBy(t => t.year)
                .ThenBy(t => t.monthIndex)
                .ToList();

            // 9) Переносим уже отсортированные DataStaticDohod в результирующий список
            foreach (var item in ordered)
            {
                dataStaticDohods.Add(item.data);
            }

            return dataStaticDohods;
        }



        private static double TryGetDoubleCellValue(IXLCell cell)
        {
            if (cell == null || cell.IsEmpty())
                return 0;

            try
            {
                // Если ячейка действительно содержит число (или формулу, вычисляющую число)
                return cell.GetDouble();
            }
            catch
            {
                // Иначе пробуем распарсить строку
                var str = cell.GetString();
                if (double.TryParse(str, NumberStyles.Any, CultureInfo.CurrentCulture, out var parsed))
                    return parsed;

                return 0;
            }
        }
    }
}
