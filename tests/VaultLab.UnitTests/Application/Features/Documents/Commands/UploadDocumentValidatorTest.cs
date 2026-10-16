using VaultLab.Application.Features.Documents.Commands.UploadDocument;

namespace VaultLab.UnitTests.Application.Features.Documents.Commands
{
    public sealed class UploadDocumentValidatorTest
    {
        private UploadDocumentValidator _validator = new();

        public static UploadDocumentCommand CreateValidCommand()
        {
            return new UploadDocumentCommand(
                Guid.NewGuid(),
                "test.pdf",
                "application/pdf",
                1024,
                new MemoryStream([1, 2, 3])
            );
        }

        [Fact]
        public void Validate_WhenUserIdIsEmpty_ReturnsValidationError()
        {
            //Arrange
            var command = CreateValidCommand() with
            {
                UserId = Guid.Empty
            };

            //Act
            var result = _validator.Validate(command);

            //Assert
            Assert.Contains(
                result.Errors,
                error => error.PropertyName == nameof(command.UserId)
            );
        }

        [Fact]
        public void Validate_WhenFileSizeIsZero_ReturnsValidationError()
        {
            //Arrange
            var command = CreateValidCommand() with
            {
                FileSize = 0
            };

            //Act
            var result = _validator.Validate(command);

            //Assert
            Assert.Contains(
                result.Errors,
                error => error.PropertyName == nameof(command.FileSize)
            );
        }


        [Fact]
        public void Validate_WhenFileNameIsEmpty_ReturnsValidationError()
        {
            // Arrange
            var command = CreateValidCommand() with
            {
                FileName = string.Empty
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.Contains(
                result.Errors,
                error => error.PropertyName == nameof(command.FileName)
            );
        }

        [Fact]
        public void Validate_WhenFileNameExceeds255Characters_ReturnsValidationError()
        {
            // Arrange
            var command = CreateValidCommand() with
            {
                FileName = new string('a', 256)
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.Contains(
                result.Errors,
                error => error.PropertyName == nameof(command.FileName)
            );
        }


        [Fact]
        public void Validate_WhenContentTypeIsEmpty_ReturnsValidationError()
        {
            // Arrange
            var command = CreateValidCommand() with
            {
                ContentType = string.Empty
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.Contains(
                result.Errors,
                error => error.PropertyName == nameof(command.ContentType)
            );
        }

        [Fact]
        public void Validate_WhenContentIsNull_ReturnsValidationError()
        {
            // Arrange
            var command = CreateValidCommand() with
            {
                Content = null!
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.Contains(
                result.Errors,
                error => error.PropertyName == nameof(command.Content)
            );
        }
    }
}