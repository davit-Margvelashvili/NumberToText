﻿using System.Globalization;
using System.Text;

namespace NumberToText.Core;

public static class NumberToTextConverter
{
    #region Fields

    private static readonly Dictionary<byte, string> PostfixOnes = new()
    {
        [0] = "",
        [1] = "ერთი",
        [2] = "ორი",
        [3] = "სამი",
        [4] = "ოთხი",
        [5] = "ხუთი",
        [6] = "ექვსი",
        [7] = "შვიდი",
        [8] = "რვა",
        [9] = "ცხრა",
    };

    private static readonly Dictionary<byte, string> PrefixOnes = new()
    {
        [1] = "ერთ",
        [2] = "ორ",
        [3] = "სამ",
        [4] = "ოთხ",
        [5] = "ხუთ",
        [6] = "ექვს",
        [7] = "შვიდ",
        [8] = "რვა",
        [9] = "ცხრა",
    };

    private static readonly Dictionary<byte, string> Tens = new()
    {
        [0] = "ათი",
        [1] = "თერთმეტი",
        [2] = "თორმეტი",
        [3] = "ცამეტი",
        [4] = "თოთხმეტი",
        [5] = "თხუთმეტი",
        [6] = "თექვსმეტი",
        [7] = "ჩვიდმეტი",
        [8] = "თვრამეტი",
        [9] = "ცხრამეტი",
    };

    private static readonly Dictionary<byte, string> PrefixTwenties = new()
    {
        [0] = "",
        [2] = "ოც",
        [4] = "ორმოც",
        [6] = "სამოც",
        [8] = "ოთხმოც",
    };

    private static readonly Dictionary<byte, string> PostfixTwenties = new()
    {
        [0] = "",
        [1] = "",
        [2] = "ოცი",
        [3] = "ოც",
        [4] = "ორმოცი",
        [5] = "ორმოც",
        [6] = "სამოცი",
        [7] = "სამოც",
        [8] = "ოთხმოცი",
        [9] = "ოთხმოც",
    };

    private static readonly string[] GroupNames = {
        " ათას ",
        " მილიონ ",
        " მილიარდ ",
    };

    #endregion Fields

    #region Public Methods

    public static (string wholePart, string decimalPart) Convert(double number)
    {
        number = Math.Abs(number);

        if (number > 999999999999)
            throw new ArgumentOutOfRangeException(nameof(number), "Unable to convert. Number was too big");

        var textNumber = $"{number:N}";

        var decimalSeparator = NumberFormatInfo.CurrentInfo.NumberDecimalSeparator;
        var numberParts = textNumber.Split(new string[] { decimalSeparator }, StringSplitOptions.RemoveEmptyEntries);

        var wholePart = Convert(numberParts[0]);
        var decimalPart = Convert(numberParts[1]);

        return (wholePart, decimalPart);
    }

    #endregion Public Methods

    #region Private Methods

    private static string Convert(string number)
    {
        if (number.All(d => d == '0'))
            return "ნული";

        var numberGroups = number.Split(new[] { NumberFormatInfo.CurrentInfo.NumberGroupSeparator }, StringSplitOptions.RemoveEmptyEntries);

        return GroupsToText(numberGroups);
    }

    public static string GroupsToText(string[] groups)
    {
        var numberNames = groups.Select(GroupToText).ToArray();

        var builder = new StringBuilder();
        for (int i = numberNames.Length - 1, j = 0; i >= 0; i--, j++)
        {
            builder.Insert(0, numberNames[i]);

            if (j < numberNames.Length - 1)
            {
                if (numberNames[i - 1] == "")
                    continue;
                builder.Insert(0, GroupNames[j]);
            }
        }
        if (numberNames.Last() == "")
        {
            builder.Remove(builder.Length - 1, 1);
            builder.Append("ი");
        }

        return builder.ToString();
    }

    public static string GroupToText(string number)
    {
        var (first, second, third) = GetDigitsFrom(number);

        string hundred = FindHundred(first);
        string ten = FindTen(second, third);
        string one = FindOne(first, second, third);

        return BuildString(first, second, third, hundred, ten, one);
    }

    private static string BuildString(byte first, byte second, byte third, string hundred, string ten, string one)
    {
        var builder = new StringBuilder();
        builder.Append(hundred == "ერთ" ? "" : hundred);
        builder.Append(hundred == "" ? "" : "ას ");
        if (first > 0 && second == 0 && third == 0)
        {
            builder.Remove(builder.Length - 1, 1);
            builder.Append("ი");
        }
        else
        {
            builder.Append(ten);
            builder.Append(ten == "" ? "" : one == "" ? "" : "და");
            builder.Append(ten != "" && one == "" ? "" : one);
        }
        return builder.ToString();
    }

    private static (byte first, byte second, byte third) GetDigitsFrom(string number)
    {
        byte first = 0;
        byte second = 0;
        byte third = 0;
        int i = 0;

        if (number.Length > 2)
        {
            first = number[i].ToByte();
            i++;
        }

        if (number.Length > 1)
        {
            second = number[i].ToByte();
            i++;
        }
        third = number[i].ToByte();

        return (first, second, third);
    }

    private static string FindOne(byte first, byte second, byte third)
    {
        if (first == 0 && second == 0) return PostfixOnes[third];
        return second.IsOdd() ? Tens[third] : PostfixOnes[third];
    }

    private static string FindTen(byte second, byte third)
    {
        if (second == 0) return "";
        if (third == 0) return PostfixTwenties[second];
        return second.IsOdd() ? PrefixTwenties[(byte)(second - 1)] : PrefixTwenties[second];
    }

    private static string FindHundred(byte first)
    {
        if (first == 0) return "";
        return PrefixOnes[first];
    }

    public static byte ToByte(this char digit)
    {
        if (char.IsDigit(digit)) return byte.Parse($"{digit}");
        else throw new FormatException("parameter was not digit");
    }

    public static bool IsOdd(this byte digit)
    {
        return digit % 2 == 1;
    }

    #endregion Private Methods
}