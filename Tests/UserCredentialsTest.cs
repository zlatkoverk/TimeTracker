using Microsoft.VisualStudio.TestTools.UnitTesting;
using TimeTracker.Shared.Model;

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

            var cred = new LoginModel(username, password);

            Assert.IsTrue(cred.Equals(new LoginModel(username, password)));
            Assert.IsFalse(cred.Equals(new LoginModel(anotherUsername, password)));
            Assert.IsFalse(cred.Equals(new LoginModel(username, anotherPassword)));
        }

        [TestMethod]
        public void Operators_test()
        {
            var username = "user123";
            var anotherUsername = username + "k";
            var password = "afij94sf1sf";
            var anotherPassword = password + "l";

            var cred = new LoginModel(username, password);

            Assert.IsTrue(cred == new LoginModel(username, password));
            Assert.IsFalse(cred == new LoginModel(anotherUsername, password));
            Assert.IsFalse(cred == new LoginModel(username, anotherPassword));
        }
    }
}
