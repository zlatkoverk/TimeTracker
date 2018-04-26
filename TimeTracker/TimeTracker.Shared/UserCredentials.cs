using System;
using System.Collections.Generic;
using System.Text;

namespace TimeTracker.Shared
{
    public class UserCredentials
    {
        public string Username { get; set; }
        public int PasswordHash { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is UserCredentials))
            {
                return false;
            }

            UserCredentials other = (UserCredentials)obj;
            return Username == other.Username && PasswordHash == other.PasswordHash;
        }

        public override int GetHashCode()
        {
            return Username.GetHashCode() + PasswordHash.GetHashCode();
        }

        public static bool operator ==(UserCredentials c1, UserCredentials c2)
        {
            if (ReferenceEquals(c1, c2))
            {
                return true;
            }
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            {
                return false;
            }
            return c1.Equals(c2);
        }

        public static bool operator !=(UserCredentials c1, UserCredentials c2)
        {
            return !(c1 == c2);
        }
    }
}
