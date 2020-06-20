using Expat;
using NUnit.Framework;

namespace DotNetExpat.Bindings.Test
{
    public class Tests
    {
        protected ExpatParser parser;

        [OneTimeSetUp]
        public void Configure()
        {
            parser = new ExpatParser("utf-8");
        }

        [Test]
        public void AssertParserNotNull()
        {
            Assert.NotNull(parser);
        }
    }
}