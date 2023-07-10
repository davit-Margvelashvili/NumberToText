using FluentAssertions;
using static NumberToText.Core.NumberToTextConverter;

namespace NumberToText.Core.Tests;

public class NumberToTextTests
{
    [Fact]
    public void Overflow()
    {
        var action = () => Convert(1000000000000);

        action.Should().ThrowExactly<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void DecimalNumbers()
    {
        var (whole, @decimal) = Convert(1234.37);
        whole.Should().Be("ერთი ათას ორას ოცდათოთხმეტი");
        @decimal.Should().Be("ოცდაჩვიდმეტი");
    }

    [Theory]
    [InlineData(0, "ნული")]
    [InlineData(1, "ერთი")]
    [InlineData(2, "ორი")]
    [InlineData(3, "სამი")]
    [InlineData(4, "ოთხი")]
    [InlineData(5, "ხუთი")]
    [InlineData(6, "ექვსი")]
    [InlineData(7, "შვიდი")]
    [InlineData(8, "რვა")]
    [InlineData(9, "ცხრა")]
    [InlineData(10, "ათი")]
    [InlineData(11, "თერთმეტი")]
    [InlineData(12, "თორმეტი")]
    [InlineData(13, "ცამეტი")]
    [InlineData(14, "თოთხმეტი")]
    [InlineData(15, "თხუთმეტი")]
    [InlineData(16, "თექვსმეტი")]
    [InlineData(17, "ჩვიდმეტი")]
    [InlineData(18, "თვრამეტი")]
    [InlineData(19, "ცხრამეტი")]
    [InlineData(20, "ოცი")]
    [InlineData(21, "ოცდაერთი")]
    [InlineData(22, "ოცდაორი")]
    [InlineData(29, "ოცდაცხრა")]
    [InlineData(30, "ოცდაათი")]
    [InlineData(31, "ოცდათერთმეტი")]
    [InlineData(32, "ოცდათორმეტი")]
    [InlineData(39, "ოცდაცხრამეტი")]
    [InlineData(40, "ორმოცი")]
    [InlineData(41, "ორმოცდაერთი")]
    [InlineData(49, "ორმოცდაცხრა")]
    [InlineData(50, "ორმოცდაათი")]
    [InlineData(51, "ორმოცდათერთმეტი")]
    [InlineData(59, "ორმოცდაცხრამეტი")]
    [InlineData(60, "სამოცი")]
    [InlineData(61, "სამოცდაერთი")]
    [InlineData(69, "სამოცდაცხრა")]
    [InlineData(70, "სამოცდაათი")]
    [InlineData(71, "სამოცდათერთმეტი")]
    [InlineData(79, "სამოცდაცხრამეტი")]
    [InlineData(80, "ოთხმოცი")]
    [InlineData(81, "ოთხმოცდაერთი")]
    [InlineData(89, "ოთხმოცდაცხრა")]
    [InlineData(90, "ოთხმოცდაათი")]
    [InlineData(91, "ოთხმოცდათერთმეტი")]
    [InlineData(99, "ოთხმოცდაცხრამეტი")]
    [InlineData(100, "ასი")]
    [InlineData(101, "ას ერთი")]
    [InlineData(199, "ას ოთხმოცდაცხრამეტი")]
    [InlineData(200, "ორასი")]
    [InlineData(299, "ორას ოთხმოცდაცხრამეტი")]
    [InlineData(300, "სამასი")]
    [InlineData(399, "სამას ოთხმოცდაცხრამეტი")]
    [InlineData(900, "ცხრაასი")]
    [InlineData(999, "ცხრაას ოთხმოცდაცხრამეტი")]
    [InlineData(1000, "ერთი ათასი")]
    [InlineData(2000, "ორი ათასი")]
    [InlineData(10_000, "ათი ათასი")]
    [InlineData(20_000, "ოცი ათასი")]
    [InlineData(100_000, "ასი ათასი")]
    [InlineData(200_000, "ორასი ათასი")]
    [InlineData(1_000_000, "ერთი მილიონი")]
    [InlineData(2_000_000, "ორი მილიონი")]
    [InlineData(10_000_000, "ათი მილიონი")]
    [InlineData(100_000_000, "ასი მილიონი")]
    [InlineData(200_000_000, "ორასი მილიონი")]
    [InlineData(1_000_000_000, "ერთი მილიარდი")]
    [InlineData(10_000_000_000, "ათი მილიარდი")]
    [InlineData(100_000_000_000, "ასი მილიარდი")]
    [InlineData(2_345, "ორი ათას სამას ორმოცდახუთი")]
    [InlineData(22_345, "ოცდაორი ათას სამას ორმოცდახუთი")]
    [InlineData(322_345, "სამას ოცდაორი ათას სამას ორმოცდახუთი")]
    [InlineData(1_322_345, "ერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი")]
    [InlineData(21_322_345, "ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი")]
    [InlineData(321_322_345, "სამას ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი")]
    [InlineData(7_321_322_345, "შვიდი მილიარდ სამას ოცდაერთი მილიონ სამას ოცდაორი ათას სამას ორმოცდახუთი")]
    public void RoundNumbers(long number, string result)
    {
        Convert(number).wholePart.Should().Be(result);
    }
}