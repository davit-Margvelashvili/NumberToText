class NumberToTextConverter {
    private static postfixOnes: { [key: number]: string } = {
        0: "",
        1: "ერთი",
        2: "ორი",
        3: "სამი",
        4: "ოთხი",
        5: "ხუთი",
        6: "ექვსი",
        7: "შვიდი",
        8: "რვა",
        9: "ცხრა",
    };

    private static prefixOnes: { [key: number]: string } = {
        1: "ერთ",
        2: "ორ",
        3: "სამ",
        4: "ოთხ",
        5: "ხუთ",
        6: "ექვს",
        7: "შვიდ",
        8: "რვა",
        9: "ცხრა",
    };

    private static tens: { [key: number]: string } = {
        0: "ათი",
        1: "თერთმეტი",
        2: "თორმეტი",
        3: "ცამეტი",
        4: "თოთხმეტი",
        5: "თხუთმეტი",
        6: "თექვსმეტი",
        7: "ჩვიდმეტი",
        8: "თვრამეტი",
        9: "ცხრამეტი",
    };

    private static prefixTwenties: { [key: number]: string } = {
        0: "",
        2: "ოც",
        4: "ორმოც",
        6: "სამოც",
        8: "ოთხმოც",
    };

    private static postfixTwenties: { [key: number]: string } = {
        0: "",
        1: "",
        2: "ოცი",
        3: "ოც",
        4: "ორმოცი",
        5: "ორმოც",
        6: "სამოცი",
        7: "სამოც",
        8: "ოთხმოცი",
        9: "ოთხმოც",
    };

    private static groupNames: string[] = [" ათას ", " მილიონ ", " მილიარდ "];

    public static convert(number: number): { wholePart: string; decimalPart: string } {
        number = Math.abs(number);

        if (number > 999999999999) {
            throw new Error("Unable to convert. Number was too big");
        }

        const textNumber = number.toLocaleString('en-US', {
            style: 'decimal',
            minimumFractionDigits: 2,
            useGrouping: true,
            maximumFractionDigits: 10
          });

        const decimalSeparator = ".";
        const numberParts = textNumber.split(decimalSeparator);

        const wholePart = NumberToTextConverter.convertNumber(numberParts[0]);
        if(numberParts.length === 1)
        {
          numberParts.push('0');
        }
        
        const decimalPart = NumberToTextConverter.convertNumber(numberParts[1]);
        
        return { wholePart, decimalPart };
    }

    private static convertNumber(number: string): string {
          if (Number.parseInt(number) === 0) {
            return "ნული";
        }

        const groupSeparator = ",";
        const numberGroups = number.split(groupSeparator);

        return NumberToTextConverter.groupsToText(numberGroups);
    }

    private static groupsToText(groups: string[]): string {
        const numberNames = groups.map((n) => NumberToTextConverter.groupToText(n));

        const builder: string[] = [];
        for (let i = numberNames.length - 1, j = 0; i >= 0; i--, j++) {
            builder.unshift(numberNames[i]);

            if (j < numberNames.length - 1) {
                if (numberNames[i - 1] === "") {
                    continue;
                }
                builder.unshift(NumberToTextConverter.groupNames[j]);
            }
        }
        if (numberNames[numberNames.length - 1] === "") {
            const lastIndex = builder.length-1;
            builder[lastIndex] = builder[lastIndex].trim() + "ი";
        }

        return builder.join("");
    }

    private static groupToText(number: string): string {
        const { first, second, third } = NumberToTextConverter.getDigitsFrom(number);

        const hundred = NumberToTextConverter.findHundred(first);
        const ten = NumberToTextConverter.findTen(second, third);
        const one = NumberToTextConverter.findOne(first, second, third);

        return NumberToTextConverter.buildString(first, second, third, hundred, ten, one);
    }

    private static buildString(
        first: number,
        second: number,
        third: number,
        hundred: string,
        ten: string,
        one: string
    ): string {
        const builder: string[] = [];
        builder.push(hundred === "ერთ" ? "" : hundred);
        builder.push(hundred === "" ? "" : "ას ");
        if (first > 0 && second === 0 && third === 0) {
          const lastIndex = builder.length-1;
          builder[lastIndex] = builder[lastIndex].trim() + "ი";  
        } else {
            builder.push(ten);
            builder.push(ten === "" ? "" : one === "" ? "" : "და");
            builder.push(ten !== "" && one === "" ? "" : one);
        }
        return builder.join("");
    }

    private static getDigitsFrom(number: string): { first: number, second: number, third: number } {
        let first = 0;
        let second = 0;
        let third = 0;
        let i = 0;

        if (number.length > 2) {
            first = NumberToTextConverter.toNumber(number[i]);
            i++;
        }

        if (number.length > 1) {
            second = NumberToTextConverter.toNumber(number[i]);
            i++;
        }
        third = NumberToTextConverter.toNumber(number[i]);

        return { first, second, third };
    }

    private static toNumber(digit: string): number {
        const parsedDigit = Number.parseInt(digit);
        if (Number.isNaN(parsedDigit)) {
            throw new Error("Parameter was not a digit");
        }
        return parsedDigit;
    }

    private static findOne(first: number, second: number, third: number): string {
        if (first === 0 && second === 0) {
            return NumberToTextConverter.postfixOnes[third];
        }
        return second % 2 === 1 ? NumberToTextConverter.tens[third] : NumberToTextConverter.postfixOnes[third];
    }

    private static findTen(second: number, third: number): string {
        if (second === 0) {
            return "";
        }
        if (third === 0) {
            return NumberToTextConverter.postfixTwenties[second];
        }
        return second % 2 === 1 ? NumberToTextConverter.prefixTwenties[second - 1] : NumberToTextConverter.prefixTwenties[second];
    }

    private static findHundred(first: number): string {
        if (first === 0) {
            return "";
        }
        return NumberToTextConverter.prefixOnes[first];
    }
}

function GEL(n: number): string {
    const result = NumberToTextConverter.convert(n);
    return `${result.wholePart} ლარი და ${result.decimalPart} თეთრი`;
  }

