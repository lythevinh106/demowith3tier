using System.Text;

namespace demoBlazor.Helpers
{
    public class StringHelper
    {
        public static string ConvertObjectToSearchParam(object _object)
        {
            if (_object != null)
            {
                StringBuilder searchParam = new StringBuilder();

                Type type = _object.GetType();

                for (int i = 0; i < type.GetProperties().Length - 1; i++)
                {
                    string propertyName = type.GetProperties()[i].Name;
                    object propertyValue = type.GetProperties()[i].GetValue(_object);


                    if (propertyValue != null)
                    {
                        if (i == 0 || searchParam.Length == 0)
                        {
                            searchParam.Append($"{propertyName}={propertyValue}");


                        }
                        else
                        {
                            searchParam.Append($"&{propertyName}={propertyValue}");
                        }
                    }

                }

                return searchParam.ToString();
            }


            return "";
        }
    }
}