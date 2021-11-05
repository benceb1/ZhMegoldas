using System;
using System.Reflection;
using System.Text;

namespace FuncLibrary
{
    public class ObjectExaminer
    {
        public string ExtractProperties(object obj, bool filter)
        {
            StringBuilder sb = new StringBuilder();
            PropertyInfo[] properties = obj.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in properties)
            {
                if (filter && propertyInfo.GetCustomAttribute<ToBeFilteredAttribute>() == null)
                {
                    continue;
                }
                
                string propertyName = propertyInfo.Name;
                var value = propertyInfo.GetValue(obj);
                sb.Append($"{propertyName}:{value} ");
            }
            return sb.ToString();
        }
    }
}
