using System;
using System.IO;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Dwolla.Client.Tests
{
	public class DateOnlyConverterShould 
	{
		[Fact]
		public void ReadValidInputSuccess()
		{
			DateTime expected = new(2019, 01, 02);
			var input = "\"2019-01-02\"";
			DateOnlyConverter dateOnlyConverter = new();
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			Utf8JsonReader reader = new(bytes.AsSpan());
			reader.Read();
			JsonSerializerOptions options = new JsonSerializerOptions();

			var result = dateOnlyConverter .Read(ref reader, typeof(DateTime), options);

			Assert.Equal(expected, result);
		}

		[Fact]
		public void ReadWriteValidInputSuccess()
		{
			DateOnlyConverter dateOnlyConverter = new();
			JsonSerializerOptions jsonSerializerOptions = new();
			MemoryStream memoryStream = new();
			Utf8JsonWriter writer = new(memoryStream, default);
			DateTime dateTime = new(2022, 2, 2, 10, 30, 25);

			dateOnlyConverter.Write(writer, dateTime, jsonSerializerOptions);
			writer.Flush();
			Utf8JsonReader reader = new Utf8JsonReader(memoryStream.ToArray(), default);
			reader.Read();
			var result = dateOnlyConverter.Read(ref reader, typeof(DateTime), jsonSerializerOptions);

			Assert.Equal(new DateTime(2022, 2, 2, 0, 0, 0), result);
		}
	}
}
