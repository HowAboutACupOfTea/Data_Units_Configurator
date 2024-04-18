//-----------------------------------------------------------------------
// <copyright file="Application.cs" company="CompanyName">
//     Company copyright tag.
// </copyright>
// <summary>
// This file contains the Application class.
// </summary>
// <author> Daniel Karner </author>
//-----------------------------------------------------------------------
namespace DataUnitsConfigurator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;

    /// <summary>
    /// Implements the application and its logic.
    /// </summary>
    public class Application
    {
        /// <summary>
        /// This methods starts the application.
        /// </summary>
        public void Start()
        {
            string[] sourceUnitsPaths = this.GetUnitFilePaths("DSU");
            string[] processingUnitsPaths = this.GetUnitFilePaths("DPU");
            string[] visualizationUnitsPaths = this.GetUnitFilePaths("DVU");

            Type[] sourceUnits = this.GetUnitsType(sourceUnitsPaths, "IDataSourceUnit");
            Type[] processingUnits = this.GetUnitsType(processingUnitsPaths, "IDataProcessorUnit");
            Type[] visualizationUnits = this.GetUnitsType(visualizationUnitsPaths, "IDataVisualizerUnit");

            this.PrintUnitInformation(sourceUnits, "Source unit/s", 0);
            this.PrintUnitInformation(processingUnits, "Processing unit/s", sourceUnits.Length);
            this.PrintUnitInformation(visualizationUnits, "Visualization unit/s", sourceUnits.Length + processingUnits.Length);
            
            int[] chosenCombination = this.GetValidCombination(sourceUnits, processingUnits, visualizationUnits);

            Console.WriteLine();
            Console.WriteLine("Press escape to stop using the chosen combination.");
            Console.WriteLine("Any other key will use the given combination again.");

            while (true)
            {
                this.UseCombination(chosenCombination, sourceUnits, processingUnits, visualizationUnits);

                if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                {
                    chosenCombination = this.GetValidCombination(sourceUnits, processingUnits, visualizationUnits);
                }
            }
        }

        /// <summary>
        /// This method uses a data visualization unit to visualize the given data.
        /// </summary>
        /// <param name="dataVisualizer">The given data visualization unit.</param>
        /// <param name="data">The data to visualize.</param>
        private void UseVisU(Type dataVisualizer, dynamic[] data)
        {
            MethodInfo displayData = dataVisualizer.GetMethod("DisplayData");

            var member = dataVisualizer.InvokeMember(string.Empty, BindingFlags.CreateInstance, null, new object(), new object[0]);

            var array = Array.CreateInstance(data[0].GetType(), data.Length);

            for (int i = 0; i < data.Length; i++)
            {
                array.SetValue(data[i], i);
            }

            displayData.Invoke(member, new object[] { array });
        }

        /// <summary>
        /// This method uses a data processing unit to process the given data.
        /// </summary>
        /// <param name="dataProcessor">The given data processing unit.</param>
        /// <param name="generatedData">The generated data to process.</param>
        /// <returns>The processed data.</returns>
        private dynamic GetOutputFromProU(Type dataProcessor, dynamic[] generatedData)
        {
            MethodInfo processData = dataProcessor.GetMethod("ProcessData");

            var member = dataProcessor.InvokeMember(string.Empty, BindingFlags.CreateInstance, null, new object(), new object[0]);

            var array = Array.CreateInstance(generatedData[0].GetType(), generatedData.Length);

            for (int i = 0; i < generatedData.Length; i++)
            {
                array.SetValue(generatedData[i], i);
            }

            return processData.Invoke(member, new object[] { array });
        }

        /// <summary>
        /// This method uses a data source unit to generate data.
        /// </summary>
        /// <param name="dataSource">The given data source unit.</param>
        /// <returns>The generated data.</returns>
        private dynamic GetOutputFromSouU(Type dataSource)
        {
            MethodInfo getOutput = dataSource.GetMethod("GenerateOutput");

            var member = dataSource.InvokeMember(string.Empty, BindingFlags.CreateInstance, null, new object(), new object[0]);

            return getOutput.Invoke(member, new object[0]);
        }

        /// <summary>
        /// This method executes the user chosen combination of extension units.
        /// </summary>
        /// <param name="combinations">The combination of extension units as chosen by the user.</param>
        /// <param name="sourceUnits">An array containing the types of the source units.</param>
        /// <param name="processingUnits">An array containing the types of the processing units.</param>
        /// <param name="visualizationUnits">An array containing the types of the visualization units.</param>
        private void UseCombination(int[] combinations, Type[] sourceUnits, Type[] processingUnits, Type[] visualizationUnits)
        {
            List<Type> chosenSouUs = new List<Type>();
            List<Type> chosenProUs = new List<Type>();
            List<Type> chosenVisUs = new List<Type>();

            for (int i = 0; i < combinations.Length; i++)
            {
                if (combinations[i] >= 0 && combinations[i] < sourceUnits.Length)
                {
                    chosenSouUs.Add(sourceUnits[combinations[i]]);
                }
                else if (combinations[i] >= sourceUnits.Length && combinations[i] < sourceUnits.Length + processingUnits.Length)
                {
                    chosenProUs.Add(processingUnits[combinations[i] - sourceUnits.Length]);
                }
                else if (combinations[i] >= sourceUnits.Length + processingUnits.Length && combinations[i] < sourceUnits.Length + processingUnits.Length + visualizationUnits.Length)
                {
                    chosenVisUs.Add(visualizationUnits[combinations[i] - sourceUnits.Length - processingUnits.Length]);
                }
            }

            dynamic[] generatedData = new dynamic[chosenSouUs.Count];

            for (int i = 0; i < chosenSouUs.Count; i++)
            {
                generatedData[i] = this.GetOutputFromSouU(chosenSouUs[i]);
            }

            dynamic[] processedData = new dynamic[chosenProUs.Count];

            for (int i = 0; i < chosenProUs.Count; i++)
            {
                processedData[i] = this.GetOutputFromProU(chosenProUs[i], generatedData);
            }

            if (chosenProUs.Count <= 0)
            {
                for (int i = 0; i < chosenVisUs.Count; i++)
                {
                    this.UseVisU(chosenVisUs[i], generatedData);
                }
            }
            else
            {
                for (int i = 0; i < chosenVisUs.Count; i++)
                {
                    this.UseVisU(chosenVisUs[i], processedData);
                }
            }
        }

        /// <summary>
        /// This method gets a valid combination for the given units from the user.
        /// </summary>
        /// <param name="sourceUnits">An array containing the types of the source units.</param>
        /// <param name="processingUnits">An array containing the types of the processing units.</param>
        /// <param name="visualizationUnits">An array containing the types of the visualization units.</param>
        /// <returns>An integer array containing the numbers of the chosen units.</returns>
        private int[] GetValidCombination(Type[] sourceUnits, Type[] processingUnits, Type[] visualizationUnits)
        {
            int[] chosenCombination = this.GetCombination();

            while (!this.IsCombinationValid(chosenCombination, sourceUnits, processingUnits, visualizationUnits))
            {
                Console.WriteLine("The combination you chose does not work.");
                chosenCombination = this.GetCombination();
            }

            return chosenCombination;
        }

        /// <summary>
        /// This method checks if the user chosen combination are valid.
        /// </summary>
        /// <param name="combination">The combination of extension units as chosen by the user.</param>
        /// <param name="sourceUnits">An array containing the types of the source units.</param>
        /// <param name="processingUnits">An array containing the types of the processing units.</param>
        /// <param name="visualizationUnits">An array containing the types of the visualization units.</param>
        /// <returns>True if the combinations are valid, otherwise false.</returns>
        private bool IsCombinationValid(int[] combination, Type[] sourceUnits, Type[] processingUnits, Type[] visualizationUnits)
        {
            List<Type> chosenSouUs = new List<Type>();
            List<Type> chosenProUs = new List<Type>();
            List<Type> chosenVisUs = new List<Type>();

            for (int i = 0; i < combination.Length; i++)
            {
                if (combination[i] >= 0 && combination[i] < sourceUnits.Length)
                {
                    chosenSouUs.Add(sourceUnits[combination[i]]);
                }
                else if (combination[i] >= sourceUnits.Length && combination[i] < sourceUnits.Length + processingUnits.Length)
                {
                    chosenProUs.Add(processingUnits[combination[i] - sourceUnits.Length]);
                }
                else if (combination[i] >= sourceUnits.Length + processingUnits.Length && combination[i] < sourceUnits.Length + processingUnits.Length + visualizationUnits.Length)
                {
                    chosenVisUs.Add(visualizationUnits[combination[i] - sourceUnits.Length - processingUnits.Length]);
                }
                else
                {
                    return false;
                }
            }

            if (chosenSouUs.Count <= 0 || chosenVisUs.Count <= 0)
            {
                return false;
            }

            ParameterInfo[] returnParametersSouU = this.GetReturnParameters(chosenSouUs, "GenerateOutput", chosenSouUs.Count);

            ParameterInfo[] inputTypesProU = this.GetInputParameters(chosenProUs, "ProcessData", chosenProUs.Count);
            ParameterInfo[] returnParametersProU = this.GetReturnParameters(chosenProUs, "ProcessData", chosenProUs.Count);

            ParameterInfo[] inputTypesVisU = this.GetInputParameters(chosenVisUs, "DisplayData", chosenVisUs.Count);

            if (chosenProUs.Count <= 0 && !this.HasSameTypes(returnParametersSouU, inputTypesVisU))
            {
                return false;
            }

            if (!this.HasSameTypes(returnParametersSouU, inputTypesProU) || !this.HasSameTypes(returnParametersProU, inputTypesVisU))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This method checks if the given arrays contain the same parameter types.
        /// </summary>
        /// <param name="returnParameters">The parameter info of the return parameters.</param>
        /// <param name="inputTypes">The parameter info of the input parameters.</param>
        /// <returns>True if the arrays contain the same parameter types, otherwise false.</returns>
        private bool HasSameTypes(ParameterInfo[] returnParameters, ParameterInfo[] inputTypes)
        {
            for (int i = 0; i < returnParameters.Length; i++)
            {
                for (int j = 0; j < inputTypes.Length; j++)
                {
                    if (returnParameters[i].ParameterType.Name + "[]" != inputTypes[j].ParameterType.Name)
                    {
                        return false;
                    }
                }
            }
            
            return true;
        }

        /// <summary>
        /// This method gets the input parameters of a certain method contained in the given types. 
        /// </summary>
        /// <param name="chosenUnits">An array containing the types of the chosen extension units.</param>
        /// <param name="methodName">The name of the method which parameters are wanted.</param>
        /// <param name="length">The length of the array that will get returned.</param>
        /// <returns>An array containing the parameter info of a certain method.</returns>
        private ParameterInfo[] GetInputParameters(List<Type> chosenUnits, string methodName, int length)
        {
            ParameterInfo[] inputParameters = new ParameterInfo[length];

            for (int i = 0; i < length; i++)
            {
                MethodInfo method = chosenUnits[i].GetMethod(methodName);
                inputParameters[i] = method.GetParameters()[0];
            }

            return inputParameters;
        }

        /// <summary>
        /// This method gets the return parameters of a certain method contained in the given types.
        /// </summary>
        /// <param name="chosenUnits">An array containing the types of the chosen extension units.</param>
        /// <param name="methodName">The name of the method which parameters are wanted.</param>
        /// <param name="length">The length of the array that will get returned.</param>
        /// <returns>An array containing the parameter info of a certain method.</returns>
        private ParameterInfo[] GetReturnParameters(List<Type> chosenUnits, string methodName, int length)
        {
            ParameterInfo[] returnParameters = new ParameterInfo[length];

            for (int i = 0; i < length; i++)
            {
                MethodInfo method = chosenUnits[i].GetMethod(methodName);
                returnParameters[i] = method.ReturnParameter;
            }

            return returnParameters;
        }

        /// <summary>
        /// This method prompts the user to choose a combination of extension units and saves the numbers of the chosen units in an integer array.
        /// </summary>
        /// <returns>An integer array containing the numbers of the chosen units.</returns>
        private int[] GetCombination()
        {
            Console.WriteLine("Please enter the numbers of the units you want to combine in the correct order.");
            Console.WriteLine("Beginning with one or more data source unit/s, followed by one or more data processing unit/s and lastly one or more data visualization unit/s.");
            Console.WriteLine("(possible example: 1, 3, 6)");

            while (true)
            {
                string chosenCombinations = Console.ReadLine();

                string[] combinations = chosenCombinations.Split(',');

                for (int i = 0; i < combinations.Length; i++)
                {
                    combinations[i] = combinations[i].Trim();
                }

                int[] unitIndexes = new int[combinations.Length];
                bool[] hasConverted = new bool[combinations.Length];

                for (int i = 0; i < combinations.Length; i++)
                {
                    hasConverted[i] = int.TryParse(combinations[i], out unitIndexes[i]);
                }

                if (this.ContainsFalse(hasConverted))
                {
                    Console.WriteLine("The format of your selection was wrong, please try again:");
                    continue;
                }

                return unitIndexes;
            }
        }

        /// <summary>
        /// This method checks if the given array contains false.
        /// </summary>
        /// <param name="booleans">An array containing booleans.</param>
        /// <returns>True if the array contains false, otherwise false.</returns>
        private bool ContainsFalse(bool[] booleans)
        {
            for (int i = 0; i < booleans.Length; i++)
            {
                if (booleans[i] == false)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// This method gets the type of the dynamic link library or exe file that implements the correct interface.
        /// </summary>
        /// <param name="unitPaths">The paths containing the assembly files.</param>
        /// <param name="unitInterface">The interface that the type must implement.</param>
        /// <returns>An array containing the relevant type/class of the assemblies at the given paths.</returns>
        private Type[] GetUnitsType(string[] unitPaths, string unitInterface)
        {
            Type[] classes = new Type[unitPaths.Length];

            for (int i = 0; i < unitPaths.Length; i++)
            {
                Assembly assembly;

                try
                {
                    assembly = Assembly.LoadFrom(unitPaths[i]);
                }
                catch (IOException e)
                {
                    throw new IOException($"{nameof(assembly)} from {nameof(unitPaths) + unitPaths[i]} could not be loaded.", e);
                }
                catch (BadImageFormatException e)
                {
                    throw new BadImageFormatException($"File at path: {unitPaths[i]} is not a .NET assembly.", e);
                }

                Type[] types = assembly.GetTypes();
                classes[i] = this.GetInterfaceImplementingType(types, unitInterface, unitPaths[i]);
            }

            return classes;
        }

        /// <summary>
        /// This method invokes the show information method of the given classes.
        /// </summary>
        /// <param name="units">The given classes.</param>
        /// <param name="unitType">The unit type of the given classes.</param>
        /// <param name="startNumber">The starting number of the unit.</param>
        private void PrintUnitInformation(Type[] units, string unitType, int startNumber)
        {
            Console.WriteLine(unitType + ":");
            Console.WriteLine();

            for (int i = 0; i < units.Length; i++)
            {
                Console.WriteLine($"Unit-number: {startNumber++}");
                MethodInfo showInfo = units[i].GetMethod("ShowInformation");

                if (units[i].IsAbstract && units[i].GetConstructors().Length <= 0)
                {
                    throw new Exception($"Class {units[i].Name} can't be used because it is abstract and no constructors were found.");
                }

                var member = units[i].InvokeMember(string.Empty, BindingFlags.CreateInstance, null, new object(), new object[0]);
                showInfo.Invoke(member, new object[0]);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// This method searches for a class that implements the given interface.
        /// </summary>
        /// <param name="types">The classes to check.</param>
        /// <param name="unitInterface">The interface, which must be implemented.</param>
        /// <param name="path">The path of the assembly of the current class. Only needed for an exception.</param>
        /// <returns>The class implementing the given interface.</returns>
        private Type GetInterfaceImplementingType(Type[] types, string unitInterface, string path)
        {
            for (int i = 0; i < types.Length; i++)
            {
                if (types[i].GetInterface(unitInterface + "`1") != null)
                {
                    return types[i];
                }
            }

            throw new ArgumentException($"{unitInterface}s contains an assembly which does not implement a class with the needed interface - path: {path}.");
        }

        /// <summary>
        /// This method searches for the files contained in a certain subdirectory of the Extensions folder.
        /// The Extensions folder is contained in the current working directory.
        /// </summary>
        /// <param name="unitName">The name of the subdirectory.</param>
        /// <returns>A string array containing the file paths.</returns>
        private string[] GetUnitFilePaths(string unitName)
        {
            string[] paths;

            try
            {
                paths = Directory.GetDirectories(@".\Extensions");
            }
            catch (DirectoryNotFoundException e)
            {
                throw new DirectoryNotFoundException("The Extensions folder could not be found.", e);
            }

            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i].EndsWith(unitName))
                {
                    return Directory.GetFiles(paths[i]);
                }
            }

            throw new DirectoryNotFoundException($"The directory containing the {unitName} could not be found.");
        }
    }
}