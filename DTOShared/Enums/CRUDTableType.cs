using DTOShared.Contracts;

namespace DTOShared.Enums
{
    public class CRUDTableTypes
    {
        public static readonly List<string> AllTypes;

        static CRUDTableTypes()
        {

            var modelNames = typeof(ICRUDTable).Assembly.GetTypes()
                .Where(x => typeof(ICRUDTable).IsAssignableFrom(x))
                .Where(x => !x.IsInterface).ToList();


            AllTypes = modelNames.Select(model => model.Name).ToList();
        }
    }
}
