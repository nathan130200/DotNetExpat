using System;
using System.Text;
using NUnit.Framework;

namespace Expat.Test
{
    public class Tests
    {
        protected ExpatParser parser;

        [OneTimeSetUp]
        public void Configure()
        {
            parser = new ExpatParser("utf-8");
            parser.SetElementHandler(OnStartElement, OnEndElement);
        }

        void OnStartElement(IntPtr _, string name, string[] attrs)
        {
            Console.WriteLine($"[OnStartElement] name={name};attrs={string.Join(",", attrs)}");
        }

        void OnEndElement(IntPtr _, string name)
        {
            Console.WriteLine($"[OnEndElement] name={name}");
        }

        [Test]
        public void RequireNotNull()
        {
            Assert.NotNull(parser);
        }

        [Test]
        public void MustBeFirstLine()
        {
            Assert.AreEqual(1, parser.CurrentLineNumber);
        }

        [Test]
        public void ParseElement()
        {
            var xml = "<foo><bar/></foo>";
            var buffer = Encoding.UTF8.GetBytes(xml);
            var status = parser.Parse(buffer, buffer.Length, true);
            Console.WriteLine("Bytes [Index/Count]: " + parser.ByteIndex + "/" + parser.ByteCount);
            Assert.AreEqual(status, ExpatStatus.Ok);
        }

        [Test]
        public void ParseUnclosedTagAsFinal()
        {
            var xml = "<foo xmlns='urn:bar'>";
            var buffer = Encoding.UTF8.GetBytes(xml);
            var status = parser.Parse(buffer, buffer.Length, true);

            if(status != ExpatStatus.Ok)
            {
                var error = parser.GetLastErrror();
                Console.WriteLine("Error: " + error);
                Console.WriteLine("Description: " + error.GetDescription());
            }

            Console.WriteLine("Bytes [Index/Count]: " + parser.ByteIndex + "/" + parser.ByteCount);
        }

        [Test]
        public void ParseUnclosedTagNotFinal()
        {
            var xml = "<foo xmlns='urn:bar'>";
            var buffer = Encoding.UTF8.GetBytes(xml);
            var status = parser.Parse(buffer, buffer.Length, false);

            if (status != ExpatStatus.Ok)
            {
                var error = parser.GetLastErrror();
                Console.WriteLine("Error: " + error);
                Console.WriteLine("Description: " + error.GetDescription());
            }

            Console.WriteLine("Bytes [Index/Count]: " + parser.ByteIndex + "/" + parser.ByteCount);
        }

        [Test]
        public void DisplayByteIndexAndCount()
        {
            Console.WriteLine($"Byte Index: {parser.ByteIndex}");
            Console.WriteLine($"Byte Index: {parser.ByteCount}");
        }

        [Test]
        public void CheckIfDisposed()
        {
            var newParser = new ExpatParser("ascii");

            var xml = "<foo xmlns='urn:bar' />";
            var buffer = Encoding.UTF8.GetBytes(xml);
            newParser.Parse(buffer, buffer.Length);

            Assert.AreNotEqual(IntPtr.Zero, newParser.GetPointer());
            Assert.IsFalse(newParser.IsDisposed);
            Console.WriteLine("Pointer Before Dispose: " + newParser.GetPointer());
            newParser.Dispose();

            try
            {
                Assert.AreEqual(IntPtr.Zero, newParser.GetPointer());
                Assert.IsTrue(newParser.IsDisposed);
                Console.WriteLine("Pointer After Dispose: " + newParser.GetPointer());

                newParser.Parse(buffer, buffer.Length); // This MUST thrown exception.
            }
            catch(Exception ex)
            {
                Assert.IsTrue(ex is ObjectDisposedException, $"Unknownu nhandled eception.\n{ex}");
            }
        }

        [Test]
        [TestCase("utf-8")]
        [TestCase("ascii")]
        public void ParserXmlThenResetParser(string encoding)
        {
            var xml = "<foo>";
            var buffer = Encoding.UTF8.GetBytes(xml);

            var objParser = new ExpatParser(encoding);
            objParser.Parse(buffer, buffer.Length, true);
            Assert.IsTrue(objParser.Reset());

            Console.WriteLine($"Byte Index: {parser.ByteIndex}");
            Console.WriteLine($"Byte Index: {parser.ByteCount}");

            Console.WriteLine("Lets reset the parser!");
            objParser.Parse(buffer, buffer.Length, true);

            Console.WriteLine($"Byte Index: {parser.ByteIndex}");
            Console.WriteLine($"Byte Index: {parser.ByteCount}");

            objParser.Dispose();
        }
    }
}