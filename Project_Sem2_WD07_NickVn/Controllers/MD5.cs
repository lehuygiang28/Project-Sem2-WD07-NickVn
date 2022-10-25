using System.Text;

public class MD5
{
    public static string CreateMD5(string input)
    {
        using (var provider = System.Security.Cryptography.MD5.Create())
        {
            StringBuilder builder = new StringBuilder();

            foreach (byte b in provider.ComputeHash(Encoding.UTF8.GetBytes(input)))
                builder.Append(b.ToString("x3").ToLower());

            return builder.ToString();
        }
    }
}