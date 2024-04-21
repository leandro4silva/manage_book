namespace ManageBooks.UnitTests.Application.User.CreateUser;

public class CreateUserTestDataGenerator
{
    public static IEnumerable<object[]> GetInvalidInputs(int times = 12)
    {
        var fixture = new CreateUserTestFixture();
        var invalidInputList = new List<object[]>();
        var totalInvalidCases = 4;

        for (int index = 0; index < times; index++)
        {
            switch (index % totalInvalidCases)
            {
                case 0:
                    //nome não pode ser maior que 255 caracteres
                    invalidInputList.Add(new object[] {
                        fixture.GetInvalidInputLongName(),
                        "Name should not be greater than 255 characters long"
                    });
                    break;
                case 1:
                    //nome não pode ser nula
                    invalidInputList.Add(new object[] {
                        fixture.GetInvalidInputNameNull(),
                        "Name should not be empty or null"
                    });
                    break;
                case 2:
                    //email não pode ser nula
                    invalidInputList.Add(new object[] {
                        fixture.GetInvalidInputEmailNull(),
                        "Email should not be empty or null"
                    });
                    break;
                case 3:
                    //email não pode ser invalido
                    invalidInputList.Add(new object[] {
                        fixture.GetInvalidInputEmailIsNotValid(),
                        "Email should be a valid email"
                    });
                    break;
                default: break;
            }
        }

        return invalidInputList;
    }
}
