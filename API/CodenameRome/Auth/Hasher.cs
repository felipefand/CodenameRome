using BC = BCrypt.Net.BCrypt;

namespace CodenameRome.Auth
{
    public class Hasher
    {
        public string passwordTextToHash(string password)
        {
            return BC.HashPassword(password);
        }

        public bool verifyPassword(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
