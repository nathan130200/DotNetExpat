using System;
using System.Diagnostics;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Expat;
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
            Debug.WriteLine($"[OnStartElement] name={name};attrs={string.Join(",", attrs)}");
        }

        void OnEndElement(IntPtr _, string name)
        {
            Debug.WriteLine($"[OnEndElement] name={name}");
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
            Debug.WriteLine("Bytes [Index/Count]: " + parser.ByteIndex + "/" + parser.ByteCount);
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
                var description = Utilities.GetErrorDescription(error);
                Debug.WriteLine("Error: " + error);
                Debug.WriteLine("Description: " + description);
            }

            Debug.WriteLine("Bytes [Index/Count]: " + parser.ByteIndex + "/" + parser.ByteCount);
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
                var description = Utilities.GetErrorDescription(error);
                Debug.WriteLine("Error: " + error);
                Debug.WriteLine("Description: " + description);
            }

            Debug.WriteLine("Bytes [Index/Count]: " + parser.ByteIndex + "/" + parser.ByteCount);
        }

        [Test]
        public void DisplayByteIndexAndCount()
        {
            Debug.WriteLine($"Byte Index: {parser.ByteIndex}");
            Debug.WriteLine($"Byte Index: {parser.ByteCount}");
        }
    }
}