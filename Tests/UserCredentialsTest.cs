using TimeTracker.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class UserCredentialsTest
    {
        [TestMethod]
        public void Equals_test()
        {
            var username = "user123";
            var anotherUsername = username + "k";
            var password = "pass123";
            var anotherPassword = password + "l";

            var cred = new UserCredentials(username, password);

            Assert.IsTrue(cred.Equals(new UserCredentials(username, password)));
            Assert.IsFalse(cred.Equals(new UserCredentials(anotherUsername, password)));
            Assert.IsFalse(cred.Equals(new UserCredentials(username, anotherPassword)));
        }

        [TestMethod]
        public void Operators_test()
        {
            var username = "user123";
            var anotherUsername = username + "k";
            var password = "afij94sf1sf";
            var anotherPassword = password + "l";

            var cred = new UserCredentials(username, password);

            Assert.IsTrue(cred == new UserCredentials(username, password));
            Assert.IsFalse(cred == new UserCredentials(anotherUsername, password));
            Assert.IsFalse(cred == new UserCredentials(username, anotherPassword));
        }
    }
}
