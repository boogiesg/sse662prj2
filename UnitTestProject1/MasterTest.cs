﻿using System;
using System.IO;
using System.Linq;
using Trivia;
using UglyTrivia;
using Xunit;

namespace UnitTestProject1
{
    public class MasterTest : IDisposable
    {
        private readonly TextWriter _originalOut;
        private const string ConsoleOutput = "output.txt";
        private const string MasterCopy = "../../master.txt";

        public MasterTest()
        {
            _originalOut = Console.Out;
        }

        public void Dispose()
        {
            Console.SetOut(_originalOut);
        }

        [Fact]
        public void generate_master()
        {
            RunTheProgram(seed: 99, outputFile: ConsoleOutput, times: 100);

            var actual = File.ReadAllText(ConsoleOutput);
            var goldenMaster = File.ReadAllText(MasterCopy);

            Assert.Equal(goldenMaster, actual);
        }

        [Fact]
        public void checks_wheter_the_output_is_deterministic()
        {
            RunTheProgram(seed: 99, outputFile: "output1.txt", times: 100);

            RunTheProgram(seed: 99, outputFile: "output2.txt", times: 100);

            var actual1 = File.ReadAllText("output1.txt");
            var actual2 = File.ReadAllText("output2.txt");
            Assert.Equal(actual1, actual2);
        }

        private static void RunTheProgram(int seed, string outputFile, int times)
        {
            int times1;
            using (var writer = File.CreateText(outputFile))
            {
                Console.SetOut(writer);
                foreach (var i in Enumerable.Range(0, times))
                {
                    GameRunner.Run(new Random(seed));
                }
            }
        }


        [Fact]
        public void TestMethod1()
        {
        }

    }
}
