using System;
using System.Collections.Generic;
using Xunit;
namespace SampleApp
{
  public class TestRunner
  {
    [Theory]
    [InlineData(1, 4, 5)]
    public void TestCases(int first, int second, int sum)
    {
      Assert.Equal(sum, Program.Sum(first, second));
    }

  }
}