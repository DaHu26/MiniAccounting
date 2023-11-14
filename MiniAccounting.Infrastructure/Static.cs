namespace MiniAccounting.Infrastructure
{
    public class Static
    {
        public static readonly Encoding Encoding = Encoding.UTF8;

        public static Guid TotalBalanceUserUid = new Guid(1,1,1,1,1,1,1,1,1,1,1);
        public static string ToStringPropsWithReflection(object obj)
        {
            var list = new List<string>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                list.Add($"{prop.Name}={prop.GetValue(obj)}");
            }
            return string.Join("; ", list);
        }

    }
}
