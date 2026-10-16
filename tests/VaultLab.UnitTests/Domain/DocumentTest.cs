using VaultLab.Domain.Enums;
using VaultLab.Domain.Entities;

namespace VaultLab.UnitTests.Domain
{
    public sealed class DocumentTest
    {
        [Fact]
        public void Constructor_ShouldSetStatusToUploaded()
        {
            //Arrange and act
            var document = new Document(
                Guid.NewGuid(),
                "test.pdf",
                "application/pdf",
                1024,
                "/uploads/test.pdf"
            );

            //Assert
            Assert.Equal(DocumentStatus.Uploaded, document.Status);
        }

        [Fact]
        public void MarkAsProcessing_WhenStatusIsUploaded_SetsStatusToProcessing()
        {
            //Arrange
            var document = new Document(
                Guid.NewGuid(),
                "test.pdf",
                "application/pdf",
                1024,
                "/uploads/test.pdf"
            );

            //Act
            document.MarkAsProcessing();

            //Assert
            Assert.Equal(DocumentStatus.Processing, document.Status);
        }

        [Fact]
        public void MarkAsProcessed_WhenStatusIsProcessing_SetsStatusToProcessed()
        {
            //Arrange
            var document = new Document(
                Guid.NewGuid(),
                "test.pdf",
                "application/pdf",
                1024,
                "/uploads/test.pdf"
            );


            //Act
            document.MarkAsProcessing();
            document.MarkAsProcessed();

            //Assert
            Assert.Equal(DocumentStatus.Processed, document.Status);
        }

        [Fact]
        public void MarkAsProcessing_WhenStatusIsProcessed_ThrowsInvalidOperationException()
        {
            //Arrange
            var document = new Document(
                Guid.NewGuid(),
                "test.pdf",
                "application/pdf",
                1024,
                "/uploads/test.pdf"
            );

            //Act
            document.MarkAsProcessing();
            document.MarkAsProcessed();

            //Assert
            Assert.Throws<InvalidOperationException>(() => document.MarkAsProcessing());
        }


        [Fact]
        public void MarkAsProcessed_WhenStatusIsUploaded_ThrowsInvalidOperationException()
        {
            // Arrange
            var document = new Document(
                Guid.NewGuid(),
                "test.pdf",
                "application/pdf",
                1024,
                "/uploads/test.pdf"
            );

            // Act & Assert
            Assert.Throws<InvalidOperationException>(
                () => document.MarkAsProcessed()
            );
        }


        [Fact]
        public void MarkAsFailed_WhenProcessingFails_SetsStatusToFailed()
        {
            // Arrange
            var document = new Document(
                Guid.NewGuid(),
                "test.pdf",
                "application/pdf",
                1024,
                "/uploads/test.pdf"
            );

            document.MarkAsProcessing();

            // Act
            document.MarkAsFailed();

            // Assert
            Assert.Equal(DocumentStatus.Failed, document.Status);
        }


        [Fact]
        public void MarkAsProcessing_WhenStatusIsFailed_ThrowsInvalidOperationException()
        {
            // Arrange
            var document = new Document(
                Guid.NewGuid(),
                "test.pdf",
                "application/pdf",
                1024,
                "/uploads/test.pdf"
            );

            document.MarkAsProcessing();
            document.MarkAsFailed();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(
                () => document.MarkAsProcessing()
            );
        }


        [Fact]
        public void Constructor_WhenUserIdIsEmpty_ThrowsArgumentException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentException>(() =>
                new Document(
                    Guid.Empty,
                    "test.pdf",
                    "application/pdf",
                    1024,
                    "/uploads/test.pdf"
                )
            );
        }
    }
}