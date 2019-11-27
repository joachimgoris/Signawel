using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Signawel.Api.Tests.Builders
{
    public class FormFileBuilder
    {

        private Mock<IFormFile> _fileMock;

        public FormFileBuilder()
        {
            _fileMock = new Mock<IFormFile>();
        }

        public FormFileBuilder WithContent(string content)
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;

            _fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            _fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return this;
        }

        public FormFileBuilder Empty()
        {
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Flush();
            ms.Position = 0;

            _fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            _fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return this;
        }

        public FormFileBuilder WithFileName(string fileName)
        {
            _fileMock.Setup(_ => _.FileName).Returns(fileName);
            return this;
        }

        public IFormFile Build()
        {
            return _fileMock.Object;
        }

        public Mock<IFormFile> BuildMock()
        {
            return _fileMock;
        }

    }
}
