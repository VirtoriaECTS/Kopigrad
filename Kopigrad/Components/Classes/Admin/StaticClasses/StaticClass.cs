using Kopigrad.Components.Classes.Data;
using MudBlazor;
using System.Linq;

namespace Kopigrad.Components.Classes.Admin.StaticClasses
{
    public class StaticClass
    {


        public  List<Classes.Data.DataStatic> getStaticRequstOnline(DateRange dateRange)
        {
            List<Classes.Data.DataStatic> dataStatics = new List<Classes.Data.DataStatic>();

            using(var context = new Models.KopigradContext())
            {

                List<Classes.Data.DataStatic> all = new List<Classes.Data.DataStatic>();

                var requsts = context.Orders.ToList();


                foreach (var item in requsts)
                {
                    DateTime date = item.DataOrder;

                    Classes.Data.DataStatic data = new Data.DataStatic(1,Convert.ToDouble(item.Price), date);
                    all.Add(data);

                }

                if(dateRange == null)
                {
                    dataStatics = all
                        // Группируем по полной дате (без учёта времени):
                        .GroupBy(x => x.XAxisLabels.Date)
                        .Select(g =>
                        {
                            // g.Key — это уже DateTime с нужной датой (в 00:00:00)
                            DateTime dayDate = g.Key;

                            // Суммируем поле data внутри этой группы
                            int sumData = g.Sum(item => item.count);

                            // Суммируем поле data внутри этой группы
                            double sumPrice = g.Sum(item => item.data);

                            // Возвращаем DataStatic с суммой и самим днём:
                            return new DataStatic(sumData, sumPrice, dayDate);
                        })
                        .ToList();

                    return dataStatics;
                }


               
                else
                {
                    DateTime start = dateRange.Start.Value.Date;
                    DateTime end = dateRange.End.Value.Date;

                    dataStatics = all
                        // Сначала фильтруем по выбранному диапазону:
                        .Where(x =>
                            x.XAxisLabels.Date >= start
                         && x.XAxisLabels.Date <= end
                        )
                        // Теперь группируем уже отфильтрованные записи по дате:
                        .GroupBy(x => x.XAxisLabels.Date)
                        .Select(g =>
                        {
                            DateTime dayDate = g.Key;

                            // g.Sum(item => item.count) даёт число заявок в этот день
                            int sumData = g.Sum(item => item.count);

                            // g.Sum(item => item.data) даёт сумму цен в этот день
                            double sumPrice = g.Sum(item => item.data);

                            // Возвращаем объект, который хранит и количество, и сумму, и дату
                            return new DataStatic(sumData, sumPrice, dayDate);
                        })
                        .ToList();

                    return dataStatics;
                }
            }

           
        }
    }
}
