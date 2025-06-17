using TaskTracker.Utilities;
using Xunit;

namespace UnitTests
{
    public class CommandParserTests
    {
        #region GetArgsForAddCommand
        [Theory]
        [InlineData("add \"My Task\"", "My Task")]
        [InlineData("add \"My Task 123\"", "My Task 123")]
        [InlineData("add \"My Task\"  ", "My Task")]
        [InlineData("add \"My Task\"\t", "My Task")]
        public void GetArgsForAddCommand_IsCorrectFormat_ReturnsTrue(string command, string expectedTaskName)
        {
            // Arrange

            // Act
            var result = CommandParser.GetArgsForAddCommand(command);

            // Assert
            Assert.Equal(expectedTaskName, result);
        }

        [Theory]
        [InlineData("add")]
        [InlineData("add \"\"")]
        [InlineData("add My Task")]
        [InlineData("add \"My Task")]
        [InlineData("add My Task\"")]
        [InlineData("add \"My Task\" \"and another\"")]
        [InlineData("add \"My Task\" extra")]
        //[InlineData("add    \"My Task\"")]
        public void GetArgsForAddCommand_IsIncorrectFormat_ThrowsFormatException(string command)
        {
            // Arrange

            // Act Assert
            var ex = Assert.Throws<FormatException>(() => CommandParser.GetArgsForAddCommand(command));

            string expectedMessage = $"Invalid command format for 'add' command.";
            Assert.Equal(expectedMessage, ex.Message);
        }
        #endregion

        #region GetArgsForUpdateCommand
        [Theory]
        [InlineData("update 1 \"My Task\"", 1, "My Task")]
        [InlineData("update 123 \"My Task 123\"", 123, "My Task 123")]
        [InlineData("update 1 \"My Task\"   ", 1, "My Task")]
        [InlineData("update 1 \"My Task\"\t", 1, "My Task")]
        public void GetArgsForUpdateCommand_IsCorrectFormat_ReturnsTrue(string command, int expectedId, string expectedTaskName)
        {
            // Arrange

            // Act
            var result = CommandParser.GetArgsForUpdateCommand(command);

            // Assert
            Assert.Equal(expectedId, result.Item1);
            Assert.Equal(expectedTaskName, result.Item2);
        }

        [Theory]
        [InlineData("update \'1 My Task\'")]
        [InlineData("update 1 My Task")]
        [InlineData("update \"My Task\" 1")]
        [InlineData("update 1 \"My Task")]
        [InlineData("update 1 My Task\"")]
        [InlineData("update 1.0 \"My Task\"")]
        [InlineData("update 1 \"\"")]
        [InlineData("update 1 \"My Task\" extra")]
        [InlineData("update 1 \"My Task\" \"And another\"")]
        //[InlineData("update 1    \"My Task\"")] // todo: check if this works
        public void GetArgsForUpdateCommand_IsIncorrectFormat_ThrowsFormatException(string command)
        {
            // Arrange

            // Act Assert
            var ex = Assert.Throws<FormatException>(() => CommandParser.GetArgsForUpdateCommand(command));

            string expectedMessage = $"Invalid command format for 'update' command.";
            Assert.Equal(expectedMessage, ex.Message);
        }

        #endregion

        #region GetArgsForCommandWithId
        [Theory]
        [InlineData("delete 1", "delete", 1)]
        [InlineData("delete 123", "delete", 123)]
        [InlineData("delete 1\t", "delete", 1)] // todo: is this ok
        [InlineData("delete 1   ", "delete", 1)] // todo: is this ok
        //[InlineData("DELETE 1", "delete", 1)] // todo consider case insensitivity
        [InlineData("mark-in-progress 1", "mark-in-progress", 1)]
        [InlineData("mark-done 1", "mark-done", 1)]
        [InlineData("fake-command 987", "fake-command", 987)]
        public void GetArgsForCommandWithId_IsCorrectFormat_ReturnsTrue(string command, string commandVerb, int expectedId)
        {
            // Arrange
            var pattern = $@"^{commandVerb}\s+\d+$";

            // Act
            int result = CommandParser.GetArgsForCommandWithId(command, commandVerb);

            // Assert
            Assert.Equal(expectedId, result);
        }

        [Theory]
        [InlineData("delete 1", "update")]
        //[InlineData("delete    1", "update")]
        [InlineData("delete 1.0", "delete")]
        [InlineData("delete \"1\"", "delete")]
        [InlineData("delete \"one\"", "delete")]
        [InlineData("delete 1 \"My Task\"", "delete")]
        public void GetArgsForCommandWithId_IsIncorrectFormat_ThrowsFormatException(string command, string commandVerb)
        {
            // Arrange
            var pattern = $@"^{commandVerb}\s+\d+$";

            // Act Assert
            var ex = Assert.Throws<FormatException>(() => CommandParser.GetArgsForCommandWithId(command, commandVerb));

            string expectedMessage = $"Invalid command format for '{commandVerb}' command.";
            Assert.Equal(expectedMessage, ex.Message);
        }

        #endregion
    }
}