using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace MiniAccounting.Tests;

[TestClass]
public class MiniAccountingServiceTests : TestBase
{
    [TestMethod]
    public async Task TopUpTotalBalanceTest()
    {
        //Arrange
        var addMoney = 50.0;
        var comment = "money";

        await Test(async () =>
        {
            //Act
            var before = await Client.GetTotalBalanceAsync();
            Logger.WriteLine($"GetTotalBalanceAsync before = {before}");

            var result = await Client.TopUpTotalBalanceAsync(addMoney, comment);
            Logger.WriteLine($"TopUpTotalBalanceAsync result = {result}");

            var after = await Client.GetTotalBalanceAsync();
            Logger.WriteLine($"GetTotalBalanceAsync after = {after}");

            //Assert
            var expectedResult = before + addMoney;
            Assert.AreEqual(expectedResult, result, ValueNotMatchError(expectedResult.ToString(), result.ToString()));
            Assert.AreEqual(expectedResult, after, ValueNotMatchError(expectedResult.ToString(), after.ToString()));
        }, async (isSuccessTest) =>
        {
            if (isSuccessTest)
            {
                await Client.RemoveFromTotalBalanceAsync(addMoney, 
                    $"Подчистил за автотестом {nameof(MiniAccountingServiceTests)}.{nameof(TopUpTotalBalanceTest)}");
            }
        });
    }

    [TestMethod]
    public async Task RevomeFromTotalBalanceTest()
    {
        var removeMoney = 101;
        var comment = "test 101";

        await Test(async() =>
        {
            var before = await Client.GetTotalBalanceAsync();
            Logger.WriteLine($"GetTotalBalanceAsync before = {before}");

            var result = await Client.RemoveFromTotalBalanceAsync(removeMoney, comment);
            Logger.WriteLine($"RemoveFromTotalBalanceAsync result = {result}");

            var after = await Client.GetTotalBalanceAsync();
            Logger.WriteLine($"GetTotalBalanceAsync after = {result}");

            var expectedResult = before - removeMoney;
            Assert.AreEqual(expectedResult, result, $"Значения результата не совпадает, ожидается {expectedResult}, получено {result}");
            Assert.AreEqual(expectedResult, after, $"Значения результата не совпадает, ожидается {expectedResult}, получено {after}");
        }, async (isSuccessTest) =>
        {
            if (isSuccessTest)
            {
                await Client.TopUpTotalBalanceAsync(removeMoney, 
                    $"Подчистил за автотестом {nameof(MiniAccountingServiceTests)}.{nameof(RevomeFromTotalBalanceTest)}");
            }
        });
    }

    [TestMethod]
    [DataRow("Tester", 100)]
    [DataRow("Test !@#$%^&4*)+=-][\\d1';(,./}|\":>?~{< Тест", -100)]
    public async Task AddUserTest(string name, double money)
    {
        var testUser = new User(name, money);
        
        await Test(async () =>
        {
            await Client.SaveAsync(testUser);
            Logger.WriteLine($"Создал юзера {JsonConvert.SerializeObject(testUser)}");
            var resultUser = await Client.ReadAsync(testUser.Uid);
            Assert.AreEqual(testUser.Name, resultUser.Name, ValueNotMatchError(testUser.Name, resultUser.Name));
            Assert.AreEqual(testUser.Uid, resultUser.Uid, ValueNotMatchError(testUser.Uid.ToString(), resultUser.Uid.ToString()));
        }, async (isSuccessTest) =>
        {
            await Client.DeleteAsync(testUser.Uid);
            Logger.WriteLine($"Удалил юзера {testUser.Uid}");
        });
    }

    [TestMethod]
    [DataRow("firstName", "secondName", 25.0, 100.5)]
    public async Task EditUserTest(string firstName, string expectedResultName, double firstMoney, double expectedResultMoney)
    {
        var firstUser = new User(firstName, firstMoney);
        var expectedResultUser = new User(expectedResultName, expectedResultMoney, firstUser.Uid);

        await Test(async () =>
        {
            await Client.SaveAsync(firstUser);
            Logger.WriteLine($"Создал юзера {JsonConvert.SerializeObject(firstUser)}");
            await Client.EditAsync(expectedResultUser);
            Logger.WriteLine($"Изменил юзера на {JsonConvert.SerializeObject(expectedResultUser)}");
            var resultUser = await Client.ReadAsync(firstUser.Uid);

            Assert.AreEqual(expectedResultUser.Name, resultUser.Name);
            Assert.AreEqual(expectedResultUser.Money, resultUser.Money);
        }, async (isSuccessTest) =>
        {
            await Client.DeleteAsync(firstUser.Uid);
            Logger.WriteLine($"Удалил юзера {firstUser.Uid}");
        });
    }

    private string ValueNotMatchError(string expected, string actual)
    {
        return $"Значение не совпадает, ожидается '{expected}', получено '{actual}'";
    }
}