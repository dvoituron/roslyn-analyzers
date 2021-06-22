﻿// Copyright (c) Microsoft.  All Rights Reserved.  Licensed under the Apache License, Version 2.0.  See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using BenchmarkDotNet.Attributes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.NetCore.Analyzers.Runtime;
using PerformanceTests.Utilities;
using PerfUtilities;

namespace VisualBasicPerformanceTests.Enabled
{
    public class VisualBasic_CA2013
    {
        [IterationSetup]
        public static void CreateEnvironmentCA2013()
        {
            var sources = new List<(string name, string content)>();
            for (var i = 0; i < Constants.Number_Of_Code_Files; i++)
            {
                var name = "TypeName" + i;
                sources.Add((name, @$"
Imports System

Class {name}
    Private Shared Function TestMethod(test As Integer, other As Object) As Boolean
        Return ReferenceEquals(test, other)
    End Function
End Class
"));
            }

            var compilation = VisualBasicCompilationHelper.CreateAsync(sources.ToArray()).GetAwaiter().GetResult();
            BaselineCompilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create<DiagnosticAnalyzer>(new EmptyAnalyzer()));
            CompilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create<DiagnosticAnalyzer>(new DoNotUseReferenceEqualsWithValueTypesAnalyzer()));
        }

        private static CompilationWithAnalyzers BaselineCompilationWithAnalyzers;
        private static CompilationWithAnalyzers CompilationWithAnalyzers;

        [Benchmark]
        public void CA2013_DiagnosticsProduced()
        {
            var analysisResult = CompilationWithAnalyzers.GetAnalysisResultAsync(default).GetAwaiter().GetResult();
            var diagnostics = analysisResult.GetAllDiagnostics(analysisResult.Analyzers.Single());
            if (analysisResult.Analyzers.Length != 1)
            {
                throw new InvalidOperationException($"Expected a single analyzer but found '{analysisResult.Analyzers.Length}'");
            }

            if (analysisResult.CompilationDiagnostics.Count != 0)
            {
                throw new InvalidOperationException($"Expected no compilation diagnostics but found '{analysisResult.CompilationDiagnostics.Count}'");
            }

            if (diagnostics.Length != 1_000)
            {
                throw new InvalidOperationException($"Expected '1,000' analyzer diagnostics but found '{diagnostics.Length}'");
            }
        }

        [Benchmark(Baseline = true)]
        public void CA2013_Baseline()
        {
            var analysisResult = BaselineCompilationWithAnalyzers.GetAnalysisResultAsync(default).GetAwaiter().GetResult();
            var diagnostics = analysisResult.GetAllDiagnostics(analysisResult.Analyzers.Single());
            if (analysisResult.Analyzers.Length != 1)
            {
                throw new InvalidOperationException($"Expected a single analyzer but found '{analysisResult.Analyzers.Length}'");
            }

            if (analysisResult.CompilationDiagnostics.Count != 0)
            {
                throw new InvalidOperationException($"Expected no compilation diagnostics but found '{analysisResult.CompilationDiagnostics.Count}'");
            }

            if (diagnostics.Length != 0)
            {
                throw new InvalidOperationException($"Expected no analyzer diagnostics but found '{diagnostics.Length}'");
            }
        }
    }
}
